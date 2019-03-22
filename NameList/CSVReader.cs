using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Homework.NameList
{
    public class CSVReader
    {
        /// <summary>
        /// <para>
        /// Reads a list of name/ID pairs from a comma-separated text file whose path is passed in
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
        /// <exception cref="System.IO.FormatException">Thrown when file has an invalid format or duplicate IDs.</exception>
        public static IDictionary<int, string> ReadPairsFromCSVFile(string filename) {
            IDictionary<int, string> dict = new Dictionary<int, string>();
            
            using (var csvReader = new StreamReader(filename)) {
                while (!csvReader.EndOfStream)
                {
                    var line = csvReader.ReadLine();
                    var pair = ParseLine(line);

                    // Do not allow duplicate IDs
                    if (dict.ContainsKey(pair.Key)) {
                        throw new FormatException("Duplicate ID: " + pair.Key);
                    }
                    dict.Add(pair.Key, pair.Value);
                }
            }

            return dict;
        }

        /// <summary>
        /// <para>
        /// Parses a single CSV line and returns it as a vey/value pair. Line must have the following format:
        /// <code>
        /// Second Name;342
        /// </code>
        /// </summary>
        /// <param name="line">The line to parse.</param>
        /// <returns>A <c>KeyValuePair&lt;int, string&gt;</c> with values from the line.</returns>
        /// <exception cref="System.IO.FormatException">Thrown when line has an invalid format.</exception>
        public static KeyValuePair<int, string> ParseLine(string line) {
            var values = line.Split(';');

            // All lines must have at least 2 values: name and ID
            if (values.Length != 2) {
                throw new FormatException("Invalid format: " + line);
            }

            int key;
            // Make sure ID is a valid integer
            if (!Int32.TryParse(values[1], out key)) {
                throw new FormatException("Invalid ID number: " + values[1]);
            }

            return new KeyValuePair<int, string>(key, values[0]);
        }
    }
}
