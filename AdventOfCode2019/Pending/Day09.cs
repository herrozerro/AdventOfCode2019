using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day09
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 9");

            var lines = Utilities.GetStringFromFile("Day9.txt").SplitLongArrayFromString(',');
            //lines = new long[]{ 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 };
            //lines = new long[]{ 1102, 34915192, 34915192, 7, 4, 7, 99, 0 };
            //lines = new long[]{ 104, 1125899906842624, 99 };
            
            var vm = new IntCodeVM();

            vm.RunProgram(lines, new long[] { 2 });

            Console.WriteLine(vm.outputs[0]);

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }
    }
}
