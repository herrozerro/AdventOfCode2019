using AdventOfCode2019;
using NUnit.Framework;
using System.Linq;

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
            var vm = new IntCodeVM();
            var code = new int[] { 1002, 4, 3, 4, 33 };
            vm.RunProgram(code, 0);
            Assert.AreEqual(vm.programCode[4], 99);

            code = new int[] { 1101, 100, -1, 4, 0 };
            vm.RunProgram(code, 0);
            Assert.AreEqual(vm.programCode[4], 99);
        }

        [Test]
        public void Part2()
        {
            var vm = new IntCodeVM();
            var code = new int[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 };
            vm.RunProgram(code, 8); //equal to eight?
            Assert.AreEqual(vm.outputs.Last(), 1);

            code = new int[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 };
            vm.RunProgram(code, 7); //equal to eight?
            Assert.AreEqual(vm.outputs.Last(), 0);

            code = new int[] { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 };
            vm.RunProgram(code, 7); //equal to eight?
            Assert.AreEqual(vm.outputs.Last(), 999);

            code = new int[] { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 };
            vm.RunProgram(code, 8); //equal to eight?
            Assert.AreEqual(vm.outputs.Last(), 1000);

            code = new int[] { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 };
            vm.RunProgram(code, 9); //equal to eight?
            Assert.AreEqual(vm.outputs.Last(), 1001);
        }
    }
}