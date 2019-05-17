using TheGuessingGame.Enums;

namespace TheGuessingGame.Entities
{
    public class Game
    {
        public int CorrectNumber { get; set; }

        public int TurnsLeft { get; set; }
        public GameState GameState { get; set; }

        public int Players { get; set; }

        public int MinRandomNumber { get; set; }
        public int MaxRandomNumber { get; set; }
    }
}