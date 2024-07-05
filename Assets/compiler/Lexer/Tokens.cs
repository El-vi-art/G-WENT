public enum TokenType
{
    // Palabras clave
    Effect, Card, For, While, If, Else, True, False,
    // Símbolos
    Plus, Minus, Star, Slash, Equal,NotEqual, BangEqual, Greater, GreaterEqual, Less, LessEqual, Assign,
    And, Or, Bang, 
    // Tipos de datos
    Number, String, Bool,
    // Otros
    Identifier, EOF,
    // Separadores
    LeftParen, RightParen, LeftBrace, RightBrace, Comma, Dot, Semicolon,
    //specials
    EndOfFile
}

public class Token
{
    public TokenType Type { get; }
    public string Lexeme { get; }
    public object Literal { get; }
    public int Line { get; }

    public Token(TokenType type, string lexeme, object literal, int line)
    {
        Type = type;
        Lexeme = lexeme;
        Literal = literal;
        Line = line;
    }

    public override string ToString()
    {
        return $"{Type} {Lexeme} {Literal}";
    }
}