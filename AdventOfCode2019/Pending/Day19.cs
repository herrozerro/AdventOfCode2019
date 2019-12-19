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
            char[,] charmap = new char[1200, 1200];

            bool hitbeam = false;
            int beamstart = 0;
            int drawx = 0;
            int drawy = 0;

            for (int i = 4; i < 1200; i++)
            {
                for (int j = 0; j < 1200; j++)
                {
                    var output = CheckBeam(j, i);
                    if (output == 1)
                    {
                        if (!hitbeam)
                        {
                            hitbeam = true;
                            beamstart = j - 2;
                        }
                        
                        charmap[i, j] = '#';
                        //Draw(charmap);

                        //check if 99 up is valid
                        //if (i > 100)
                        //{
                        //    output = CheckBeam(j, i - 9);
                        //    if (output == 1)
                        //    {
                        //        if (CheckBeam(j + 9, i - 9) == 1)
                        //        {
                        //            Draw(charmap, j - 5, i - 20, 12, 12);
                        //            Console.Write($"{j} , {i - 10}");
                        //            return;
                        //        }

                        //    }
                        //}
                    }
                    else
                    {
                        if (hitbeam)
                        {
                            hitbeam = false;
                            j = beamstart;
                            i++;
                            if (i == 1200)
                            {
                                break;
                            }
                            continue;
                        }
                    }

                }
            }

            string map = "";

            for (int i = 0; i < charmap.GetLength(1); i++)
            {
                for (int j = 0; j < charmap.GetLength(0); j++)
                {
                    map += (charmap[j, i] == '\0' ? '.' : charmap[j, i]);
                }
                map += Environment.NewLine;
            }


            Console.WriteLine(map);
            //for (int i = 101; i < 120; i++)
            //{
            //    for (int j = 70; j < 100; j++)
            //    {
            //        var output = CheckBeam(j, i);
            //        if (output == 1)
            //        {
            //            charmap[i, j] = '#';
            //        }
            //    }
            //}

            //Draw(charmap, 101, 70, 20, 20);
        }
        static void Draw(char[,] grid, int startx = 0, int starty = 0, int endy = 0, int endx = 0)
        {
            Console.Clear();
            for (int i = starty; i < starty + (endy == 0 ? grid.GetLength(1) : endy); i++)
            {
                var row = "";
                for (int j = startx; j < starty + (endx == 0 ? grid.GetLength(0) : endx); j++)
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
