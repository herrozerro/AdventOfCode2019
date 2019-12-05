using AdventOfCode2019;
using NUnit.Framework;

namespace AdventOfCode2019Tests
{
    public class Day05Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Part1()
        {
            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            var vm = new IntCodeVM();
            var code = new int[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 };
            vm.RunProgram(code, 8); //equal to eight?
            code = new int[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 };
            vm.RunProgram(code, 7); //equal to eight?


            Assert.Pass();
        }
    }
}