using System;
using System.Linq;
using TheGuessingGame.Enums;
using TheGuessingGame.Interfaces;

namespace TheGuessingGame.Services
{
    public class GuessService : IGuessService
    {
        public GuessResult Guess(GuessModel guessModel)
        {
            var closestGuess = guessModel.PlayerGuess.OrderBy(x => Math.Abs(guessModel.Game.CorrectNumber - x.Guess))
                .FirstOrDefault();

            guessModel.Game.TurnsLeft -= 1;

            if (guessModel.Game.TurnsLeft == 0)
                guessModel.Game.GameState = GameState.Completed;

            if (closestGuess.Guess == guessModel.Game.CorrectNumber)
                guessModel.Game.GameState = GameState.Completed;

            return new GuessResult
            {
                PlayerId = closestGuess.Id
            };
        }
    }
}