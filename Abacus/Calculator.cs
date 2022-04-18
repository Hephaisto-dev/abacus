using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Abacus.Syntax;
using Abacus.Tokens;
using Abacus.Tokens.Functions;

namespace Abacus
{
    public class Calculator
    {
        private bool _isRpn;

        private const string AllowedArg = "--rpn";
        private readonly string[] _args;

        public Calculator(string[] args)
        {
            this._args = args;
            _isRpn = args.Contains(AllowedArg);
        }

        public int Run(string expression)
        {
            if (!CheckArgs())
                return 1;

            try
            {
                Console.WriteLine(Calculate(expression));
                return 0;
            }
            catch (ArithmeticException exception)
            {
                Console.Error.WriteLine(exception.Message);
                return 3;
            }
            catch (SyntaxErrorException  exception) 
            {
                Console.Error.WriteLine(exception.Message);
                return 2;
            }
            catch (InvalidOperationException exception)
            {
                Console.Error.WriteLine(exception.Message);
                return 2;
            }
        }

        public int Calculate(string expression)
        {
            Lexer lexer = new ();
            List<List<IToken>> tokensExpressions = lexer.Lex(expression, _isRpn);
            
            Stack<ATokenValuable> output = new Stack<ATokenValuable>();
            foreach (List<IToken> tokens in tokensExpressions)
            {
                output.Clear();
                Queue<IToken> currentExpression = new Queue<IToken>(_isRpn ? tokens : Syntaxer.ShuntingYard(tokens));
                while (currentExpression.Count != 0)
                {
                    IToken dequeue = currentExpression.Dequeue();
                    if (dequeue is TokenNumber @tokenNumber)
                    {
                        output.Push(tokenNumber);
                    }
                    else if (dequeue is TokenVariable @tokenVariable)
                    {
                        output.Push(tokenVariable);
                    }
                    else if (dequeue is TokenOperator @tokenOperator)
                    {
                        int rhs = output.Pop().Value;
                        ATokenValuable lhs = output.Peek(); //To avoid reference type creation and push
                        lhs.Value = tokenOperator.Compute(lhs.Value, rhs);
                    }
                    else if (dequeue is ATokenFunction @aTokenFunction)
                    {
                        ATokenValuable buffer = output.Pop(); //To avoid reference type creation 
                        int param = buffer.Value;
                        switch (@aTokenFunction)
                        {
                            case TokenFunction<int> @function1:
                                buffer.Value = function1.Compute(param);
                                break;
                            case TokenFunction<(int, int)> @function2:
                                buffer.Value = function2.Compute((output.Pop().Value, param)); 
                                break;
                        }
                        output.Push(buffer);
                    }
                }
            }

            if (output.Count != 1)
                throw new SyntaxErrorException("Syntax Error: operator expected");
            return output.Pop().Value;
        }

        private bool CheckArgs()
        {
            switch (_args.Length)
            {
                case > 1:
                    Console.Error.WriteLine("Too much args");
                    return false;
                case 1 when !_args[0].Equals(AllowedArg):
                    Console.Error.WriteLine($"Unknown Argument: {_args[0]}");
                    return false;
            }

            return true;
        }
    }
}