using System;
using System.Collections;
using System.Collections.Generic;

namespace Homework.NameList
{
    public class QuickSort
    {
        /// <summary>
        /// Swaps two elements in a <c>List</c>.
        /// </summary>
        /// <param name="a">Index of first element.</param>
        /// <param name="b">Index of second element.</param>
        public static void Swap<T>(ref IList<T> elements, int a, int b) {
            var temp = elements[a];
            elements[a] = elements[b];
            elements[b] = temp;
        }

        /// <summary>
        /// <para>
        /// Partitions a list using the <a href="https://en.wikipedia.org/wiki/Quicksort#Hoare_partition_scheme">Hoare partition scheme</a>.
        /// </para>
        /// <para>
        /// Picks a value from <c>elements</c> (called a pivot) and partitions the list such that all
        /// elements with values less than the pivot are placed to its left, and all values greater
        /// are placed to its right.
        /// </para>
        /// </summary>
        /// <param name="elements">List of elements to partition.</param>
        /// <param name="shouldBeBefore">A function which, when given two values, returns whether
        /// the first value should appear before the second value in the sorted list.</param>
        /// <param name="left">Index of element in array at which the partition begins.</param>
        /// <param name="right">Index of element in array at which the partition ends.</param>
        /// <returns>The new index of the pivot.</returns>
        public static int Partition<T>(ref IList<T> elements, Func<T, T, bool> shouldBeBefore, int left, int right) {
            var pivot = elements[(left + right) / 2];

            int i = left - 1;
            int j = right + 1;
            
            while (true) {
                do {
                    i += 1;
                } while (shouldBeBefore(elements[i], pivot) && !elements[i].Equals(pivot));

                do {
                    j -= 1;
                } while (shouldBeBefore(pivot, elements[j]) && !elements[j].Equals(pivot));

                if (i >= j) {
                    return j;
                }

                Swap(ref elements, i, j);
            }
        }

        /// <summary>
        /// <para>
        /// Sorts a list, in-place, using the quicksort algorithm. Since the sorting is done in-place,
        /// it is an unstable sorting algorithm.
        /// </para>
        /// <para>
        /// See <a href="https://en.wikipedia.org/wiki/Quicksort">Quicksort</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="elements">List of elements to partition.</param>
        /// <param name="shouldBeBefore">A function which, when given two values, returns whether
        /// the first value should appear before the second value in the sorted list.</param>
        /// <param name="left">Index of element in array at which the partition begins.</param>
        /// <param name="right">Index of element in array at which the partition ends.</param>
        /// <returns>The new index of the pivot.</returns>
        public static void Sort<T>(ref IList<T> elements, Func<T, T, bool> shouldBeBefore, int left, int right) {
            if (left >= right) return;
            int pivot = Partition(ref elements, shouldBeBefore, left, right);
            Sort(ref elements, shouldBeBefore, left, pivot);
            Sort(ref elements, shouldBeBefore, pivot + 1, right);
        }
    }
}
