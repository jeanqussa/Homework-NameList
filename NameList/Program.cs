﻿using System;
using System.Collections;
using System.Collections.Generic;
using Homework.NameList;

namespace Homework.NameList
{
    class Program
    {
        /// <summary>
        /// Converts a <c>Dictionary</c> into a <c>List</c> of <c>KeyValuePair</c>s and sorts it in
        /// ascending or descending order based on the dictionary's key.
        /// </summary>
        /// <param name="pairs"><c>Dictionary&lt;int, string&gt;</c> to sort.</param>
        /// <param name="descending">Whether to sort in descending order. Default = false.</param>
        /// <returns>A <c>List&lt;KeyValuePair&lt;int, string&gt;&gt;</c> containing all values 
        /// in the dictionary but in sorted order.</returns>
        public static IList<KeyValuePair<int, string>> SortPairs(ref IDictionary<int, string> pairs, bool descending = false) {
            IList<KeyValuePair<int, string>> pairsArray = new List<KeyValuePair<int, string>>();
            foreach (var entry in pairs) {
                pairsArray.Add(entry);
            }
            QuickSort.Sort(ref pairsArray, (a, b) => descending ? (a.Key > b.Key) : (a.Key < b.Key), 0, pairsArray.Count - 1);
            return pairsArray;
        }

        static int Main(string[] args)
        {
            // Print the help message
            if (args.Length == 0) {
                Console.WriteLine("Please specify a file name.");
                Console.WriteLine("Usage: NameList [-d] <filename>");
                Console.WriteLine("");
                Console.WriteLine("Options:");
                Console.WriteLine("  -d (optional): sort in descending order");
                Console.WriteLine("  <filename>: path to CSV file");
                Console.WriteLine("");
                Console.WriteLine("Examples:");
                Console.WriteLine("  NameList names.csv");
                Console.WriteLine("  NameList -d names.csv");
                return 1;
            }

            // Sort descendingly if we have a '-d' flag specified. Since this
            // is the only flag available, we can get away with only
            // checking the first argument instead of using a loop
            bool descending = false;
            if (args[0] == "-d") {
                descending = true;
            }
            // Last argument is the filename. Ignore the rest
            string filename = args[args.Length - 1];

            // Handle exceptions by printing a helpful error message
            IDictionary<int, string> names;
            try {
                names = CSVReader.ReadPairsFromCSVFile(filename);
            } catch (Exception e) {
                Console.WriteLine("Error: " + e.Message);
                return 1;
            }

            // Sort names and print the resulting list
            var sorted = SortPairs(ref names, descending);
            foreach (var entry in sorted) {
                Console.WriteLine("" + entry.Key + " " + entry.Value);
            }

            return 0;
        }
    }
}
