using System;
using System.Collections.Generic;

namespace Abacus.Tokens.Functions
{
    public class FunctionManager
    {
        public List<ATokenFunction> Functions { get; } = new();
        
        public FunctionManager()
        {
            Functions.Add(new TokenFunction<int>("sqrt",Sqrt));
            Functions.Add(new TokenFunction<(int,int)>("max",Max));
            Functions.Add(new TokenFunction<(int,int)>("min",Min));
            Functions.Add(new TokenFunction<int>("facto",Facto));
            Functions.Add(new TokenFunction<int>("isprime",IsPrime));
            Functions.Add(new TokenFunction<(int,int)>("gcd",Gcd));
        }

        private static int Sqrt(int a)
        {
            return (int)Math.Floor(Math.Sqrt(a));
        }

        private static int Max((int, int) valueTuple)
        {
            (int a, int b) = valueTuple;
            return a > b ? a : b;
        }
        
        private static int Min((int, int) valueTuple)
        {
            (int a, int b) = valueTuple;
            return a < b ? a : b;
        }
        
        private static int Facto(int a)
        {
            int result = 1;
            for (int i = 1; i <= a; i++)
            {
                result *= i;
            }
            return result;
        }

        private static int Gcd((int, int) valueTuple)
        {
            (int a, int b) = valueTuple;
            if (b == 0)
                return a;
            return Gcd((b, a % b));
        }

        private static int IsPrime(int a)
        {
            if (a <= 1) return 0;
            if (a == 2) return 1;
            if (a % 2 == 0) return 0;

            int boundary = Sqrt(a); //stop point for better perf
            for (int i = 3; i <= boundary; i += 2)
                if (a % i == 0)
                    return 0;
            
            return 1;   
        }
    }
}