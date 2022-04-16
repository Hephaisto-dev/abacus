using System;

namespace Abacus.Tokens.Functions
{
    public class TokenFunction<T> : ATokenFunction
    {
        private readonly Func<T,int> _computeFunction;

        public TokenFunction(string canonicalName, Func<T,int> func) : base(canonicalName)
        {
            _computeFunction = func;
        }

        public int Compute(T param)
        {
            return _computeFunction.Invoke(param);
        }
    }
}