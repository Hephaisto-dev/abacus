using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Abacus.Tokens
{
    public class Lexer
    {
        public static List<Token> Lex(string expression)
        {
            List<Token> tokens = new List<Token>();
            foreach (char token in expression)
            {
                if (Operators.Contains(token))
                {
                    tokens.Add(new TokenOperator(token));
                    continue;
                }

                int numericValue = (int)char.GetNumericValue(token);
                if (numericValue != -1)
                    tokens.Add(new TokenNumber(token));
            }

            return tokens;
        }

        private static string Operators => "∗÷/+-%^*";
    }
}