namespace Wörds
{
    using System.Collections.Generic;
    using System.Linq;

    public class AnagramFinder
    {
        public const int MIN_WORD_LENGTH = 2;
        public const char WILDCARD = '*';

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

                var used = new List<char>();
                var unused = letters.ToList();

                foreach (var letter in word)
                {
                    if (unused.Remove(letter))
                    {
                        used.Add(letter);
                    }
                    else if (unused.Remove(WILDCARD))
                    {
                        used.Add(WILDCARD);
                    }
                    else
                    {
                        break;
                    }
                }

                if (used.Count != word.Length)
                    continue;

                var value = used.Where(c => c != WILDCARD).Sum(c => languageInfo.Letters[c]);
                anagrams.Add(new Anagram(word, value));
            }

            return anagrams.OrderByDescending(x => x.Points).Take(maxAnagramCount).ToList();
        }
    }
}