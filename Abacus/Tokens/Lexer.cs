using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<List<IToken>> Lex(string expression, bool isRpn)
        {
            List<List<IToken>> tokensExpressions = new();
            string[] split = expression.Split(';', StringSplitOptions.TrimEntries);
            List<TokenVariable> variables = new List<TokenVariable>();

            foreach (string subExpression in split)
            {
                List<IToken> tokens = new List<IToken>();
                int i = 0;
                int currentSign = 1;
                while (i < subExpression.Length)
                {
                    char token = subExpression[i];
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
                                    if (!isRpn && tokens.Count > 0 && tokens.Last() is ATokenValuable)
                                        tokens.Add(new TokenOperator('*')); // implicit multiplication
                                    tokens.Add(new TokenLParenthesis());
                                    break;
                                case ')':
                                    tokens.Add(new TokenRParenthesis());
                                    break;
                                default:
                                {
                                    if (char.IsLetter(token) || token == '_')
                                    {
                                        string word = ParseWord(subExpression, ref i);
                                        ATokenFunction aTokenFunction =
                                            _functionManager.Functions.Find(function =>
                                                function.CanonicalName.Equals(word));
                                        if (aTokenFunction != null)
                                            tokens.Add(aTokenFunction);
                                        else
                                        {
                                            if (!isRpn && tokens.Count > 0 && tokens.Last() is ATokenValuable)
                                                tokens.Add(new TokenOperator('*')); // implicit multiplication

                                            TokenVariable variable = variables.Find(tokenVariable =>
                                                tokenVariable.Name == word);
                                            if (variable == null)
                                            {
                                                TokenVariable tokenVariable = new TokenVariable(0, word);
                                                tokens.Add(tokenVariable);
                                                variables.Add(tokenVariable);
                                            }
                                            else
                                                tokens.Add(variable);
                                        }
                                    }
                                    else if (char.IsNumber(token))
                                    {
                                        i = AddNumber(subExpression, i, tokens, isRpn ? 1 : currentSign);
                                        currentSign = 1;
                                    }

                                    break;
                                }
                            }
                    }

                    i++;
                }
                
                if (tokens.Count > 0)
                    tokensExpressions.Add(tokens);
            }

            return tokensExpressions;
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