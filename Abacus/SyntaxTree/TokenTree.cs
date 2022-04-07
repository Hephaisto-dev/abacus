using Abacus.Tokens;

namespace Abacus.SyntaxTree
{
    public class TokenTree : BinTreeParent<Token>
    {
        public TokenTree(Token key, BinTreeParent<Token> left, BinTreeParent<Token> right) : base(key, left, right)
        {
        }

        public TokenTree(Token key) : base(key)
        {
        }
    }
}