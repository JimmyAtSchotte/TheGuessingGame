namespace TheGuessingGame.Interfaces
{
    public interface IGuessService
    {
        GuessResult Guess(GuessModel guessModel);
    }
}