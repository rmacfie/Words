namespace Wörds.Tests
{
    using System.Collections.Generic;
    using Machine.Fakes;
    using Machine.Fakes.Adapters.FakeItEasy;
    using Machine.Specifications;

    public class When_creating_language : LanguageFactorySpecs
    {
        static LanguageInfo actual;
        static List<string> lexicon;
        static Dictionary<char, int> letters;

        Establish context = () =>
        {
            LanguageFactory.LexiconPathPattern = "aa:{0}";
            LanguageFactory.LettersPathPattern = "bb:{0}";
            lexicon = new List<string> { "someword" };
            letters = new Dictionary<char, int> { { 's', 2 } };
            The<LexiconFileReader>().WhenToldTo(x => x.GetLexicon("aa:xx")).Return(lexicon);
            The<LettersFileReader>().WhenToldTo(x => x.GetLetters("bb:xx")).Return(letters);
        };

        Because of = () =>
            actual = Subject.GetLanguage("xx");

        It should_load_the_letters_file = () =>
            actual.Letters.ShouldContain(letters);

        It should_load_the_lexicon_file = () =>
            actual.Lexicon.ShouldContain(lexicon);

        It should_set_code = () =>
            actual.Code.ShouldEqual("xx");
    }

    public abstract class LanguageFactorySpecs : WithSubject<LanguageFactory>
    {
    }
}