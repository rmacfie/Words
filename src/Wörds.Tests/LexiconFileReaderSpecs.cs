namespace Wörds.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Machine.Fakes;
    using Machine.Fakes.Adapters.FakeItEasy;
    using Machine.Specifications;

    public class When_reading_lexicon_file_and_the_path_is_empty : LexiconFileReaderSpecs
    {
        It should_throw = () =>
            Catch.Exception(() => Subject.GetLexicon("").ToList()).ShouldBeOfType<ArgumentException>();
    }

    public class When_reading_lexicon_file_and_the_path_is_null : LexiconFileReaderSpecs
    {
        It should_throw = () =>
            Catch.Exception(() => Subject.GetLexicon(null).ToList()).ShouldBeOfType<ArgumentException>();
    }

    public class When_reading_lexicon_file : LexiconFileReaderSpecs
    {
        static List<string> actual;

        Because of = () =>
            actual = Subject.GetLexicon(Path.Combine(Environment.CurrentDirectory, "LexiconFileReaderTestData1.txt")).ToList();

        It should_return_a_list_of_all_words = () =>
            actual.ShouldContain("bar", "baz", "foo", "foobar", "foobaz");
    }

    public abstract class LexiconFileReaderSpecs : WithSubject<LexiconFileReader>
    {
    }
}