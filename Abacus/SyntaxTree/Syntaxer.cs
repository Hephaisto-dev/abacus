using System;
using System.Collections.Generic;
using Abacus.Tokens;

namespace Abacus.SyntaxTree
{
    public class Syntaxer
    {
        private List<Token> tokens;

        public Syntaxer(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public CalculatorTree TranslateFromRpn()
        {
            if (tokens.Count > 0)
                return null;
            
            CalculatorTree tree = new CalculatorTree(tokens[0]);
            foreach (Token token in tokens)
            {
                if (token is TokenNumber)
                {
                    if (tree.Key == null)
                    {
                        tree.Key = token;
                        CalculatorTree newTree = new CalculatorTree(null,tree,null);
                        tree = newTree;
                    }
                }
            }
        }
        
        public CalculatorTree TranslateFromArithmetic()
        {
            throw new NotImplementedException();
        }
    }
}