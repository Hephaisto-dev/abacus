using Abacus.Tokens;

namespace Abacus.SyntaxTree
{
    public class CalculatorTree : BinTree<Token>
    {
        public CalculatorTree(Token key, BinTree<Token> left, BinTree<Token> right) : base(key, left, right)
        {
        }

        public CalculatorTree(Token key) : base(key)
        {
        }
    }
}