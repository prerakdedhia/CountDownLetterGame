using CountdownLetterGame.Constants;

namespace CountdownLetterGame.Repositories
{
    public class WordDataRepository : IWordDataRepository
    {
        private List<string> wordList = new List<string>();

        public List<string> GetAllWords()
        {
            try
            {
                string[] words = File.ReadAllLines(CountDownGameConstants.FilePath);

                foreach (var word in words)
                {
                    wordList.Add(word.Trim());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading words from file: {ex.Message}");
            }

            return wordList;
        }
    }
}
