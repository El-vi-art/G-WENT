using System;
using System.Collections.Generic;

class Parser
{
    private readonly List<Token> tokens;
    private int current = 0;

    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
    }

    public Program Parse()
    {
        var statements = new List<Statement>();
        while (!IsAtEnd())
        {
            statements.Add(Declaration());
        }
        return new Program { MainBlock = new Block { Statements = statements } };
    }

    private Statement Declaration()
    {
        try
        {
            if (Match(TokenType.Effect)) return EffectStatement();
            if (Match(TokenType.Card)) return CardStatement();
            if (Match(TokenType.For)) return ForStatement();
            if (Match(TokenType.While)) return WhileStatement();
            if (Match(TokenType.If)) return IfStatement();

            return Statement();
        }
        catch (ParseException)
        {
            Synchronize();
            return null;
        }
    }

    private Statement Statement()
    {
        if (Match(TokenType.LeftBrace)) return new Block { Statements = Block() };
        if (Match(TokenType.Identifier)) return Assignment();
        throw Error(Peek(), "Expected statement.");
    }

    private Statement EffectStatement()
    {
        var name = Consume(TokenType.Identifier, "Expect effect name.");
        Consume(TokenType.Semicolon, "Expect ';' after effect.");
        return new EffectStatement { Effect = name.Lexeme };
    }

    private Statement CardStatement()
    {
        var name = Consume(TokenType.Identifier, "Expect card name.");
        Consume(TokenType.Semicolon, "Expect ';' after card.");
        return new CardStatement { Card = name.Lexeme };
    }

    private Statement ForStatement()
    {
        Consume(TokenType.LeftParen, "Expect '(' after 'for'.");
        var initializer = Statement();
        var condition = Expression();
        Consume(TokenType.Semicolon, "Expect ';' after loop condition.");
        var increment = Statement();
        Consume(TokenType.RightParen, "Expect ')' after for clauses.");
        var body = Statement();

        return new ForStatement
        {
            Initialization = initializer,
            Condition = condition,
            Increment = increment,
            Body = body
        };
    }

    private Statement WhileStatement()
    {
        Consume(TokenType.LeftParen, "Expect '(' after 'while'.");
        var condition = Expression();
        Consume(TokenType.RightParen, "Expect ')' after condition.");
        var body = Statement();

        return new WhileStatement { Condition = condition, Body = body };
    }

    private Statement IfStatement()
    {
        Consume(TokenType.LeftParen, "Expect '(' after 'if'.");
        var condition = Expression();
        Consume(TokenType.RightParen, "Expect ')' after if condition.");
        var thenBranch = Statement();
        Statement elseBranch = null;
        if (Match(TokenType.Else))
        {
            elseBranch = Statement();
        }

        return new IfStatement { Condition = condition, ThenBranch = thenBranch, ElseBranch = elseBranch };
    }

    private Statement Assignment()
    {
        var name = Previous();
        if (Match(TokenType.Assign))
        {
            var value = Expression();
            Consume(TokenType.Semicolon, "Expect ';' after expression.");
            return new Assignment { Variable = new VariableReference { Name = name.Lexeme }, Value = value };
        }
        throw Error(Peek(), "Expect '=' after variable name.");
    }

    private List<Statement> Block()
    {
        var statements = new List<Statement>();

        while (!Check(TokenType.RightBrace) && !IsAtEnd())
        {
            statements.Add(Declaration());
        }

        Consume(TokenType.RightBrace, "Expect '}' after block.");
        return statements;
    }

    private Expression Expression()
    {
        return Equality();
    }

    private Expression Equality()
    {
        var expr = Comparison();
        while (Match(TokenType.Equal, TokenType.NotEqual))
        {
            var operatorToken = Previous();
            var right = Comparison();
            expr = new BinaryExpression { Left = expr, Operator = operatorToken.Lexeme, Right = right };
        }

        return expr;
    }

    private Expression Comparison()
    {
        var expr = Term();

        while (Match(TokenType.Less, TokenType.LessEqual, TokenType.Greater, TokenType.GreaterEqual))
        {
            var operatorToken = Previous();
            var right = Term();
            expr = new BinaryExpression { Left = expr, Operator = operatorToken.Lexeme, Right = right };
        }

        return expr;
    }

    private Expression Term()
    {
        var expr = Factor();

        while (Match(TokenType.Plus, TokenType.Minus))
        {
            var operatorToken = Previous();
            var right = Factor();
            expr = new BinaryExpression { Left = expr, Operator = operatorToken.Lexeme, Right = right };
        }

        return expr;
    }

    private Expression Factor()
    {
        var expr = Unary();

        while (Match(TokenType.Star, TokenType.Slash))
        {
            var operatorToken = Previous();
            var right = Unary();
            expr = new BinaryExpression { Left = expr, Operator = operatorToken.Lexeme, Right = right };
        }

        return expr;
    }

    private Expression Unary()
    {
        if (Match(TokenType.Minus))
        {
            var operatorToken = Previous();
            var right = Unary();
            return new UnaryExpression { Operator = operatorToken.Lexeme, Operand = right };
        }

        return Primary();
    }

    private Expression Primary()
    {
        if (Match(TokenType.False)) return new BoolLiteral { Value = "false" };
        if (Match(TokenType.True)) return new BoolLiteral { Value = "true" };
        if (Match(TokenType.Number)) return new NumberLiteral { Value = Previous().Literal.ToString() };
        if (Match(TokenType.String)) return new StringLiteral { Value = Previous().Literal.ToString() };
        if (Match(TokenType.Identifier)) return new VariableReference { Name = Previous().Lexeme };

        throw Error(Peek(), "Expect expression.");
    }

    private bool Match(params TokenType[] types)
    {
        foreach (var type in types)
        {
            if (Check(type))
            {
                Advance();
                return true;
            }
        }

        return false;
    }

    private Token Consume(TokenType type, string message)
    {
        if (Check(type)) return Advance();
        throw Error(Peek(), message);
    }

    private bool Check(TokenType type)
    {
        if (IsAtEnd()) return false;
        return Peek().Type == type;
    }

    private Token Advance()
    {
        if (!IsAtEnd()) current++;
        return Previous();
    }

    private bool IsAtEnd()
    {
        return Peek().Type == TokenType.EndOfFile;
    }

    private Token Peek()
    {
        return tokens[current];
    }

    private Token Previous()
    {
        return tokens[current - 1];
    }

    private ParseException Error(Token token, string message)
    {
        // Implement error reporting here
        return new ParseException(message);
    }

    private void Synchronize()
    {
        Advance();

        while (!IsAtEnd())
        {
            if (Previous().Type == TokenType.Semicolon) return;

            switch (Peek().Type)
            {
                case TokenType.If:
                case TokenType.While:
                case TokenType.For:
                case TokenType.Card:
                case TokenType.Effect:
                    return;
            }

            Advance();
        }
    }
}

public class ParseException : Exception
{
    public ParseException(string message) : base(message)
    {
    }
}