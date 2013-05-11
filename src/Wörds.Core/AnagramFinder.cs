namespace Wörds
{
    using System.Collections.Generic;
    using System.Linq;

    public class AnagramFinder
    {
        const int MIN_WORD_LENGTH = 2;

        readonly LanguageInfo languageInfo;

        public AnagramFinder(LanguageInfo languageInfo)
        {
            this.languageInfo = languageInfo;
        }

        public IReadOnlyCollection<Anagram> GetTopAnagrams(IReadOnlyCollection<char> letters, int maxAnagramCount)
        {
            var anagrams = new List<Anagram>();

            foreach (var word in languageInfo.Lexicon)
            {
                if (word.Length < MIN_WORD_LENGTH)
                    continue;

                if (word.Length > letters.Count)
                    continue;

                var rack = letters.ToList();

                foreach (var letter in word)
                {
                    if (!rack.Remove(letter))
                        break;
                }

                if (rack.Count > letters.Count - word.Length)
                    continue;

                var value = word.Sum(c => languageInfo.Letters[c]);
                anagrams.Add(new Anagram(word, value));
            }

            return anagrams.OrderByDescending(x => x.Value).Take(maxAnagramCount).ToList();
        }
    }
}