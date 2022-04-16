using System;

namespace Abacus
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            Calculator calculator = new Calculator(args);
            return calculator.Run( Console.In.ReadToEnd());
        }
    }
}
