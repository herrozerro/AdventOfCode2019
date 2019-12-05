using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day01
    {
        public static void RunDay()
        {
            var FuelForComponents = 0;
            var FuelForWholeRocket = 0;
            
            var lines = Program.GetStringFromFile("Day2.txt");
            
            var inputs = lines.Split(',').Select<string, int>(int.Parse).ToList();
            
            foreach (var component in inputs)
            {
                FuelForComponents += GetFuelForModule(component);
            }

            foreach (var component in inputs)
            {
                FuelForWholeRocket += GetAllFuelRecursivly(component);
            }
            Console.WriteLine("Day 1");
            Console.WriteLine($"Part 1, Fuel for rocket: {FuelForComponents}");
            Console.WriteLine($"Part 2, Fuel for whole rocket + fuel: {FuelForWholeRocket}");
            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static int GetFuelForModule(int componentMass)
        {
            var fuel = Math.Floor((double)componentMass / 3) - 2;

            return (int)fuel;
        }

        public static int GetAllFuelRecursivly(int mass)
        {
            var fuelformass = GetFuelForModule(mass);

            if (fuelformass <= 0)
            {
                return 0;
            }

            return fuelformass + GetAllFuelRecursivly(fuelformass);
        }

    }
}
