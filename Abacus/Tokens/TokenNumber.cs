using System;

namespace Abacus.Tokens
{
    public class TokenNumber : Token<int>
    {
        protected TokenNumber(int value) : base(value)
        {
        }
    }
}