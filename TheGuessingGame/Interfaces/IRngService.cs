namespace TheGuessingGame.Interfaces
{
    public interface IRngService
    {
        int Generate(int minValue, int maxValue);
    }
}