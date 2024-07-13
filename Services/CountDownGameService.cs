using CountdownLetterGame.Constants;
using CountdownLetterGame.Repositories;

namespace CountdownLetterGame.Services
{
    public class CountDownGameService : ICountDownGameService
    {        
        private readonly List<char> selectedLetters = new List<char>();
        private readonly List<string> wordList;
        private readonly IWordDataRepository _wordDataRepository;
        private readonly Random random = new Random();
        private int totalScore = 0;
        public int FinalScore => totalScore;

        public CountDownGameService(IWordDataRepository wordDataRepository) {
            _wordDataRepository = wordDataRepository;
            wordList = wordDataRepository.GetAllWords();
        }

        public void CountDownLetterGame()
        {
            selectedLetters.Clear();
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine($"Please select Consonants/Vowels for letter { i + 1 }, 1 for Consonant or 2 for Vowel");
                SelectLetter();
            }

            Console.WriteLine($"\nSelected letters for the round {string.Join(", ", selectedLetters)}");

            string longestWord = FindLongestWord(selectedLetters);
            Console.WriteLine($"Longest word for the round: {longestWord}");

            int wordScore = CalculateScore(longestWord);
            Console.WriteLine($"Score for the round: {wordScore}");

            totalScore += wordScore;
            Console.WriteLine($"\nTotal score: {totalScore}");

        }

        private void SelectLetter()
        {
            int choice;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out choice) && ValidateSelection(choice))
                {
                    if (choice == 1)
                    {
                        selectedLetters.Add(GetRandomConsonant());
                        break;
                    }
                    else if (choice == 2)
                    {
                        selectedLetters.Add(GetRandomVowel());
                        break;
                    }
                }
                else
                    Console.WriteLine("Invalid choice. Please select 1 for Consonant or 2 for Vowel:");
            }
        }

        private bool ValidateSelection(int choice)
        {
            if (choice != 1 && choice != 2)
                return false;

            int vowelCount = selectedLetters.Count(c => IsVowel(c));
            int consonantCount = selectedLetters.Count - vowelCount;

            if (choice == 1 && consonantCount >= 6)
            {
                Console.WriteLine("Cannot add more of consonant letter. Choose vowel.");
                return false;
            }
            if (choice == 2 && vowelCount >= 5)
            {
                Console.WriteLine("Cannot add more of vowel letter. Choose consonant.");
                return false;
            }
            return true;
        }

        private bool IsVowel(char letter)
        {
            return CountDownGameConstants.Vowels.Contains(char.ToLower(letter));
        }

        private char GetRandomVowel()
        {
            return CountDownGameConstants.Vowels[random.Next(CountDownGameConstants.Vowels.Length)];
        }

        private char GetRandomConsonant()
        {
            return CountDownGameConstants.Consonants[random.Next(CountDownGameConstants.Consonants.Length)];
        }

        private string FindLongestWord(List<char> selectedLetters)
        {
            string longestWord = "";
            int maxLength = 0;

            foreach (var word in wordList)
            {
                if (IsWordValid(word, selectedLetters))
                {
                    if (word.Length > maxLength)
                    {
                        maxLength = word.Length;
                        longestWord = word;
                    }
                }
            }

            return longestWord;
        }

        private bool IsWordValid(string word, List<char> selectedLetters)
        {
            var letterCounts = selectedLetters.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

            foreach (var letter in word)
            {
                if (!letterCounts.ContainsKey(letter) || letterCounts[letter] == 0)
                {
                    return false;
                }
                else
                {
                    letterCounts[letter]--;
                }
            }

            return true;
        }

        private int CalculateScore(string word)
        {
            return word.Length == 9 ? 18 : word.Length;
        }
    }
}
