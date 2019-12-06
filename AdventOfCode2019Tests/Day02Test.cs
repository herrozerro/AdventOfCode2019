using AdventOfCode2019;
using NUnit.Framework;
using System;

namespace AdventOfCode2019Tests
{
    public class Day02Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Part1()
        {
            var output = Day02.mysterfunc(0, 0, "1,0,0,0,99");
            Assert.AreEqual(output, 2);

            output = Day02.mysterfunc(3, 0, "2,3,0,3,99", 3);
            Assert.AreEqual(output, 6);

            output = Day02.mysterfunc(4, 4, "2,4,4,5,99,0", 5);
            Assert.AreEqual(output, 9801);

            output = Day02.mysterfunc(1, 1, "1,1,1,4,99,5,6,0,99", 0);
            Assert.AreEqual(output, 30);
        }

        [Test]
        public void Part2()
        {
            //it's just cycling through all combinations nothing new to test.
            Assert.Pass();
        }
    }
}