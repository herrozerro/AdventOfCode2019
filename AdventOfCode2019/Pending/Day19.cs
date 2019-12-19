using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AdventOfCode2019
{
    public static class Day19
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 19");

           // P1();
            P2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        static void P1()
        {
            var lines = Utilities.GetStringFromFile("Day19.txt").SplitLongArrayFromString(',');

            var vm = new IntCodeVM(new IntCodeVMConfiguration() { });
            vm.WriteProgram(lines);

            char[,] charmap = new char[50, 50];

            Queue<KeyValuePair<int, int>> coords = new Queue<KeyValuePair<int, int>>();

            Queue<KeyValuePair<int, int>> usedcoords = new Queue<KeyValuePair<int, int>>();

            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    coords.Enqueue(new KeyValuePair<int, int>(i, j));
                }
            }

            while (coords.Count > 0)
            {
                while (vm.RunProgram() == HALTTYPE.HALT_WAITING)
                {

                    if (coords.Count == 0)
                    {
                        break;
                    }
                    var coord = coords.Dequeue();
                    vm.WriteInput(coord.Key);
                    vm.WriteInput(coord.Value);
                    usedcoords.Enqueue(coord);
                }
            }
            


            long count = 0;
            while (vm.outputs.Count > 0)
            {
                var output = vm.outputs.Dequeue();
                var outcoord = usedcoords.Dequeue();
                charmap[outcoord.Key, outcoord.Value] = output.ToString()[0];
                count += output;
            }



        }

        static void P2()
        {
            var lines = Utilities.GetStringFromFile("Day19.txt").SplitLongArrayFromString(',');

            var vm = new IntCodeVM(new IntCodeVMConfiguration() { });

            var rows = 500;
            char[,] charmap = new char[5000, 5000];

            bool hitbeam = false;
            long beamwidth = 0;
            long BeamStart = 0;

            for (int i = 4; i < 5000; i++)
            {
                for (int j = 0; j < 5000; j++)
                {
                    var output = CheckBeam(j, i);
                    if (output == 1)
                    {
                        charmap[i, j] = '#';
                        //Draw(charmap);

                        //check if 99 up is valid
                        if (i > 100)
                        {
                            output = CheckBeam(j, i - 100);
                            if (output == 1)
                            {
                                if (CheckBeam(j+99, i - 100)==1)
                                {
                                    Console.Write($"{j} , {i - 100}");
                                    return;
                                }
                                
                            }
                        }
                        


                        i++;
                        j--;
                        continue;
                    }
                }
            }
        }
        static void Draw(char[,] grid)
        {
            Console.Clear();
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                var row = "";
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    row = row + (grid[j, i] == '\0' ? '.' : grid[j, i]);
                }
                Console.WriteLine(row);
            }
            Thread.Sleep(100);
            //Console.WriteLine($"Score: {score} ---- Blockes Remaining: {blockcount}");
        }

        static int CheckBeam(int x, int y)
        {
            var lines = Utilities.GetStringFromFile("Day19.txt").SplitLongArrayFromString(',');

            var vm = new IntCodeVM(new IntCodeVMConfiguration() { });
            
            vm.WriteProgram(lines);
            while (vm.RunProgram() == HALTTYPE.HALT_WAITING)
            {
                vm.WriteInput(x);
                vm.WriteInput(y);
            }

            return (int)vm.outputs.Dequeue();
        }
    }
}
