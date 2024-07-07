public enum TokenType
{
    // Palabras clave
    For, While, If, Else, True, False,
//effects y miembros
    Effect,Name,Params,Action,
                       targets,context,
                               TriggerPlayer,Board,
HandOfPlayer,FieldOfPlayer,GraveyardOfPlayer, DeckOfPlayer,Find,SendBottom,Pop,Remove, Shuffle,
//cartas y miembros,name ya está en los efectos
Card,Type,Faction,Power,Range, OnActivation,
                               PostAction, Selector,
Source, Single,Predicate

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