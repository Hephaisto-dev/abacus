using System;
using Abacus;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private Calculator _rpnCalculator;
        private Calculator _normalCalculator;

        public Tests()
        {
            _rpnCalculator = new Calculator(new[] {"--rpn"});
            _normalCalculator = new Calculator(Array.Empty<string>());
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("2 3 + 2 *", 10,0)]
        [TestCase("2 3 + 2", 0,2)]
        [TestCase("0 2 * 1 + 3 -", -2,0)]
        [TestCase("2 0 /", 0,3)]
        [TestCase("2 2 ^", 4,0)]
        [TestCase("2 3 max isprime", 1,0)]
        public void TestRpn(string expression, int expectedResult, int expectedReturn)
        {
            try
            {
                Assert.AreEqual(expectedResult, _rpnCalculator.Calculate(expression));
            }
            catch (Exception)
            {
                // ignored
            }

            Assert.AreEqual(expectedReturn,_rpnCalculator.Run(expression));
        }

        [Test]
        [TestCase("5 + 2 - 7", 0,0)]
        [TestCase("2 + 3*4", 14,0)]
        [TestCase("(2 + 3 * 4", 0,2)]
        [TestCase("(2 + 1) ^ 5 * 2", 486,0)]
        [TestCase("-1 * 2", -2,0)]
        [TestCase("isprime (max (2 ,3))", 1,0)]
        [TestCase("--1", 1,0)]
        [TestCase("a = b = 2", 2,0)]
        public void TestNormal(string expression, int expectedResult, int expectedReturn)
        {
            try 
            {
                Assert.AreEqual(expectedResult, _normalCalculator.Calculate(expression));
            }
            catch (Exception)
            {
                // ignored
            }

            Assert.AreEqual(expectedReturn,_normalCalculator.Run(expression));
        }
    }
}