using NUnit.Framework;
using TheGuessingGame.Interfaces;
using TheGuessingGame.Services;

namespace TheGuessingGame.Tests
{
    [TestFixture]
    public class RngServiceTest
    {
        [TestCase(0, 100)]
        [TestCase(100, 200)]
        public void Create_IsInRange(int minValue, int maxValue)
        {
            IRngService rngService = new RngService();

            var result = rngService.Generate(minValue, maxValue);

            Assert.GreaterOrEqual(result, minValue);
            Assert.LessOrEqual(result, maxValue);
        }
    }
}