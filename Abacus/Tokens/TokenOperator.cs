using System;

namespace Abacus.Tokens
{
    public class TokenOperator : IToken
    {
        public char Value { get; }

        public int Priority { get; }

        protected internal TokenOperator(char value)
        {
            Value = value;
            Priority = SetPriority();
        }

        public int Compute(int lhs, int rhs)
        {
            switch (Value)
            {
                case '*':
                case '∗': return lhs * rhs;
                case '÷':
                case '/': 
                    if (rhs == 0)
                        throw new DivideByZeroException();
                    return lhs / rhs;
                case '+': return lhs + rhs;
                case '-': return lhs - rhs;
                case '%': return lhs % rhs;
                case '^':
                    if (lhs == 0 && rhs < 0)
                        throw new DivideByZeroException();
                    return (int) Math.Pow(lhs, rhs);
                case '=':
                    return rhs;
            }
            return 0;
        }

        private int SetPriority()
        {
            switch (Value)
            {
                case '*':
                case '∗':
                case '÷':
                case '/':
                case '%': 
                    return 1;
                case '^': 
                    return 2;
                case '=':
                    return 3;
            }

            return 0;
        }
    }
}