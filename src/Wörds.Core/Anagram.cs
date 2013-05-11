namespace Wörds
{
    public class Anagram
    {
        public Anagram(string word, int value)
        {
            Word = word;
            Points = value;
        }

        public string Word { get; private set; }
        public int Points { get; private set; }
    }
}