using System;
using System.Collections.Generic;
using Abacus.SyntaxTree;
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
            if (!ParseArgs())
                return 1;

            string expression = Console.In.ReadToEnd();
            List<Token> tokens = Lexer.Lex(expression);
            
            Syntaxer syntaxer = new Syntaxer(tokens);
            TokenTree tokenTree = isRpn ? syntaxer.TranslateFromRpn() : syntaxer.TranslateFromNormal();
            
            
            return 0;
        }

        private bool ParseArgs()
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