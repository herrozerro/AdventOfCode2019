using System;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            var fuel1 = 0;

            var f = DayOne.GetAllFuel(100756);

            foreach (var component in DayOne.Inputs)
            {
                fuel1 += DayOne.GetAllFuel(component);
            }

            Console.WriteLine($"First Method Sum: {fuel1}");
        }
    }
}
