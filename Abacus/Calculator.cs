using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Abacus.Syntax;
using Abacus.Tokens;

namespace Abacus
{
    public class Calculator
    {
        private bool isRpn;

        private const string AllowedArg = "--rpn";
        private string[] args;

        public Calculator(string[] args)
        {
            this.args = args;
        }

        public int Run()
        {
            if (!CheckArgs())
                return 1;

            string expression = Console.In.ReadToEnd();
            Lexer lexer = new Lexer();
            List<Token> tokens = new List<Token>();
            try
            {
                tokens = lexer.Lex(expression, isRpn); //TODO check for errors
            }
            catch (SyntaxErrorException exception)
            {
                Console.Error.WriteLine(exception.Message);
                return 2;
            }

            if (!isRpn)
            {
                try
                {
                    tokens = Syntaxer.ShuntingYard(tokens); //TODO check for errors
                }
                catch (InvalidOperationException exception)
                {
                    Console.Error.WriteLine(exception.Message);
                    return 2;
                }
            }

            Stack<Token> output = new Stack<Token>();
            return 0;
        }

        private bool CheckArgs()
        {
            if (args.Length > 1)
            {
                Console.Error.WriteLine("Too much args");
                return false;
            }
            if (args.Length == 1)
            {
                if (!args[0].Equals(AllowedArg))
                {
                    Console.Error.WriteLine("Unknown Argument: " + args[0]);
                    return false;
                }
                isRpn = true;
            }

            return true;
        }
    }
}