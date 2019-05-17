using Moq;
using NUnit.Framework;
using TheGuessingGame.Entities;
using TheGuessingGame.Enums;
using TheGuessingGame.Interfaces;
using TheGuessingGame.Services;

namespace TheGuessingGame.Tests
{
    [TestFixture]

    public class GuessServiceTest
    {
        [Test]
        public void Guess_ShouldReturnClosestGuest()
        {
            var rngServiceMock = new Mock<IRngService>();
            rngServiceMock.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>())).Returns(10);

            IGameFactory gameFactory = new GameFactory(rngServiceMock.Object);
            var game = gameFactory.Create(null);

            IGuessService guessService = new GuessService();
            var result = guessService.Guess(GuessModel.Create(game, 1, 9));

            Assert.AreEqual(2, result.PlayerId);
        }

        [Test]
        public void Guess_TurnsLeftShouldCountDown()
        {
            var rngServiceMock = new Mock<IRngService>();
            rngServiceMock.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>())).Returns(10);

            IGameFactory gameFactory = new GameFactory(rngServiceMock.Object);
            var game = gameFactory.Create(null);

            IGuessService guessService = new GuessService();
            guessService.Guess(GuessModel.Create(game, 1, 9));

            Assert.AreEqual(2, game.TurnsLeft);
        }

        [Test]
        public void Guess_AllTurnsUsed_GameStateEqualsCompleted()
        {
            var rngServiceMock = new Mock<IRngService>();
            rngServiceMock.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>())).Returns(10);

            IGameFactory gameFactory = new GameFactory(rngServiceMock.Object);
            var game = gameFactory.Create(null);

            IGuessService guessService = new GuessService();

            var turns = game.TurnsLeft;

            for (int i = 0; i < turns; i++)
                guessService.Guess(GuessModel.Create(game, 1, 9));

            Assert.AreEqual(GameState.Completed, game.GameState);
        }

        [Test]
        public void Guess_CorrectGuess_GameStateEqualsCompleted()
        {
            var rngServiceMock = new Mock<IRngService>();
            rngServiceMock.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>())).Returns(10);

            IGameFactory gameFactory = new GameFactory(rngServiceMock.Object);
            var game = gameFactory.Create(null);

            IGuessService guessService = new GuessService();
            guessService.Guess(GuessModel.Create(game, 1, 10));

            Assert.AreEqual(GameState.Completed, game.GameState);
        }
    }
}