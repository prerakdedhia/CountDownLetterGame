namespace CountdownLetterGame.Services
{
    public interface ICountDownGameService
    {
        public int FinalScore { get; }
        public void CountDownLetterGame();
    }
}
