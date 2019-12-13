using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AdventOfCode2019
{
    public static class Day13
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 13");

            P1();
            P2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        static long[] instruction = new long[3];
        static long[] input = new long[1];
        static long score = 0;
        static long ballPos = 0;
        static long paddlepos = 0;
        static long blockcount = 0;
        static char[,] grid = new char[37, 22];

        static void P1()
        {
            var lines = Utilities.GetStringFromFile("Day13.txt").SplitLongArrayFromString(',');

            var vm = new IntCodeVM(new IntCodeVMConfiguration() { });
            vm.WriteProgram(lines);
            //vm.WriteMemory(0, 1);
            vm.RunProgram();
            processOutputs(vm);
            Draw(grid);

            var blockcount = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 'B')
                    {
                        blockcount++;
                    }

                }
            }

        }

        static void P2()
        {
            var lines = Utilities.GetStringFromFile("Day13.txt").SplitLongArrayFromString(',');

            var vm = new IntCodeVM(new IntCodeVMConfiguration() { });
            vm.WriteProgram(lines);
            vm.WriteMemory(0, 2);
            processOutputs(vm);
            //Draw(grid);

            while (vm.RunProgram() == HALTTYPE.HALT_WAITING)
            {
                processOutputs(vm);

                if (ballPos < paddlepos)
                {
                    vm.WriteInput(-1);
                }
                else if (ballPos > paddlepos)
                {
                    vm.WriteInput(1);
                }
                else
                {
                    vm.WriteInput(0);
                }
                Draw(grid);
                Thread.Sleep(5);
                if (blockcount == 0)
                {
                    break;
                }
            }
            processOutputs(vm);
            Draw(grid);
        }

        static void processOutputs(IntCodeVM vm)
        {
            blockcount = 0;
            while (vm.outputs.Count > 0)
            {
                var x = vm.outputs.Dequeue();
                var y = vm.outputs.Dequeue();
                var tile = vm.outputs.Dequeue();

                if (x == -1 && y == 0)
                {
                    /* score output, not screen */
                    score = tile;
                }
                else
                {
                    char writeTile = '_';
                    switch (tile)
                    {
                        case 0:
                            writeTile = '█';
                            break;
                        case 1:
                            writeTile = 'X';
                            break;
                        case 2:
                            writeTile = 'B';
                            blockcount++;
                            break;
                        case 3:
                            writeTile = '-';
                            paddlepos = x;
                            break;
                        case 4:
                            writeTile = 'O';
                            ballPos = x;
                            break;
                        default:
                            break;
                    }

                    //write tile to array
                    grid[x, y] = writeTile;
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
                    row = row + (grid[j, i] == '\0' ? '`' : grid[j, i]);
                }
                Console.WriteLine(row);
            }
            Console.WriteLine($"Score: {score} ---- Blockes Remaining: {blockcount}");
        }
    }
}
