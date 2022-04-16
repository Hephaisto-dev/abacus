namespace Abacus.Tokens
{
    public abstract class ATokenValuable : IToken
    {
        public int Value { get; }

        protected internal ATokenValuable(int value)
        {
            Value = value;
        }
    }
}