using TheGuessingGame.Configuration;
using TheGuessingGame.Entities;

namespace TheGuessingGame.Interfaces
{
    public interface IGameFactory
    {
        Game Create(GameSetting gameSetting);
    }
}