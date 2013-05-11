namespace Wörds.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Machine.Fakes;
    using Machine.Fakes.Adapters.FakeItEasy;
    using Machine.Specifications;

    public class When_getting_top_scoring_anagrams_with_wildcard : AnagramFinderSpecs
    {
        static List<Anagram> result;

        Establish context = () =>
        {
            var lexicon = new List<string> { "ab", "az", "foo", "baz" };
            var letters = new Dictionary<char, int> { { 'a', 1 }, { 'b', 2 }, { 'f', 3 }, { 'o', 4 }, { 'z', 8 } };
            var language = new LanguageInfo("Test", letters, lexicon);

            Subject = new AnagramFinder(language);
        };

        Because of = () =>
            result = Subject.GetTopAnagrams(new[] { 'a', 'b', '*' }, 2).ToList();

        It should_find_anagrams_using_the_wildcard = () =>
            result.ShouldContain(x => x.Word == "baz");
    }

    public class When_getting_top_scoring_anagrams : AnagramFinderSpecs
    {
        static List<Anagram> result;

        Establish context = () =>
        {
            var lexicon = new List<string> { "baz", "zz", "bar", "foo", "ba", "az", "x" };
            var letters = new Dictionary<char, int> { { 'a', 1 }, { 'b', 2 }, { 'f', 3 }, { 'o', 4 }, { 'z', 5 }, { 'x', 100 } };
            var language = new LanguageInfo("Test", letters, lexicon);

            Subject = new AnagramFinder(language);
        };

        Because of = () =>
            result = Subject.GetTopAnagrams(new[] { 'z', 'a', 'b', 'b', 'x' }, 2).ToList();

        It should_filter_out_too_short_words = () =>
            result.ShouldNotContain(x => x.Word == "x");

        It should_find_the_top_anagrams = () =>
        {
            result.ShouldContain(x => x.Word == "baz" && x.Points == 8);
            result.ShouldContain(x => x.Word == "az" && x.Points == 6);
        };

        It should_limit_the_results = () =>
            result.Count.ShouldEqual(2);

        It should_sort_the_results_descending_by_points = () =>
        {
            var prevPoints = int.MaxValue;
            foreach (var anagram in result)
            {
                anagram.Points.ShouldBeLessThanOrEqualTo(prevPoints);
                prevPoints = anagram.Points;
            }
        };
    }


    public abstract class AnagramFinderSpecs : WithSubject<AnagramFinder>
    {
    }
}