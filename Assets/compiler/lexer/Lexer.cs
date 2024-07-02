using System.Text;
/*inicio del lexer guiandome por el hulk
la mayoria de los comentarios en el codigo es para enfocarse primero en la construccion del lexer y dejar 
temporalmente el analisis de errores de lado*/
public class Lexer
{
    List<string> AritmeticOperators = new List<string>(new string[]{"+","-","*","/","^","++"});
    List<string> LogicOperators = new List<string>(new string[]{"&&","||"});
    List<string> ComparisonOperators = new List<string>(new string[]{"<",">"});
    List<string> SpecialCharacters = new List<string>(new string[]{"@",";","="});
    //List<string> AsignationOperators = new List<string>(new string[]{"=",":"});
    List<string> SeparatorCharacters = new List<string>(new string[]{",","\"","(",")","{","}"});
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
     public List<TokenInterface> GetTokens() => TokenList;
    private void CreateTokens(string input)
        {
            //StringBuilder preToken = new StringBuilder();
            string code = input;

            foreach (char x in code )
            {
                if (x == ' ')
                {
                    continue;
                }
                if (SeparatorCharacters.Contains(x.ToString()))
                {
                    TokenList.Add(new SeparatorToken(x.ToString()));
                    code.Remove(x);
                    continue;
                }
                else if (AritmeticOperators.Contains(x.ToString()))
                {
                    if (code[code.IndexOf(x)+1]=='=')
                    {                        
                        TokenList.Add( new ComparisonOperatorToken(string.Concat(x,"=")));
                        code.Remove(code.IndexOf(x),2);
                        continue;
                    }
                    TokenList.Add(new ArithmeticOperatorToken(x.ToString()));
                    //code.Remove(x);
                    continue;
                }
                else if (LogicOperators.Contains(x.ToString()))
                {
                    if(code[code.IndexOf(x)+1]==x)
                    {
                        if (x == '&')
                        {
                            TokenList.Add(new LogicBooleanOperatorToken("&&"));
                            code.Remove(x);
                            continue;
                        }
                        else if (x == '|')
                        {
                            TokenList.Add(new LogicBooleanOperatorToken("||"));
                            code.Remove(x);
                            continue;
                        }
                        else code.Remove(x);continue;
                    
                    }
                }
                else if (SpecialCharacters.Contains(x.ToString()))
                {
                    if (x == ';')
                        {TokenList.Add(new EndOfLineToken());
                        code.Remove(x);
                            continue;}
                    else if (x == '@')
                        {   
                            if (code[code.IndexOf(x)+1]=='@')
                            {TokenList.Add(new SpecialOperatorToken("@@"));
                            code.Remove(x);
                            continue;}
                            else TokenList.Add(new SpecialOperatorToken("@"));
                        }
                    else if (x == '=')
                    {
                        if ( code[code.IndexOf(x)+1] == '=')
                        {
                            TokenList.Add(new ComparisonOperatorToken("=="));
                            code.Remove(x);
                            continue;
                        }
                        else
                        {
                            TokenList.Add(new SpecialOperatorToken("="));
                            code.Remove(x);
                            continue;
                        }
                    }
                }
                else if (ComparisonOperators.Contains(x.ToString()))
                {
                    if (code[code.IndexOf(x)+1]!='=') 
                    {
                        TokenList.Add( new ComparisonOperatorToken(x.ToString()));
                        code.Remove(x);
                        continue;
                    }
                    else 
                    {
                        string temp = string.Concat(x,code[code.IndexOf(x)+1]);
                        TokenList.Add( new ComparisonOperatorToken(temp));
                        code.Remove(code.IndexOf(x),2);
                        continue;

                    }

                }
                //en caso de que sea un numero
                else if (char.IsDigit(x))
                {
                    string number = x.ToString();
                    int temp =code.IndexOf(x);
                    while (temp<code.Length-1 && char.IsDigit(code[temp+1]))
                    {
                        number = string.Concat(number,code[code.IndexOf(x)+1]);
                        temp+=1;
                    }
                    TokenList.Add(new NumberToken(Double.Parse(number)));
                    code.Remove(code.IndexOf(x),number.Count());
                    continue;
                }
                
                
                

            }
        }
}