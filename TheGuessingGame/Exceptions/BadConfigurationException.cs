using System;

namespace TheGuessingGame.Exceptions
{
    public class BadConfigurationException : Exception
    {
        public BadConfigurationException(string message) : base(message)
        {
        }
    }
}