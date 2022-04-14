using System;

namespace Abacus.Tokens
{
    public class TokenNumber : Token
    {
        public int Value { get; }

        protected internal TokenNumber(int value)
        {
            Value = value;
        }
    }
}