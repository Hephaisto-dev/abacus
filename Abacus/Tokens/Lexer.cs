using System.Collections.Generic;
using Abacus.Tokens.Functions;

namespace Abacus.Tokens
{
    public class Lexer
    {
        private readonly FunctionManager _functionManager;

        public Lexer()
        {
            _functionManager = new FunctionManager();
        }

        public List<IToken> Lex(string expression, bool isRpn)
        {
            List<IToken> tokens = new List<IToken>();

            int i = 0;
            int currentSign = 1;
            while (i < expression.Length)
            {
                char token = expression[i];
                if (token != ' ')
                {
                    if (Operators.Contains(token))
                    {
                        if (!isRpn && token == '-' && (tokens.Count == 0 || tokens.Count >= 2 &&
                            tokens[^1] is TokenOperator && tokens[^2] is TokenNumber))
                            currentSign = currentSign == 1 ? -1 : 1;
                        else
                        {
                            tokens.Add(new TokenOperator(token));
                        }
                    }
                    else
                        switch (token)
                        {
                            case ',':
                                tokens.Add(new TokenSeparator());
                                break;
                            case '(':
                                tokens.Add(new TokenLParenthesis());
                                break;
                            case ')':
                                tokens.Add(new TokenRParenthesis());
                                break;
                            default:
                            {
                                if (char.IsLetter(token) || token == '_')
                                {
                                    string word = ParseWord(expression, ref i);
                                    ATokenFunction aTokenFunction =
                                        _functionManager.Functions.Find(function =>
                                            function.CanonicalName.Equals(word));
                                    if (aTokenFunction != null)
                                        tokens.Add(aTokenFunction);
                                    else
                                    {
                                        TokenVariable variable = (TokenVariable) tokens
                                            .Find(token1 =>
                                                token1 is TokenVariable @tokenVariable && tokenVariable.Name == word);
                                        if (variable == null)
                                            tokens.Add(new TokenVariable(0, word));
                                        else 
                                            tokens.Add(variable);
                                    }
                                }
                                else if (char.IsNumber(token))
                                {
                                    i = AddNumber(expression, i, tokens, isRpn ? 1 : currentSign);
                                    currentSign = 1;
                                }

                                break;
                            }
                        }
                }

                i++;
            }

            return tokens;
        }

        private string ParseWord(string expression, ref int i)
        {
            string word = expression[i].ToString();
            i++;
            while (i < expression.Length && !Operators.Contains(expression[i]) && expression[i] != ' ')
            {
                word += expression[i];
                i++;
            }

            i--;

            return word;
        }

        private static int AddNumber(string expression, int i, List<IToken> tokens, int sign)
        {
            int currentNumber = (int) char.GetNumericValue(expression[i]);
            int number = currentNumber;
            i++;
            while (i < expression.Length && char.IsNumber(expression[i]))
            {
                currentNumber = (int) char.GetNumericValue(expression[i]);
                number = number * 10 + currentNumber;
                i++;
            }

            tokens.Add(new TokenNumber(number * sign));
            return i - 1;
        }

        private static string Operators => "∗÷/+-%^*=";
    }
}