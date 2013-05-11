namespace Wörds
{
    public class Anagram
    {
        public Anagram(string word, int value)
        {
            Word = word;
            Value = value;
        }

        public string Word { get; private set; }
        public int Value { get; private set; }
    }
}