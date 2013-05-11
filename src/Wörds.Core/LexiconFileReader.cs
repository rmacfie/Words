namespace Wörds
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;

    public class LexiconFileReader
    {
        public virtual IReadOnlyCollection<string> GetLexicon(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("filePath");

            var list = File.ReadLines(filePath).ToList();
            return new ReadOnlyCollection<string>(list);
        }
    }
}