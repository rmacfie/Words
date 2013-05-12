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

        public IReadOnlyCollection<Anagram> GetTopAnagrams(IReadOnlyCollection<char> source, int maxAnagramCount)
        {
            var anagrams = new List<Anagram>();

            foreach (var lexiconWord in languageInfo.Lexicon)
            {
                matchIntoAnagrams(source, lexiconWord, anagrams);
            }

            return anagrams
                .OrderByDescending(x => x.Points)
                .Take(maxAnagramCount)
                .ToList();
        }

        void matchIntoAnagrams(IReadOnlyCollection<char> source, string lexiconWord, ICollection<Anagram> anagrams)
        {
            if (lexiconWord.Length < MIN_WORD_LENGTH)
                return;

            if (lexiconWord.Length > source.Count)
                return;

            var unused = source.ToList();
            var used = new List<char>();

            foreach (var letter in lexiconWord)
            {
                if (!unused.Move(letter, used))
                    if (!unused.Move(WILDCARD, used))
                        break;
            }

            if (used.Count != lexiconWord.Length)
                return;

            var points = getTotalPoints(used);
            var anagram = new Anagram(lexiconWord, points);

            anagrams.Add(anagram);
        }

        int getTotalPoints(IEnumerable<char> letters)
        {
            var points = letters
                .Where(c => c != WILDCARD)
                .Sum(c => languageInfo.Letters[c]);

            return points;
        }
    }
}