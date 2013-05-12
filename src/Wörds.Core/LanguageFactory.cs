namespace Wörds
{
    using System;

    public class LanguageFactory
    {
        public static string LettersPathPattern = @"{1}\data\{0}.letters.txt";
        public static string LexiconPathPattern = @"{1}\data\{0}.lexicon.txt";

        readonly LettersFileReader lettersFileReader;
        readonly LexiconFileReader lexiconFileReader;

        public LanguageFactory(LettersFileReader lettersFileReader, LexiconFileReader lexiconFileReader)
        {
            this.lettersFileReader = lettersFileReader;
            this.lexiconFileReader = lexiconFileReader;
        }

        public LanguageInfo GetLanguage(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
                throw new ArgumentException("LanguageCode is required.", "languageCode");

            var lettersPath = string.Format(LettersPathPattern, languageCode, Environment.CurrentDirectory);
            var lexiconPath = string.Format(LexiconPathPattern, languageCode, Environment.CurrentDirectory);
            var letters = lettersFileReader.GetLetters(lettersPath);
            var lexicon = lexiconFileReader.GetLexicon(lexiconPath);

            return new LanguageInfo(languageCode, letters, lexicon);
        }
    }
}