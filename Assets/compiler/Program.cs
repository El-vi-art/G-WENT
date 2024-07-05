static void Main(string[] args)
{
    // Ejemplo de código fuente del DSL
    string source = @"
    effect myEffect;
    card myCard;
    for (var i = 0; i < 10; i = i + 1) {
    // loop body
    }
                while (true) {
                    // loop body
                }
                if (true) {
                    // if body
                } else {
                    // else body
                }
                var x = 5;
            ";

            // Crear el lexer
            Lexer lexer = new Lexer(source);
            List<Token> tokens = lexer.ScanTokens();

            // Crear el parser
            Parser parser = new Parser(tokens);
            Program program = parser.Parse();

            // Crear el visitor
            PrintVisitor printVisitor = new PrintVisitor();

            // Imprimir el AST
            program.Accept(printVisitor);
        }