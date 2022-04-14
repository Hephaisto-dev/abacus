using System.Collections.Generic;
using System.Data;
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

        public List<Token> Lex(string expression, bool isRpn)
        {
            List<Token> tokens = new List<Token>();

            int i = 0;
            while (i < expression.Length)
            {
                char token = expression[i];
                if (Operators.Contains(token))
                {
                    tokens.Add(new TokenOperator(token));
                }
                else switch (token)
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
                        if (char.IsLetter(token))
                        {
                            if (isRpn)
                                throw new SyntaxErrorException("No function in RPN");
                            i = AddFunction(expression, i, tokens);
                        }
                        else
                        {
                            i = AddNumber(expression, i, tokens);
                        }
                        tokens.Add(new TokenEmpty());
                        break;
                    }
                }
                i++;
            }

            tokens.RemoveAll(token => token is TokenEmpty);
            
            return tokens;
        }

        private int AddFunction(string expression, int i, List<Token> tokens)
        {
            string word = expression[i].ToString();
            i++;
            while (i < expression.Length && !Operators.Contains(expression[i]))
            {
                word += expression[i];
                i++;
            }
            ATokenFunction aTokenFunction = _functionManager.Functions.Find(function => function.CanonicalName.Equals(word));
            if (aTokenFunction != null)
                tokens.Add(aTokenFunction);
            return i;
        }

        private static int AddNumber(string expression, int i, List<Token> tokens)
        {
            int currentNumber = (int)char.GetNumericValue(expression[i]);
            int number = currentNumber == -1 ? -1 : 0;
            while (currentNumber != -1 && i + 1 < expression.Length)
            {
                number = number * 10 + currentNumber;
                i++;
                currentNumber = (int)char.GetNumericValue(expression[i]);
            }
            if (number >= 0)
                tokens.Add(new TokenNumber(number));
            return i;
        }

        private static string Operators => "∗÷/+-%^*";
    }
}