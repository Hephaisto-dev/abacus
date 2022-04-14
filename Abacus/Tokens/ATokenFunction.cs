namespace Abacus.Tokens
{
    public abstract class ATokenFunction : Token
    {
        protected ATokenFunction(string canonicalName)
        {
            CanonicalName = canonicalName;
        }

        public string CanonicalName { get; }
    }
}