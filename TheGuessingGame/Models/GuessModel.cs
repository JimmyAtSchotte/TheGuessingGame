using System.Linq;
using TheGuessingGame.Entities;

namespace TheGuessingGame.Interfaces
{
    public class GuessModel
    {
        public Game Game { get; set; }
        public PlayerGuess[] PlayerGuess { get; set; }

        public static GuessModel Create(Game game, params int[] guesses)
        {
            return new GuessModel
            {
                Game = game,
                PlayerGuess = Enumerable.Range(0, guesses.Length).Select(i => new PlayerGuess
                {
                    Id = i + 1,
                    Guess = guesses[i]
                }).ToArray()
            };
        }
    }
}