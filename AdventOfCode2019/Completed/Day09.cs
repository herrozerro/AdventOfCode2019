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

            P1();
            P2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void P1()
        {
            var lines = Utilities.GetStringFromFile("Day9.txt").SplitLongArrayFromString(',');

            var config = new IntCodeVMConfiguration();
            //config = new IntCodeVMConfiguration { Logging = true, FriendlyLogging = false };
            var vm = new IntCodeVMOLD(config);

            vm.RunProgram(lines, new long[] { 1 });

            Console.WriteLine(vm.outputs.Peek());
        }

        public static void P2()
        {
            var lines = Utilities.GetStringFromFile("Day9.txt").SplitLongArrayFromString(',');
            var config = new IntCodeVMConfiguration();
            //config = new IntCodeVMConfiguration { Logging = true, FriendlyLogging = false };

            var vm = new IntCodeVMOLD(config);

            vm.RunProgram(lines, new long[] { 2 });

            Console.WriteLine(vm.outputs.Peek());
        }
    }
}
