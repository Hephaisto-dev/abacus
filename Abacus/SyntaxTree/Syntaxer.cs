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

        public TokenTree TranslateFromRpn()
        {
            while (tokens.Count > 0)
            {
                TokenTree tokenTree = new TokenTree(tokens[0]);
            }
        }
        
        public TokenTree TranslateFromNormal()
        {
            throw new NotImplementedException();
        }
    }
}