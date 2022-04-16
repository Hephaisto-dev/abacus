using System;

namespace Abacus.Tokens
{
    public class TokenOperator : IToken
    {
        public char Value { get; }

        public int Priority { get; }

        protected internal TokenOperator(char value)
        {
            this.Value = value;
            Priority = SetPriority();
        }

        public int Compute(double lhs, double rhs)
        {
            switch (Value)
            {
                case '*':
                case '∗': return (int) (lhs * rhs);
                case '÷':
                case '/': 
                    if (rhs == 0)
                        throw new DivideByZeroException();
                    return (int) (lhs / rhs);
                case '+': return (int) (lhs + rhs);
                case '-': return (int) (lhs - rhs);
                case '%': return (int) (lhs % rhs);
                case '^':
                    if (lhs == 0 && rhs < 0)
                        throw new DivideByZeroException();
                    return (int) Math.Pow(lhs, rhs);
            }
            return 0;
        }

        private int SetPriority()
        {
            switch (Value)
            {
                case '*':
                case '∗': return 1;
                case '÷':
                case '/': return 1;
                case '%': return 1;
                case '^': return 2;
            }

            return 0;
        }
    }
}