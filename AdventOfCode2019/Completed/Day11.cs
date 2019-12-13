using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day11
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 11");

            P1();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        static void P1()
        {
            var code = Utilities.GetStringFromFile("Day11.txt").SplitLongArrayFromString(',');
            var paintedPoints = new List<string>();

            var vm = new IntCodeVMOLD(new IntCodeVMConfiguration() {});
            var grid = new int[100, 100];
            var curPos = new int[2] { 50, 50 };
            var currentColor = 0;
            var input = new long[1] { 1 };
            var state = 0;
            int facing = 90;

            while (true)
            {
                //get color to paint
                state = vm.RunProgram(code, input, true);
                if (state == 1)
                {
                    var ls = paintedPoints.Distinct().ToList();
                    Draw(grid);
                    break;
                }
                var paint = vm.outputs.Dequeue();
                

                //get direction to turn
                state = vm.RunProgram(code, input, true);
                if (state == 1)
                {
                    break;
                }
                var turnDir = vm.outputs.Dequeue();
                var turnAngle = turnDir == 0 ? -90 : 90;
                //paint
                grid[curPos[0], curPos[1]] = (int)paint;

                //turn
                facing += turnAngle;
                facing = facing < 0 ? facing + 360 : facing; //less than 0, add 360
                facing = facing == 360 ? 0 : facing; //360, set to 0

                //Move
                switch (facing)
                {
                    case 90:
                        //up
                        curPos[0]++;
                        break;
                    case 270:
                        //down
                        curPos[0]--;
                        break;
                    case 180:
                        //right
                        curPos[1]++;
                        break;
                    case 0:
                        //left
                        curPos[1]--;
                        break;
                    default:
                        break;
                }

                paintedPoints.Add($"{curPos[0]}, {curPos[1]}");

                //check to make sure we are on the grid
                if (curPos[0] > 99 || curPos[0] < 0 || curPos[1] > 99 || curPos[1] < 0)
                {
                    throw new Exception("Fail!");
                }

                currentColor = grid[curPos[0], curPos[1]];
                input[0] = currentColor;
            }
        }

        static void Draw(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                var row = "";
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    row = row + (grid[i,j] == 0 ? " " : "█");
                }
                Console.WriteLine(row);
            }
        }
    }
}
