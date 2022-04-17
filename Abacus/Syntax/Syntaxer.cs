using System;
using System.Collections.Generic;
using Abacus.Tokens;

namespace Abacus.Syntax
{
    public static class Syntaxer
    {
        public static List<IToken> ShuntingYard(List<IToken> tokens)
        {
            Stack<IToken> operatorStack = new ();
            List<IToken> output = new ();

            foreach (IToken token in tokens)
            {
                switch (token)
                {
                    case ATokenValuable:
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
                        while (operatorStack.Count > 0 && operatorStack.Peek() is TokenOperator &&
                               ((TokenOperator)operatorStack.Peek()).Priority > @operator.Priority)
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
                        while (operatorStack.Peek() is not TokenLParenthesis)
                        {
                            output.Add(operatorStack.Pop());
                        }
                        operatorStack.Pop();
                        if (operatorStack.Count > 0 && operatorStack.Peek() is ATokenFunction)
                        {
                            output.Add(operatorStack.Pop());
                        }
                        break;
                    }
                }
            }
            while (operatorStack.Count != 0)
            {
                IToken token = operatorStack.Pop();
                if (token is TokenLParenthesis)
                    throw new InvalidOperationException("Syntax Error: Unbalanced parentheses");
                output.Add(token);
            }

            return output;
        }
    }
}