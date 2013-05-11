namespace Wörds
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;

    public class LettersFileReader
    {
        public virtual IReadOnlyDictionary<char, int> GetLetters(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("filePath");

            var lineNumber = 0;
            var data = new Dictionary<char, int>();

            foreach (var line in File.ReadLines(path))
            {
                lineNumber++;

                try
                {
                    var letterAndPoint = parseLine(line);
                    data.Add(letterAndPoint.Key, letterAndPoint.Value);
                }
                catch (Exception)
                {
                    throw new Exception(string.Format("Bad line format in Letters file '{0}', line number {1} ('{2}'). ", path, lineNumber, line));
                }
            }

            return new ReadOnlyDictionary<char, int>(data);
        }

        KeyValuePair<char, int> parseLine(string line)
        {
            var split = line.Split(new[] { ' ', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length != 2)
                throw new Exception();

            if (split[0].Length != 1)
                throw new Exception();

            return new KeyValuePair<char, int>(split[0][0], int.Parse(split[1]));
        }
    }
}