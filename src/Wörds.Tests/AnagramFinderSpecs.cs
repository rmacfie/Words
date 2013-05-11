﻿namespace Wörds.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Machine.Fakes;
    using Machine.Fakes.Adapters.FakeItEasy;
    using Machine.Specifications;

    public class When_getting_top_scoring_anagrams : AnagramFinderSpecs
    {
        static List<Anagram> result;

        Establish context = () =>
        {
            var lexicon = new List<string> { "baz", "zz", "bar", "foo", "ba", "az" };
            var letters = new Dictionary<char, int> { { 'a', 1 }, { 'b', 2 }, { 'f', 3 }, { 'o', 4 }, { 'z', 5 } };
            var language = new LanguageInfo("Test", letters, lexicon);

            Subject = new AnagramFinder(language);
        };

        Because of = () =>
            result = Subject.GetTopAnagrams(new[] { 'z', 'a', 'b' }, 2).ToList();

        It should_suggest = () =>
        {
            result.Count.ShouldEqual(2);
            result[0].ShouldMatch(x => x.Word == "baz" && x.Value == 8);
            result[1].ShouldMatch(x => x.Word == "az" && x.Value == 6);
        };
    }


    public abstract class AnagramFinderSpecs : WithSubject<AnagramFinder>
    {
    }
}