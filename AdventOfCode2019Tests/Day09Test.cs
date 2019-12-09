using AdventOfCode2019;
using NUnit.Framework;
using System.Linq;

namespace AdventOfCode2019Tests
{
    public class Day09Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Part1()
        {
            var lines = new long[]{ 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 };
            var vm = new IntCodeVM();
            vm.RunProgram(lines, new long[] { 0 });
            Assert.IsTrue(vm.outputs.ToArray().SequenceEqual(lines));

            lines = new long[]{ 1102, 34915192, 34915192, 7, 4, 7, 99, 0 };
            vm.RunProgram(lines, new long[] { 0 }, false, true);
            Assert.AreEqual(vm.outputs.Last().ToString(), "1219070632396864");

            lines = new long[]{ 104, 1125899906842624, 99 };
            vm.RunProgram(lines, new long[] { 0 }, false, true);
            Assert.AreEqual(vm.outputs.Peek().ToString(), "1125899906842624");
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}