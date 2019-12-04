using System;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            //Day01.RunDay();
            //Day02.RunDay();
            //Day02.RunAllComobos();
            //Day03.RunDay();
            Day04.RunDay();
            Day05.RunDay();
            Day06.RunDay();
            Day07.RunDay();
            Day08.RunDay();
            Day09.RunDay();
            Day10.RunDay();
            Day11.RunDay();
            Day12.RunDay();
            Day13.RunDay();
            Day14.RunDay();
            Day15.RunDay();
            Day16.RunDay();
            Day17.RunDay();
            Day18.RunDay();
            Day19.RunDay();
            Day20.RunDay();
            Day21.RunDay();
            Day22.RunDay();
            Day23.RunDay();
            Day24.RunDay();
            Day25.RunDay();
            
        }

        public static string[] GetLinesFromFile(string filename)
        {
            var lines = System.IO.File.ReadAllLines("Data\\" + filename);

            return lines;
        }

        public static string GetStringFromFile(string filename)
        {
            var lines = System.IO.File.ReadAllText("Data\\" + filename);

            return lines;
        }

    }
}
