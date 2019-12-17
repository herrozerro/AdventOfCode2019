using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventOfCode2019
{
    public static class Day17
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 17");

            //P1();
            P2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void P1()
        {
            var lines = Utilities.GetStringFromFile("Day17.txt").SplitLongArrayFromString(',');

            var vm = new IntCodeVM(new IntCodeVMConfiguration() { });
            vm.WriteProgram(lines);

            char[,] charmap = new char[41, 49];

            while (vm.RunProgram() == HALTTYPE.HALT_WAITING)
            {
                break;
            }

            string map = "";

            int J = 0;
            int I = 0;
            while (vm.outputs.Count > 0)
            {
                char tile;
                tile = (char)vm.outputs.Dequeue();
                map += tile;
                if (tile == 10)
                {
                    J++;
                    I = 0;
                }
                else
                {
                    charmap[J, I] = tile;
                    I++;
                }
            }

            int score = 0;
            for (int i = 1; i < charmap.GetLength(1) - 1; i++)
            {
                for (int j = 1; j < charmap.GetLength(0) - 1; j++)
                {
                    if (charmap[j, i] == '#' && (charmap[j, i - 1] == '#' && charmap[j, i + 1] == '#' && charmap[j - 1, i] == '#' && charmap[j + 1, i] == '#'))
                    {
                        score += i * j;
                    }
                }
            }

            Console.Write(map);
        }

        public static void P2()
        {
            var lines = Utilities.GetStringFromFile("Day17.txt").SplitLongArrayFromString(',');

            var vm = new IntCodeVM(new IntCodeVMConfiguration() { FriendlyLogging = false });
            vm.WriteProgram(lines);
            vm.WriteMemory(0, 2);

            var input = "65,44,65,44,66,44,67,44,66,44,67,44,66,44,67,44,67,44,65,10,76,44,49,48,44,82,44,56,44,82,44,56,10,76,44,49,48,44,76,44,49,50,44,82,44,56,44,82,44,49,48,10,82,49,48,44,76,49,50,44,82,49,48,10".Split(",").Select(int.Parse).ToList();
            var main = "65,44,65,44,66,44,67,44,66,44,67,44,66,44,67,44,67,44,65,10".Split(",").Select(int.Parse).ToList();
            var A = "76,44,49,48,44,82,44,56,44,82,44,56,10".Split(",").Select(int.Parse).ToList();
            var B = "76,44,49,48,44,76,44,49,50,44,82,44,56,44,82,44,49,48,10".Split(",").Select(int.Parse).ToList();
            var C = "82,44,49,48,44,76,44,49,50,44,82,44,49,48,10".Split(",").Select(int.Parse).ToList();
            //foreach (var inp in input)
            //{
            //    vm.WriteInput(inp);
            //}
            //vm.WriteInput(110);
            //vm.WriteInput(10);

            var inpPhase = 0;

            while (vm.RunProgram() == HALTTYPE.HALT_WAITING)
            {
                while (vm.outputs.Count > 0)
                {
                    Console.Write((char)vm.outputs.Dequeue());
                }
                switch (inpPhase)
                {
                    case 0:
                        foreach (var inp in main)
                        {
                            vm.WriteInput(inp);
                        }
                        inpPhase++;
                        break;
                    case 1:
                        foreach (var inp in A)
                        {
                            vm.WriteInput(inp);
                        }
                        inpPhase++;
                        break;
                    case 2:
                        foreach (var inp in B)
                        {
                            vm.WriteInput(inp);
                        }
                        inpPhase++;
                        break;
                    case 3:
                        foreach (var inp in C)
                        {
                            vm.WriteInput(inp);
                        }
                        inpPhase++;
                        break;
                    case 4:
                        vm.WriteInput((int)'Y');
                        vm.WriteInput(10);
                        inpPhase++;
                        break;
                }
            }

            var x = vm.outputs.First();
            var y = vm.outputs.Last();
            int e = 0;
            var s = "";
            while (vm.outputs.Count > 0)
            {
                s += (char)vm.outputs.Dequeue();
                e++;
                if (e == 2051)
                {
                    Console.Clear();
                    Console.WriteLine(s);
                    s = "";
                    e = 0;
                    Thread.Sleep(250);
                }
            }

            while (vm.RunProgram() == HALTTYPE.HALT_WAITING)
            { }


        }
    }
}
