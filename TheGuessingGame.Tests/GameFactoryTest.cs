using Moq;
using NUnit.Framework;
using TheGuessingGame.Configuration;
using TheGuessingGame.Enums;
using TheGuessingGame.Interfaces;
using TheGuessingGame.Services;

namespace TheGuessingGame.Tests
{
    [TestFixture]

    public class GameFactoryTest
    {
        [Test]
        public void Create_CorrectNumber_IsNotNull()
        {
            IGameFactory gameFactory = new GameFactory(new RngService());

            var game = gameFactory.Create(null);
             
            Assert.IsNotNull(game.CorrectNumber);
        }
        
        [Test]
        public void Create_TurnsLeft_ShouldStartAt3()
        {
            IGameFactory gameFactory = new GameFactory(new RngService());

            var game = gameFactory.Create(null);
             
            Assert.AreEqual(3, game.TurnsLeft);
        }

        [Test]
        public void Create_GameState_ShouldBeOnGoing()
        {
            IGameFactory gameFactory = new GameFactory(new RngService());

            var game = gameFactory.Create(null);
             
            Assert.AreEqual(GameState.OnGoing, game.GameState);
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void Create_Players(int players)
        {
            IGameFactory gameFactory = new GameFactory(new RngService());

            var game = gameFactory.Create(new GameSetting()
            {
                Players = players
            });
             
            Assert.AreEqual(players, game.Players);
        }

        [Test]
        public void Create_MinRandomNumber_FromGameSettings()
        {
            int result = 0;

            var rngMock = new Mock<IRngService>();
            rngMock.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>()))
                .Callback((int minValue, int maxValue) => { result = minValue; });

            IGameFactory gameFactory = new GameFactory(rngMock.Object);

            var gameSetting = new GameSetting()
            {
                MinRandomNumber = 1000
            };

            var game = gameFactory.Create(gameSetting);
            
            Assert.AreEqual(gameSetting.MinRandomNumber, result);
            Assert.AreEqual(gameSetting.MinRandomNumber, game.MinRandomNumber);
        }

        [Test]
        public void Create_MinRandomNumber_DefaultValue()
        {
            int result = 0;

            var rngMock = new Mock<IRngService>();
            rngMock.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>()))
                .Callback((int minValue, int maxValue) => { result = minValue; });

            IGameFactory gameFactory = new GameFactory(rngMock.Object);
            
           var game = gameFactory.Create(null);
            
            Assert.AreEqual(0, result);
            Assert.AreEqual(0, game.MinRandomNumber);
        }

        [Test]
        public void Create_MaxRandomNumber_FromGameSettings()
        {
            int result = 0;

            var rngMock = new Mock<IRngService>();
            rngMock.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>()))
                .Callback((int minValue, int maxValue) => { result = maxValue; });

            IGameFactory gameFactory = new GameFactory(rngMock.Object);
             var gameSetting = new GameSetting()
             {
                 MaxRandomNumber = 1000
             };

            var game = gameFactory.Create(gameSetting);

            Assert.AreEqual(gameSetting.MaxRandomNumber, result);
            Assert.AreEqual(gameSetting.MaxRandomNumber, game.MaxRandomNumber);
        }

        [Test]
        public void Create_MaxRandomNumber_DefaultValue()
        {
            int result = 0;

            var rngMock = new Mock<IRngService>();
            rngMock.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>()))
                .Callback((int minValue, int maxValue) => { result = maxValue; });

            IGameFactory gameFactory = new GameFactory(rngMock.Object);
           
            var game = gameFactory.Create(null);

            Assert.AreEqual(100, result);
            Assert.AreEqual(100, game.MaxRandomNumber);

        }
    }
}