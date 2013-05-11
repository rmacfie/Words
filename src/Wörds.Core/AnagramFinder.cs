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

            foreach (var lexiconWord in languageInfo.Lexicon)
            {
                if (lexiconWord.Length < MIN_WORD_LENGTH)
                    continue;

                if (lexiconWord.Length > letters.Count)
                    continue;

                var used = new List<char>();
                var unused = letters.ToList();

                foreach (var letter in lexiconWord)
                {
                    if (!moveLetter(unused, used, letter))
                        if (!moveLetter(unused, used, WILDCARD))
                            break;
                }

                if (used.Count != lexiconWord.Length)
                    continue;

                var value = used.Where(c => c != WILDCARD).Sum(c => languageInfo.Letters[c]);
                anagrams.Add(new Anagram(lexiconWord, value));
            }

            return anagrams.OrderByDescending(x => x.Points).Take(maxAnagramCount).ToList();
        }

        static bool moveLetter(ICollection<char> from, ICollection<char> to, char letter)
        {
            if (!from.Remove(letter))
                return false;

            to.Add(letter);
            return true;
        }
    }
}