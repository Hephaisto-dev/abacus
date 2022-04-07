using System;

namespace Abacus.Tokens
{
    public class TokenNumber : Token
    {
        private int value;

        public int Value => value;
        protected internal TokenNumber(int value)
        {
            this.value = value;
        }
    }
}