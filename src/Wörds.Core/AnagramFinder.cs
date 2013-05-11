namespace Wörds
{
    using System.Collections.Generic;
    using System.Linq;

    public class AnagramFinder
    {
        readonly LanguageInfo languageInfo;

        public AnagramFinder(LanguageInfo languageInfo)
        {
            this.languageInfo = languageInfo;
        }

        public IEnumerable<Anagram> GetTopAnagrams(IReadOnlyCollection<char> letters, int maxAnagramCount)
        {
            var anagrams = new List<Anagram>();

            foreach (var word in languageInfo.Lexicon)
            {
                if (word.Length > letters.Count)
                    continue;

                var targetLetters = letters.ToList();

                foreach (var w in word)
                {
                    if (!targetLetters.Contains(w))
                        break;

                    targetLetters.Remove(w);
                }

                if (targetLetters.Count > letters.Count - word.Length)
                    continue;

                var value = word.Sum(c => languageInfo.Letters[c]);
                anagrams.Add(new Anagram(word, value));
            }

            return anagrams.OrderByDescending(x => x.Value).Take(maxAnagramCount);
        }
    }
}