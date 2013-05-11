namespace Wörds.Terminal
{
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("= Wörds {0}", typeof(Program).Assembly.GetName().Version);
            Console.WriteLine("==========================================================");
            Console.WriteLine();

            try
            {
                var language = loadLanguage("sv");
                anagramsMode(language);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.Write(ex.ToString());
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("A fatal error has occured. ");

                Environment.Exit(10);
            }
        }

        static void anagramsMode(LanguageInfo language)
        {
            Console.Write("Get top 20 anagrams for: ");
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Input was empty. Try again.");
            }
            else if (!input.All(x => language.Letters.Keys.Contains(x)))
            {
                Console.WriteLine("Invalid letter(s) in the input. Try again.");
            }
            else
            {
                var anagramFinder = new AnagramFinder(language);

                foreach (var anagram in anagramFinder.GetTopAnagrams(input.ToCharArray(), 20))
                {
                    Console.WriteLine("  {0} ({1}p)", anagram.Word, anagram.Points);
                }
            }

            Console.WriteLine();
            anagramsMode(language);
        }

        static LanguageInfo loadLanguage(string languageCode)
        {
            var languageFactory = new LanguageFactory(new LettersFileReader(), new LexiconFileReader());
            var language = languageFactory.GetLanguage(languageCode);

            Console.WriteLine("Loaded language: {0}", language.Code);
            Console.WriteLine("  Lexicon: {0} words loaded", language.Lexicon.Count);
            Console.WriteLine("  Letters: {0} letters loaded", language.Letters.Count);
            Console.WriteLine();

            return language;
        }
    }
}