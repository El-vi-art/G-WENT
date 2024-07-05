public interface IVisitor
{
    void Visit(NumberLiteral numberLiteral);
    void Visit(StringLiteral stringLiteral);
    void Visit(BoolLiteral boolLiteral);
    void Visit(BinaryExpression binaryExpression);
    void Visit(UnaryExpression unaryExpression);
    void Visit(VariableReference variableReference);
    void Visit(PropertyAccess propertyAccess);
    void Visit(IndexAccess indexAccess);
    void Visit(Assignment assignment);
    void Visit(EffectStatement effectStatement);
    void Visit(CardStatement cardStatement);
    void Visit(ForStatement forStatement);
    void Visit(WhileStatement whileStatement);
    void Visit(IfStatement ifStatement);
    void Visit(Block block);
    void Visit(Program program);
}

public class PrintVisitor : IVisitor
{
    public void Visit(NumberLiteral numberLiteral)
    {
        Console.WriteLine($"Number: {numberLiteral.Value}");
    }

    public void Visit(StringLiteral stringLiteral)
    {
        Console.WriteLine($"String: {stringLiteral.Value}");
    }

    public void Visit(BoolLiteral boolLiteral)
    {
        Console.WriteLine($"Bool: {boolLiteral.Value}");
    }

    public void Visit(BinaryExpression binaryExpression)
    {
        Console.WriteLine($"Binary Expression: {binaryExpression.Operator}");
        binaryExpression.Left.Accept(this);
        binaryExpression.Right.Accept(this);
    }

    public void Visit(UnaryExpression unaryExpression)
    {
        Console.WriteLine($"Unary Expression: {unaryExpression.Operator}");
        unaryExpression.Operand.Accept(this);
    }

    public void Visit(VariableReference variableReference)
    {
        Console.WriteLine($"Variable: {variableReference.Name}");
    }

    public void Visit(PropertyAccess propertyAccess)
    {
        Console.WriteLine($"Property Access: {propertyAccess.PropertyName}");
        propertyAccess.Object.Accept(this);
    }

    public void Visit(IndexAccess indexAccess)
    {
        Console.WriteLine("Index Access");
        indexAccess.Object.Accept(this);
        indexAccess.Index.Accept(this);
    }

    public void Visit(Assignment assignment)
    {
        Console.WriteLine("Assignment");
        assignment.Variable.Accept(this);
        assignment.Value.Accept(this);
    }

    public void Visit(EffectStatement effectStatement)
    {
        Console.WriteLine($"Effect Statement: {effectStatement.Effect}");
    }

    public void Visit(CardStatement cardStatement)
    {
        Console.WriteLine($"Card Statement: {cardStatement.Card}");
    }

    public void Visit(ForStatement forStatement)
    {
        Console.WriteLine("For Statement");
        forStatement.Initialization.Accept(this);
        forStatement.Condition.Accept(this);
        forStatement.Increment.Accept(this);
        forStatement.Body.Accept(this);
    }

    public void Visit(WhileStatement whileStatement)
    {
        Console.WriteLine("While Statement");
        whileStatement.Condition.Accept(this);
        whileStatement.Body.Accept(this);
    }

    public void Visit(IfStatement ifStatement)
    {
        Console.WriteLine("If Statement");
        ifStatement.Condition.Accept(this);
        ifStatement.ThenBranch.Accept(this);
        if (ifStatement.ElseBranch != null)
        {
            ifStatement.ElseBranch.Accept(this);
        }
    }

    public void Visit(Block block)
    {
        Console.WriteLine("Block");
        foreach (var statement in block.Statements)
        {
            statement.Accept(this);
        }
    }

    public void Visit(Program program)
    {
        Console.WriteLine("Program");
        program.MainBlock.Accept(this);
    }
}