using System;
using System.IO;
using System.Collections.Generic;

namespace Homework.NameList
{
    class Program
    {
        /// <summary>
        /// Read a list of name/ID pairs from a comma-separated text file whose path is passed in
        /// in the <c>filename</a> parameter.
        /// 
        /// The file must contain one entry on each line in the following format:
        /// 
        /// <code>
        /// First Name;1<br>
        /// Second Name;5
        /// </code>
        /// </summary>
        /// <param name="filename">Path and name of CSV file.</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown when <c>filename</c> is not found.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Thrown when part of the path in <c>filename</c> is not found.</exception>
        /// <exception cref="System.IO.IOException">Thrown when invalid <c>filename</c> is passed.</exception>
        /// <exception cref="System.IO.FormatException">Thrown when file contains an invalid format.</exception>
        /// <example>
        /// <code>
        /// var pairs = ReadPairsFromCSVFile("pairs.csv");
        /// </code>
        /// </example>
        public static Dictionary<int, string> ReadPairsFromCSVFile(string filename) {
            var dict = new Dictionary<int, string>();

            using (var csvReader = new StreamReader(filename)) {
                while (!csvReader.EndOfStream)
                {
                    var line = csvReader.ReadLine();
                    var values = line.Split(';');
                    if (values.Length != 2) {
                        throw new FormatException("Invalid format: " + line);
                    }

                    int key;
                    if (!Int32.TryParse(values[1], out key)) {
                        throw new FormatException("Invalid ID number: " + values[1]);
                    }

                    dict.Add(key, values[0]);
                }
            }

            return dict;
        }

        static void Main(string[] args)
        {
            var dict = ReadPairsFromCSVFile("./names.csv");
            foreach (var entry in dict) {
                Console.WriteLine("" + entry.Key + " " + entry.Value);
            }
        }
    }
}
