using System;
using System.Collections.Generic;
using System.Linq;
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
                {
                    if (tokens.Count > 0 && tokens.Last() is TokenNumber)
                    {
                        TokenNumber last = (TokenNumber)tokens.Last();
                        last.Concat(numericValue);
                    }
                    else
                        tokens.Add(new TokenNumber(numericValue));
                }
                else 
                    tokens.Add(new TokenEmpty());
            }

            tokens.RemoveAll(token => token is TokenEmpty);
            
            return tokens;
        }

        private static string Operators => "∗÷/+-%^*";
    }
}