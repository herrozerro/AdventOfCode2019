using AdventOfCode2019;
using NUnit.Framework;

namespace AdventOfCode2019Tests
{
    public class DayOneTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Part1()
        {
            var component = 100756;

            var fuel = DayOne.GetFuelForModule(component);

            Assert.AreEqual(33583, fuel);
        }

        [Test]
        public void Part2()
        {
            var component = 100756;

            var fuel = DayOne.GetAllFuel(component);

            Assert.AreEqual(50346, fuel);
        }
    }
}