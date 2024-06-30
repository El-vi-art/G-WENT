using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Tokenizer
{
    private readonly List<(string Type, string Pattern)> _tokenDefinitions = new List<(string Type, string Pattern)>
    {
        ("KEYWORD", @"\b(if|else|for|while)\b"),
        ("IDENTIFIER", @"[a-zA-Z_][a-zA-Z0-9_]*"),
        ("NUMBER", @"\b\d+\b"),
        ("OPERATOR", @"[+\-*/=]"),
        ("WHITESPACE", @"\s+"),
        ("UNKNOWN", @".")
    };

    public List<(string Type, string Value)> Tokenize(string code)
    {
        var tokens = new List<(string Type, string Value)>();
        int position = 0;

        while (position < code.Length)
        {
            Match match = null;

            foreach (var (type, pattern) in _tokenDefinitions)
            {
                var regex = new Regex(pattern);
                match = regex.Match(code, position);

                if (match.Success)
                {
                    if (type != "WHITESPACE")
                    {
                        tokens.Add((type, match.Value));
                    }
                    position += match.Length;
                    break;
                }
            }

            if (match == null || !match.Success)
            {
                throw new Exception($"Unexpected character: {code[position]}");
            }
        }

        return tokens;
    }
}

public class Program
{
    public static void Main()
    {
        string code = "if x == 10";
        var tokenizer = new Tokenizer();
        var tokens = tokenizer.Tokenize(code);

        foreach (var token in tokens)
        {
            Console.WriteLine($"Type: {token.Type}, Value: {token.Value}");
        }
    }
}