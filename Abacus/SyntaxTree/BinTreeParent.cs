namespace Abacus.SyntaxTree
{
    public class BinTreeParent<T>
    {
        public T Key;
        public BinTreeParent<T> Left;
        public BinTreeParent<T> Right;
        public BinTreeParent<T> Parent;

        public BinTreeParent (T key , BinTreeParent<T> left , BinTreeParent<T> right )
        {
            this.Key = key;
            this.Right = right;
            this.Right.Parent = this;
            this.Left = left;
            this.Left.Parent = this;
            Parent = null;
        }

        public BinTreeParent (T key)
        {
            this.Key = key;
            Right = null;
            Left = null;
            Parent = null;
        }
    }
}