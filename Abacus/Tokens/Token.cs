namespace Abacus.Tokens
{
    public class Token<T>
    {
        private T value;

        public T Value => value;

        protected Token(T value) =>
            this.value = value;
    }
}