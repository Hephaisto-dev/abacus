using System;
using System.Collections.Generic;
using System.Data;
using Abacus.Syntax;
using Abacus.Tokens;

namespace Abacus
{
    public class Calculator
    {
        private bool _isRpn;

        private const string AllowedArg = "--rpn";
        private string[] args;

        public Calculator(string[] args)
        {
            this.args = args;
        }

        public int Run()
        {
            /*if (!CheckArgs())
                return 1;*/

            try
            {
                //string expression = Console.In.ReadToEnd();
                string expression = "2 + 3";
                Lexer lexer = new Lexer();
                List<Token> tokens = lexer.Lex(expression, _isRpn);
                
                if (!_isRpn)
                    tokens = Syntaxer.ShuntingYard(tokens);

                Queue<Token> input = new Queue<Token>(tokens);
                Stack<int> output = new Stack<int>();
                while (input.Count != 0)
                {
                    Token dequeue = input.Dequeue();
                    if (dequeue is TokenNumber @tokenNumber)
                    {
                        output.Push(tokenNumber.Value);
                    }
                    else if (dequeue is TokenOperator @tokenOperator)
                    {
                        int lhs = output.Pop();
                        int rhs = output.Pop();
                        output.Push(tokenOperator.Compute(lhs,rhs));
                    }
                    else if (dequeue is ATokenFunction)
                    {
                        //TODO
                    }
                }

                Console.WriteLine(output.Peek());
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

        private bool CheckArgs()
        {
            switch (args.Length)
            {
                case > 1:
                    Console.Error.WriteLine("Too much args");
                    return false;
                case 1 when !args[0].Equals(AllowedArg):
                    Console.Error.WriteLine("Unknown Argument: " + args[0]);
                    return false;
                case 1:
                    _isRpn = true;
                    break;
            }

            return true;
        }
    }
}