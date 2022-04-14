using System;
using System.Collections.Generic;
using Abacus.Tokens;
using Abacus.Tokens.Functions;

namespace Abacus.Syntax
{
    public class Syntaxer
    {
        public static List<Token> ShuntingYard(List<Token> tokens)
        {
            Stack<Token> operatorStack = new Stack<Token>();
            List<Token> output = new List<Token>();

            foreach (Token token in tokens)
            {
                switch (token)
                {
                    case TokenNumber:
                        output.Add(token);
                        break;
                    case ATokenFunction:
                        operatorStack.Push(token);
                        break;
                    case TokenSeparator:
                    {
                        while (operatorStack.Peek() is not TokenLParenthesis)
                        {
                            output.Add(operatorStack.Pop());
                        }

                        break;
                    }
                    case TokenOperator @operator:
                    {
                        while (operatorStack.Peek() is TokenOperator &&
                               ((TokenOperator)operatorStack.Peek()).GetPriority() > @operator.GetPriority())
                        {
                            output.Add(operatorStack.Pop());
                        }
                        operatorStack.Push(@operator);
                        break;
                    }
                    case TokenLParenthesis:
                        operatorStack.Push(token);
                        break;
                    case TokenRParenthesis:
                    {
                        operatorStack.Pop();
                        while (operatorStack.Peek() is not TokenLParenthesis)
                        {
                            output.Add(operatorStack.Pop());
                        }
                        operatorStack.Pop();
                        if (operatorStack.Peek() is ATokenFunction)
                        {
                            output.Add(operatorStack.Pop());
                        }
                        break;
                    }
                }
            }
            while (operatorStack.Count != 0)
            {
                Token token = operatorStack.Pop();
                if (token is TokenLParenthesis)
                    throw new InvalidOperationException("Syntax Error: Unbalanced parentheses");
                output.Add(token);
            }

            return output;
        }
    }
}