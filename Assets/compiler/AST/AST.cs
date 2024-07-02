using System;
using System.Collections.Generic;
using System.Xml;

public abstract class ASTNode
{
    public TokenType Type {get; protected set;}
    public string value { get;set;}
    protected ASTNode(TokenType type)
    {
        Type =type;
    }
    public abstract IEnumerable<ASTNode> GetChildren();
    public abstract void Accept(IVisitor visitor);
}

public class ProgramNode : ASTNode
{
    public List<ASTNode> Statements { get; set; } = new List<ASTNode>();
    public override IEnumerable<ASTNode> GetChildren()
    {
        return Statements;
    }
    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class IfStatementNode : ASTNode
{
    public ASTNode Condition { get; set; }
    public ASTNode ThenBranch { get; set; }
    public ASTNode ElseBranch { get; set; }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class BinaryExpressionNode : ASTNode
{
    public ASTNode Left { get; set; }
    public string Operator { get; set; }
    public ASTNode Right { get; set; }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class LiteralNode : ASTNode
{
    public string Value { get; set; }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class VariableNode : ASTNode
{
    public string Name { get; set; }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public interface IVisitor
{
    void Visit(ProgramNode node);
    void Visit(IfStatementNode node);
    void Visit(BinaryExpressionNode node);
    void Visit(LiteralNode node);
    void Visit(VariableNode node);
}