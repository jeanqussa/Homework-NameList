using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Homework.NameList;

namespace Homework.NameList
{
    class Program
    {
        /// <summary>
        /// <para>
        /// Read a list of name/ID pairs from a comma-separated text file whose path is passed in
        /// in the <c>filename</a> parameter.
        /// </para>
        /// <para>
        /// The file must contain one entry on each line in the following format:
        /// 
        /// <code>
        /// First Name;1<br>
        /// Second Name;5
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="filename">Path and name of CSV file.</param>
        /// <returns>A <c>Dictionary&lt;int, string&gt;</c> with values read from the file.</returns>
        /// <exception cref="System.IO.FileNotFoundException">Thrown when <c>filename</c> is not found.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Thrown when part of the path in <c>filename</c> is not found.</exception>
        /// <exception cref="System.IO.IOException">Thrown when invalid <c>filename</c> is passed.</exception>
        /// <exception cref="System.IO.FormatException">Thrown when file contains an invalid format.</exception>
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

                    if (dict.ContainsKey(key)) {
                        throw new FormatException("Duplicate key: " + key);
                    }
                    dict.Add(key, values[0]);
                }
            }

            return dict;
        }

        /// <summary>
        /// Converts a <c>Dictionary</c> into a <c>List</c> of <c>KeyValuePair</c>s and sorts it in
        /// ascending or descending order based on the dictionary's key.
        /// </summary>
        /// <param name="pairs"><c>Dictionary&lt;int, string&gt;</c> to sort.</param>
        /// <param name="descending">Whether to sort in descending order. Default = false.</param>
        /// <returns>A <c>List&lt;KeyValuePair&lt;int, string&gt;&gt;</c> containing all values 
        /// in the dictionary but in sorted order.</returns>
        public static List<KeyValuePair<int, string>> SortPairs(ref Dictionary<int, string> pairs, bool descending = false) {
            var pairsArray = new List<KeyValuePair<int, string>>();
            foreach (var entry in pairs) {
                pairsArray.Add(entry);
            }
            QuickSort.Sort(ref pairsArray, (a, b) => (a.Key > b.Key) == descending && a.Key != b.Key, 0, pairsArray.Count-1);
            return pairsArray;
        }

        static void Main(string[] args)
        {
            var dict = ReadPairsFromCSVFile("./names.csv");
            var sorted = SortPairs(ref dict, false);
            foreach (var entry in sorted) {
                Console.WriteLine("" + entry.Key + " " + entry.Value);
            }
        }
    }
}
