using System;

namespace Abacus.Tokens
{
    public abstract class TokenOperator : Token<char>
    {
        protected TokenOperator(char value) : base(value)
        {
        }
        
        protected string AllowedChars => "∗÷/+-%^*";

        private double Compute(double lhs, double rhs)
        {
            switch (Value)
            {
                case '*':
                case '∗': return lhs * rhs;
                case '÷':
                case '/':
                    return lhs / rhs;
                case '+': return lhs + rhs;
                case '-': return lhs - rhs;
                case '%': return lhs % rhs;
                case '^': return Math.Pow(lhs, rhs);
            }

            return 0;
        }
    }
}