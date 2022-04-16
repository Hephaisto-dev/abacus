namespace Abacus.Tokens
{
    public class TokenVariable : ATokenValuable
    {
        public string Name { get; }

        protected internal TokenVariable(int value, string name) : base(value)
        {
            Name = name;
        }
    }
}