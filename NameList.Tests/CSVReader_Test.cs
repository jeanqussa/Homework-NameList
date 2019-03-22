using System;
// using System.IO;
using Xunit;
using System.Collections;
using Homework.NameList;

namespace Homework.NameList.Tests
{
    public class CSVReader_Test
    {
        [Fact]
        public void ParseLineWorks()
        {
            var kv1 = CSVReader.ParseLine("My Name;123");
            var kv2 = CSVReader.ParseLine("Another Name;-5");

            Assert.Equal(123, kv1.Key);
            Assert.Equal("My Name", kv1.Value);
            Assert.Equal(-5, kv2.Key);
            Assert.Equal("Another Name", kv2.Value);
        }

        [Theory]
        [InlineData("Some Name-12234")]
        [InlineData("Some Name;a123")]
        [InlineData("123")]
        [InlineData("JIiosefh")]
        public void ParseLineThrowsExceptionOnInvalidInput(string input)
        {
            Assert.Throws<FormatException>(() => CSVReader.ParseLine(input));
        }
    }
}
