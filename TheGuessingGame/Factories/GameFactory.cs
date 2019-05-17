using TheGuessingGame.Configuration;
using TheGuessingGame.Entities;
using TheGuessingGame.Enums;
using TheGuessingGame.Interfaces;

namespace TheGuessingGame
{
    public class GameFactory : IGameFactory
    {
        private readonly IRngService _rngService;

        public GameFactory(IRngService rngService)
        {
            _rngService = rngService;
        }

        public Game Create(GameSetting gameSetting)
        {
            var game = new Game
            {
                CorrectNumber =
                _rngService.Generate(gameSetting?.MinRandomNumber ?? 0, gameSetting?.MaxRandomNumber ?? 100),
                TurnsLeft = 3,
                GameState = GameState.OnGoing,
                Players = gameSetting?.Players ?? 2,
                MinRandomNumber = gameSetting?.MinRandomNumber ?? 0,
                MaxRandomNumber = gameSetting?.MaxRandomNumber ?? 100
            };

            return game;
        }
    }
}