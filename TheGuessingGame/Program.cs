using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using TheGuessingGame.Configuration;
using TheGuessingGame.Entities;
using TheGuessingGame.Enums;
using TheGuessingGame.Exceptions;
using TheGuessingGame.Interfaces;
using TheGuessingGame.Services;

namespace TheGuessingGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            var appConfiguration = GetAppConfiguration(configuration);
            var gameSettings = GetGameSettings(appConfiguration);

            var game = SelectGame(gameSettings);

            Play(game);

            Console.ReadLine();
        }

        private static Game SelectGame(GameSetting[] gameSettings)
        {
            Console.WriteLine("Välj spel");

            for (var i = 0; i < gameSettings.Length; i++)
                Console.WriteLine($"[{i}] {gameSettings[i].Name}");

            var gameSettingIndex = ReadInteger(0, gameSettings.Length);
            var gameSetting = gameSettings[gameSettingIndex];

            var gameFactory = new GameFactory(new RngService());

            return gameFactory.Create(gameSetting);
        }

        private static void Play(Game game)
        {
            var guessService = new GuessService();

            GuessResult result = null;

            Console.WriteLine($"Gissa ett tal mellan {game.MinRandomNumber} och {game.MaxRandomNumber}");

            while (game.GameState == GameState.OnGoing)
            {
                var guesses = new List<int>();

                for (var i = 0; i < game.Players; i++)
                {
                    Console.WriteLine($"Spelare {i + 1} gissar");
                    guesses.Add(ReadInteger(game.MinRandomNumber, game.MaxRandomNumber));
                }

                result = guessService.Guess(GuessModel.Create(game, guesses.ToArray()));

                if (game.GameState == GameState.OnGoing)
                    Console.WriteLine($"Spelare {result.PlayerId} var närmast");
            }

            Console.WriteLine($"Spelare {result.PlayerId} vann och var närmast {game.CorrectNumber}");
        }

        private static GameSetting[] GetGameSettings(AppConfiguration appConfiguration)
        {
            if (appConfiguration.GameSettings == null)
                throw new BadConfigurationException($"{nameof(appConfiguration.GameSettings)} har ej angivits");

            return appConfiguration.GameSettings;
        }

        private static AppConfiguration GetAppConfiguration(IConfiguration configuration)
        {
            var appConfiguration = new AppConfiguration();
            configuration.GetSection("AppConfiguration").Bind(appConfiguration);

            return appConfiguration;
        }

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, false)
                .Build();
        }

        private static int ReadInteger(int minValue, int maxValue)
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (!int.TryParse(input, out var output))
                {
                    Console.WriteLine($"Inmatningsfel: Skriv ett heltal mellan {minValue} och {maxValue}");

                    continue;
                }

                if (output < minValue)
                {
                    Console.WriteLine($"Inmatningsfel: Skriv ett heltal som är större än {minValue}");
                    continue;
                }

                if (output > maxValue)
                {
                    Console.WriteLine($"Inmatningsfel: Skriv ett heltal som är mindre än {maxValue}");
                    continue;
                }

                return output;
            }
        }
    }
}