using System;
using System.IO;
using System.Collections.Generic;

namespace Homework.NameList
{
    public class CSVReader
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
        /// <exception cref="System.IO.FormatException">Thrown when file has an invalid format or duplicate IDs.</exception>
        public static Dictionary<int, string> ReadPairsFromCSVFile(string filename) {
            var dict = new Dictionary<int, string>();
            
            using (var csvReader = new StreamReader(filename)) {
                while (!csvReader.EndOfStream)
                {
                    var line = csvReader.ReadLine();
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

                    // Do not allow duplicate IDs
                    if (dict.ContainsKey(key)) {
                        throw new FormatException("Duplicate ID: " + key);
                    }
                    dict.Add(key, values[0]);
                }
            }

            return dict;
        }
    }
}
