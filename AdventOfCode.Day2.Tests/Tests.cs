using AdventOfCode.Day2;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class Tests
    {

        [Test]
        public void BoxScan_GetBoxIds_ReadsDataFromFile()
        {
            var scan = new BoxScan();

            var ids = scan.GetBoxIds();

            Assert.IsTrue(ids.Any());
        }

        [Test]
        public void BoxScan_CountTwiceAppearingLetters()
        {
            var ids = GetExampleData();
            var scan = new BoxScan();

            var actual = scan.CountMultipleAppearingLetters(ids, 2);
            var expected = 4;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BoxScan_CountThreeTimesAppearingLetters()
        {
            var ids = GetExampleData();
            var scan = new BoxScan();

            var actual = scan.CountMultipleAppearingLetters(ids, 3);
            var expected = 3;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BoxScan_CheckSum()
        {
            var scan = new BoxScan();
            var first = 4;
            var second = 3;

            var actual = scan.GenerateBoxCheckSum(first, second);
            var expected = 12;

            Assert.AreEqual(expected, actual);
        }

        private List<string> GetExampleData()
        {
            return new List<string>
            {
                "abcdef",
                "bababc",
                "abbcde",
                "abcccd",
                "aabcdd",
                "abcdee",
                "ababab"
            };
        }
    }
}