// Interfaz del visitante

public abstract class AstNode
{
    public string Value { get; set; }
    public TokenType Type { get; set; }

    public abstract IEnumerable<AstNode> GetChildren();
    public abstract void Accept(IVisitor visitor);
}

// Nodo base para expresiones
public abstract class Expression : AstNode { }

// Nodo base para declaraciones
public abstract class Statement : AstNode { }

// Literales
public class NumberLiteral : Expression
{
    public double NumberValue { get; set; }

    public override IEnumerable<AstNode> GetChildren() => new List<AstNode>();
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

public class StringLiteral : Expression
{
    public string StringValue { get; set; }

    public override IEnumerable<AstNode> GetChildren() => new List<AstNode>();
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

public class BoolLiteral : Expression
{
    public bool BoolValue { get; set; }

    public override IEnumerable<AstNode> GetChildren() => new List<AstNode>();
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

// Expresiones
public class BinaryExpression : Expression
{
    public Expression Left { get; set; }
    public Expression Right { get; set; }
    public string Operator { get; set; }

    public override IEnumerable<AstNode> GetChildren()
    {
        yield return Left;
        yield return Right;
    }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

public class UnaryExpression : Expression
{
    public Expression Operand { get; set; }
    public string Operator { get; set; }

    public override IEnumerable<AstNode> GetChildren() => new List<AstNode> { Operand };
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

public class VariableReference : Expression
{
    public string Name { get; set; }

    public override IEnumerable<AstNode> GetChildren() => new List<AstNode>();
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

public class PropertyAccess : Expression
{
    public Expression Object { get; set; }
    public string PropertyName { get; set; }

    public override IEnumerable<AstNode> GetChildren() => new List<AstNode> { Object };
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

public class IndexAccess : Expression
{
    public Expression Object { get; set; }
    public Expression Index { get; set; }

    public override IEnumerable<AstNode> GetChildren() => new List<AstNode> { Object, Index };
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

// Declaraciones y Sentencias
public class Assignment : Statement
{
    public VariableReference Variable { get; set; }
    public Expression Value { get; set; }

    public override IEnumerable<AstNode> GetChildren()
    {
        yield return Variable;
        yield return Value;
    }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

public class EffectStatement : Statement
{
    public string Effect { get; set; }

    public override IEnumerable<AstNode> GetChildren() => new List<AstNode>();
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

public class CardStatement : Statement
{
    public string Card { get; set; }

    public override IEnumerable<AstNode> GetChildren() => new List<AstNode>();
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

// Control de Flujo
public class ForStatement : Statement
{
    public Statement Initialization { get; set; }
    public Expression Condition { get; set; }
    public Statement Increment { get; set; }
    public Statement Body { get; set; }

    public override IEnumerable<AstNode> GetChildren()
    {
        yield return Initialization;
        yield return Condition;
        yield return Increment;
        yield return Body;
    }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

public class WhileStatement : Statement
{
    public Expression Condition { get; set; }
    public Statement Body { get; set; }

    public override IEnumerable<AstNode> GetChildren()
    {
        yield return Condition;
        yield return Body;
    }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

public class IfStatement : Statement
{
    public Expression Condition { get; set; }
    public Statement ThenBranch { get; set; }
    public Statement ElseBranch { get; set; }

    public override IEnumerable<AstNode> GetChildren()
    {
        yield return Condition;
        yield return ThenBranch;
        if (ElseBranch != null) yield return ElseBranch;
    }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

// Bloques y Programas
public class Block : Statement
{
    public List<Statement> Statements { get; set; } = new List<Statement>();

    public override IEnumerable<AstNode> GetChildren() => Statements;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

public partial class Program : AstNode
{
    public Block? MainBlock { get; set; }

    public override IEnumerable<AstNode> GetChildren() => [MainBlock];
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}