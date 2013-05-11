namespace Wörds
{
    using System.Collections.Generic;

    public class LanguageInfo
    {
        public LanguageInfo(string code, IReadOnlyDictionary<char, int> letters, IReadOnlyCollection<string> lexicon)
        {
            Code = code;
            Letters = letters;
            Lexicon = lexicon;
        }

        public string Code { get; private set; }
        public IReadOnlyDictionary<char, int> Letters { get; private set; }
        public IReadOnlyCollection<string> Lexicon { get; private set; }
    }
}