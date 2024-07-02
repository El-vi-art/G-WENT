using System;
using System.Collections.Generic;

public class Parser
{
    private readonly List<TokenInterface> _tokens;
    private int _position = 0;

    public Parser(List<TokenInterface> tokens)
    {
        _tokens = tokens;
    }

    public ProgramNode Parse()
    {
        var program = new ProgramNode();

        while (!IsAtEnd())
        {
            program.Statements.Add(ParseStatement());
        }

        return program;
    }

    private ASTNode ParseStatement()
    {
        if (Match("KEYWORD", "if"))
        {
            return ParseIfStatement();
        }

        // Agregar otras declaraciones aquí (por ejemplo, while, for, etc.)
        throw new Exception("Unknown statement");
    }

    private IfStatementNode ParseIfStatement()
    {
        var ifStatement = new IfStatementNode();

        ifStatement.Condition = ParseExpression();
        ifStatement.ThenBranch = ParseStatement();

        if (Match("KEYWORD", "else"))
        {
            ifStatement.ElseBranch = ParseStatement();
        }

        return ifStatement;
    }

    private ASTNode ParseExpression()
    {
        return ParseEquality();
    }

    private ASTNode ParseEquality()
    {
        var node = ParseComparison();

        while (Match("OPERATOR", "=="))
        {
            var operatorToken = Previous();
            var right = ParseComparison();
            node = new BinaryExpressionNode { Left = node, Operator = operatorToken.GetType().ToString(), Right = right };
        }

        return node;
    }

    private ASTNode ParseComparison()
    {
        var node = ParseTerm();

        while (Match("OPERATOR", "<", ">"))
        {
            var operatorToken = Previous();
            var right = ParseTerm();
            node = new BinaryExpressionNode { Left = node, Operator = operatorToken.GetType().ToString(), Right = right };
        }

        return node;
    }

    private ASTNode ParseTerm()
    {
        var node = ParseFactor();

        while (Match("OPERATOR", "+", "-"))
        {
            var operatorToken = Previous();
            var right = ParseFactor();
            node = new BinaryExpressionNode { Left = node, Operator = operatorToken.GetType().ToString(), Right = right };
        }

        return node;
    }

    private ASTNode ParseFactor()
    {
        var node = ParseUnary();

        while (Match("OPERATOR", "*", "/"))
        {
            var operatorToken = Previous();
            var right = ParseUnary();
            node = new BinaryExpressionNode { Left = node, Operator = operatorToken.GetType().ToString(), Right = right };
        }

        return node;
    }

    private ASTNode ParseUnary()
    {
        if (Match("OPERATOR", "-"))
        {
            var operatorToken = Previous();
            var right = ParseUnary();
            return new BinaryExpressionNode { Operator = operatorToken.GetType().ToString(), Right = right };
        }

        return ParsePrimary();
    }

    private ASTNode ParsePrimary()
    {
        if (Match("NUMBER"))
        {
            return new LiteralNode { Value = Previous().GetType().ToString()};
        }

        if (Match("IDENTIFIER"))
        {
            return new VariableNode { Name = Previous().GetType().ToString() };
        }

        throw new Exception("Expected expression");
    }

    private bool Match(string type, params string[] values)
    {
        if (Check(type, values))
        {
            _position++;
            return true;
        }

        return false;
    }

    private bool Check(string type, params string[] values)
    {
        if (IsAtEnd()) return false;

        var token = _tokens[_position];
        if (token.GetType().ToString() != type) return false;

        if (values.Length == 0) return true;

        foreach (var value in values)
        {
            if (token.GetTokenValueAsString() == value) return true;
        }

        return false;
    }

    private TokenInterface Previous()
    {
        return _tokens[_position - 1];
    }

    private bool IsAtEnd()
    {
        return _position >= _tokens.Count;
    }
}