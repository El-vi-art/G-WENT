public class Program
{
    public static void Main()
    {
        string code = Console.ReadLine();
        Lexer tokens =  new Lexer(code);
        //var tokens = tokenizer.Tokenize(code);

        var parser = new Parser(tokens.GetTokens());
        var ast = parser.Parse();

        PrintAST(ast);
    }

    private static void PrintAST(ASTNode node, string indent = "")
    {
        if (node is ProgramNode programNode)
        {
            Console.WriteLine($"{indent}Program:");
            foreach (var statement in programNode.Statements)
            {
                PrintAST(statement, indent + "  ");
            }
        }
        else if (node is IfStatementNode ifNode)
        {
            Console.WriteLine($"{indent}IfStatement:");
            Console.WriteLine($"{indent}  Condition:");
            PrintAST(ifNode.Condition, indent + "    ");
            Console.WriteLine($"{indent}  ThenBranch:");
            PrintAST(ifNode.ThenBranch, indent + "    ");
            if (ifNode.ElseBranch != null)
            {
                Console.WriteLine($"{indent}  ElseBranch:");
                PrintAST(ifNode.ElseBranch, indent + "    ");
            }
        }
        else if (node is BinaryExpressionNode binaryNode)
        {
            Console.WriteLine($"{indent}BinaryExpression:");
            Console.WriteLine($"{indent}  Left:");
            PrintAST(binaryNode.Left, indent + "    ");
            Console.WriteLine($"{indent}  Operator: {binaryNode.Operator}");
            Console.WriteLine($"{indent}  Right:");
            PrintAST(binaryNode.Right, indent + "    ");
        }
        else if (node is LiteralNode literalNode)
        {
            Console.WriteLine($"{indent}Literal: {literalNode.Value}");
        }
        else if (node is VariableNode variableNode)
        {
            Console.WriteLine($"{indent}Variable: {variableNode.Name}");
        }
        // Agregar otros nodos si es necesario
    }
}