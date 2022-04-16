namespace Abacus.Tokens
{
    public abstract class ATokenValuable : IToken
    {
        public int Value { get; set; }

        protected internal ATokenValuable(int value)
        {
            Value = value;
        }
    }
}