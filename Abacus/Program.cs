using System;

namespace Abacus
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            Calculator calculator = new Calculator(args);
            Console.WriteLine(- 1 / 10 + 1);
            return calculator.Run();
        }
    }
}
