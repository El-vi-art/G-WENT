using System.Text;
/*inicio del lexer guiandome por el hulk
la mayoria de los comentarios en el codigo es para enfocarse primero en la construccion del lexer y dejar 
temporalmente el analisis de errores de lado*/
public class Lexer
{
    List<string> AritmeticOperators = new List<string>(new string[]{"+","-","*","/","^","++"});
    List<string> LogicOperators = new List<string>(new string[]{"&&","||"});
    List<string> ComparisonOperators = new List<string>(new string[]{"<",">","==","<=",">="});
    List<string> ConcatOperators = new List<string>(new string[]{"@","@@"});
    List<string> AsignationOperators = new List<string>(new string[]{"=",":"});
    List<string> SeparatorCharacters = new List<string>(new string[]{",",";","\"","(",")","{","}"});
    // Access operator {"."}

    List<string> KeywordCharacters = new List<string>(new string[]{"effect","Name","Params","Action",
    "card","Type","Faction","Power","Range","OnActivation"});

    List<TokenInterface> TokenList;
    //List<CompilingError> ErrorList;

    public Lexer(string input)
    {
            TokenList = new List<TokenInterface>();
            //ErrorList = new List<CompilingError>();
            CreateTokens(input);

    }
    private void CreateTokens(string input)
        {
            StringBuilder preToken = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (SeparatorCharacters.Contains(input[i]))
                {
                    TokenList.Add(new SeparatorToken(input[i].ToString()));
                }
                else if (AritmeticOperators.Contains(input[i]))
                {
                    if (i + 1 != input.Length && input[i] == '=' && input[i + 1] == '>')
                    {
                        TokenList.Add(new OperatorToken("=>"));
                        i++;
                    }
                    else
                        TokenList.Add(new ArithmeticOperatorToken(input[i].ToString()));
                }
                else if (LogicOperators.Contains(input[i]))
                {
                    if (input[i] == '&' || input[i] == '|')
                        TokenList.Add(new LogicBooleanOperatorToken(input[i].ToString()));
                    else if (input[i] == '!' && (i == input.Length || input[i + 1] != '='))
                        TokenList.Add(new LogicBooleanOperatorToken(input[i].ToString()));
                    else if (i + 1 != input.Length && input[i + 1] == '=')
                    {
                        TokenList.Add(new LogicArimeticOperatorToken(input[i].ToString() + "="));
                        i++;
                    }
                    else
                        TokenList.Add(new LogicArimeticOperatorToken(input[i].ToString()));
                }
                else if (SpecialCharacters.Contains(input[i]))
                {
                    if (input[i] == ';')
                        TokenList.Add(new EndOfLineToken());
                    else if (input[i] == '@')
                        TokenList.Add(new SpecialOperatorToken("@"));
                    else if (input[i] == '=')
                    {
                        if (i + 1 != input.Length && input[i + 1] == '=')
                        {
                            TokenList.Add(new LogicArimeticOperatorToken("=="));
                            i++;
                        }
                        else if (i + 1 != input.Length && input[i + 1] == '>')
                        {
                            TokenList.Add(new SpecialOperatorToken("=>"));
                            i++;
                        }
                        else
                            TokenList.Add(new SpecialOperatorToken("="));
                    }
                }
                else if (char.IsDigit(input[i]))
                {
                    if (TokenList.Count >= 1)
                    {
                        if (TokenList[TokenList.Count - 1] is ArithmeticOperatorToken && ((ArithmeticOperatorToken)TokenList[TokenList.Count - 1]).TokenValue == "-")
                        {
                            preToken.Append("-");

                            if (TokenList.Count >= 2 && (TokenList[TokenList.Count - 2] is NumberToken or IdentifierToken || TokenList[TokenList.Count - 2].GetTokenValueAsString() == ")"))
                                ((ArithmeticOperatorToken)TokenList[TokenList.Count - 1]).ChangeValue("+");
                            else
                                TokenList.RemoveAt(TokenList.Count - 1);
                        }
                    }

                    while (i != input.Length && input[i] != ' ')
                    {
                        if (SeparatorCharacters.Contains(input[i]) || AritmeticOperators.Contains(input[i]) || LogicOperators.Contains(input[i]) || SpecialCharacters.Contains(input[i]))
                            break;

                        preToken.Append(input[i]);
                        i++;
                    }

                    double number;
                    if (double.TryParse(preToken.ToString(), out number))
                        TokenList.Add(new NumberToken(number));
                    //else
                        //AddLexicalErrorToList($"Invalid token {number}");

                    preToken.Clear();
                    i--;
                }
                else if (input[i] == '"')
                {
                    preToken.Append(input[i]);
                    i++;

                    while (true)
                    {
                        if (i == input.Length)
                            break;

                        if (input[i] == '"')
                        {
                            preToken.Append(input[i]);
                            break;
                        }

                        if (input[i] == '\\' && i < input.Length - 1)
                        {
                            switch (input[i + 1])
                            {
                                case '\"':
                                    preToken.Append("\"");
                                    i += 1;
                                    break;
                                case 'n':
                                    preToken.Append("\n");
                                    i += 1;
                                    break;
                                case 't':
                                    preToken.Append("\t");
                                    i += 1;
                                    break;
                                default:
                                    preToken.Append(input[i]);
                                    break;
                            }
                        }
                        else
                        {
                            preToken.Append(input[i]);
                        }

                        i++;
                    }

                    /*if (preToken[preToken.Length - 1] != '\"')
                        AddLexicalErrorToList($"Invalid token {preToken}. Expected double-quotes `\"`");
                    else
                        TokenList.Add(new StringToken(preToken.ToString()));*/

                    preToken.Clear();

                }
                else if (input[i] == ' ')
                {
                    continue;
                }
                else
                {
                    bool invalidToken = false;
                    while (i != input.Length && input[i] != ' ')
                    {
                        if (SeparatorCharacters.Contains(input[i]) || AritmeticOperators.Contains(input[i]) || LogicOperators.Contains(input[i]) || SpecialCharacters.Contains(input[i]))
                            break;

                        if (!char.IsLetter(input[i]))
                            invalidToken = true;

                        preToken.Append(input[i]);
                        i++;
                    }

                    string preTokenSTR = preToken.ToString();

                    /*if (invalidToken)
                        AddLexicalErrorToList($"Invalid token {preTokenSTR}");
                    else*/ if (KeywordCharacters.Contains(preToken.ToString()))
                        TokenList.Add(new KeywordToken(preTokenSTR));
                    else if (preTokenSTR == "true" || preTokenSTR == "false")
                        TokenList.Add(new BooleanToken(bool.Parse(preTokenSTR)));
                    else if (preTokenSTR == "PI")
                        TokenList.Add(new NumberToken(Math.PI));
                    else if (preTokenSTR == "E")
                        TokenList.Add(new NumberToken(Math.E));
                    else
                        TokenList.Add(new IdentifierToken(preTokenSTR));

                    preToken.Clear();
                    i--;
                }

            }
        }
}