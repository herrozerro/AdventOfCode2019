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

            p1();
            p2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void p1()
        {
            var lines = Utilities.GetStringFromFile("Day9.txt").SplitLongArrayFromString(',');

            var vm = new IntCodeVM();

            vm.RunProgram(lines, new long[] { 1 });

            Console.WriteLine(vm.outputs[0]);
        }

        public static void p2()
        {
            var lines = Utilities.GetStringFromFile("Day9.txt").SplitLongArrayFromString(',');

            var vm = new IntCodeVM();

            vm.RunProgram(lines, new long[] { 2 });

            Console.WriteLine(vm.outputs[0]);
        }
    }
}
