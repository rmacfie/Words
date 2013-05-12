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

            var list = File.ReadLines(filePath)
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList();

            return new ReadOnlyCollection<string>(list);
        }
    }
}