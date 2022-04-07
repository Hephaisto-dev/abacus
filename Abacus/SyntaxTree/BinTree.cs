namespace Abacus.SyntaxTree
{
    public class BinTree<T>
    {
        public T Key;
        public BinTree<T> Left;
        public BinTree<T> Right;

        public BinTree (T key , BinTree<T> left , BinTree<T> right )
        {
            this.Key = key;
            this.Right = right;
            this.Left = left;
        }

        public BinTree (T key)
        {
            this.Key = key;
            Right = null;
            Left = null;
        }
    }
}