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
            catch (ArithmeticException exception)  //TODO check for errors
            {
                Console.Error.WriteLine(exception.Message);
                return 3;
            }
            catch (SyntaxErrorException  exception)  //TODO check for errors
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
            List<IToken> tokens = lexer.Lex(expression, _isRpn);

            if (!_isRpn)
                tokens = Syntaxer.ShuntingYard(tokens);

            
            
            Queue<IToken> input = new Queue<IToken>(tokens);
            Stack<int> output = new Stack<int>();
            while (input.Count != 0)
            {
                IToken dequeue = input.Dequeue();
                if (dequeue is TokenNumber @tokenNumber)
                {
                    output.Push(tokenNumber.Value);
                }
                else if (dequeue is TokenOperator @tokenOperator)
                {
                    int rhs = output.Pop();
                    int lhs = output.Pop();
                    output.Push(tokenOperator.Compute(lhs,rhs));
                }
                else if (dequeue is ATokenFunction @atokenFunction)
                {
                    int param = output.Pop();
                    switch (atokenFunction)
                    {
                        case TokenFunction<int> @function1:
                            output.Push(function1.Compute(param));
                            break;
                        case TokenFunction<(int, int)> @function2:
                            output.Push(function2.Compute((output.Pop(),param)));
                            break;
                    }
                }
            }

            if (output.Count != 1)
                throw new SyntaxErrorException("Syntax Error: operator expected");
            return output.Pop();
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