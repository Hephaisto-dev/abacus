using System;

namespace Abacus.Tokens
{
    public class TokenOperator : Token
    {

        private char value;

        public char Value => value;

        private int priority;

        protected internal TokenOperator(char value)
        {
            this.value = value;
            priority = GetPriority();
        }

        public int Compute(double lhs, double rhs)
        {
            switch (Value)
            {
                case '*':
                case '∗': return (int) (lhs * rhs);
                case '÷':
                case '/': return (int) (lhs / rhs);
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

        public int GetPriority()
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