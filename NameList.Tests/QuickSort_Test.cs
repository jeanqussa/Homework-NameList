using System;
using Xunit;
using System.Collections.Generic;
using Homework.NameList;

namespace Homework.NameList.Tests
{
    public class QuickSort_Test
    {
        [Fact]
        public void SwappingWorks()
        {
            IList<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            QuickSort.Swap(ref list, 0, 1);
            Assert.Equal(2, list[0]);
            Assert.Equal(1, list[1]);
        }

        [Fact]
        public void PartitioningWorks()
        {
            IList<int> list = new List<int>();
            list.Add(4);
            list.Add(1);
            list.Add(3);
            list.Add(2);
            list.Add(5);
            QuickSort.Partition(ref list, (a, b) => a < b, 0, list.Count - 1);
            Assert.Equal(2, list[0]);
            Assert.Equal(1, list[1]);
            Assert.Equal(3, list[2]);
            Assert.Equal(4, list[3]);
            Assert.Equal(5, list[4]);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(0, true)]
        [InlineData(1, false)]
        [InlineData(1, true)]
        [InlineData(5, false)]
        [InlineData(5, true)]
        public void SortingEmptyListWorks(int count, bool descending)
        {
            IList<int> list = new List<int>();
            Random rnd = new Random();

            for (int i = 0; i < count; i++) {
                list.Add(rnd.Next(1, 1000));
            }

            QuickSort.Sort(ref list, (a, b) => descending ? (a > b) : (a < b), 0, list.Count - 1);

            Assert.Equal(count, list.Count);
            for (int i = 1; i < list.Count; i++) {
                if (descending) {
                    Assert.True(list[i] < list[i - 1]);
                } else {
                    Assert.True(list[i] > list[i - 1]);
                }
            }
        }
    }
}
