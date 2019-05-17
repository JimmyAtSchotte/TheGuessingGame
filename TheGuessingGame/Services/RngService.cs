using System;
using TheGuessingGame.Interfaces;

namespace TheGuessingGame.Services
{
    public class RngService : IRngService
    {
        private static readonly Random _random = new Random();

        public int Generate(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue + 1);
        }
    }
}