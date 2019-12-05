using AdventOfCode2019;
using NUnit.Framework;
using System.Collections.Generic;

namespace AdventOfCode2019Tests
{
    public class Day04Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Part1()
        {
            var range = Day04.GeneratePasswords("1-10");

            Assert.AreEqual(range.Count, 10);

            var input = new List<string>() { "111111", "223450", "123789" };

            var results = Day04.ParsePasswordsP1(input);

            Assert.AreEqual(results, new List<string>() { "111111" });
        }

        [Test]
        public void Part2()
        {
            var input = new List<string>() { "112233", "123444", "111122" };

            var results = Day04.ParsePasswordsP2(input);

            Assert.AreEqual(results, new List<string>() { "112233", "111122" });
        }
    }
}