namespace Abacus.Tokens
{
    public abstract class ATokenFunction : IToken
    {
        protected ATokenFunction(string canonicalName)
        {
            CanonicalName = canonicalName;
        }

        public string CanonicalName { get; }
    }
}