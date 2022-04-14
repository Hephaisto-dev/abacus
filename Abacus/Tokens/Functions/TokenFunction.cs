using System;

namespace Abacus.Tokens
{
    public class ATokenFunction<T> : ATokenFunction
    {
        private readonly Func<T,int> _computeFunction;

        public ATokenFunction(string canonicalName, Func<T,int> func) : base(canonicalName)
        {
            _computeFunction = func;
        }

        public int Compute(T param)
        {
            return _computeFunction.Invoke(param);
        }
    }
}