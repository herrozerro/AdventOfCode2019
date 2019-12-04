using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day04
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 4");

            var lines = Program.GetStringFromFile("Day4.txt");

            var passwords2 = ParsePasswords(new List<string>() { "112233", "123444", "111122" });
            var passwords = ParsePasswords(GeneratePasswords(lines));


            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static List<string> GeneratePasswords(string range)
        {
            var ranges = range.Split("-").Select(int.Parse).ToList();

            var allRanges = Enumerable.Range(ranges[0], ranges[1] - ranges[0]).Select(x => x.ToString("000000")).ToList();

            return allRanges;
        }

        public static List<string> ParsePasswords(List<string> passwords)
        {
            var passedPasswords = new List<string>();

            var doubledigets = new List<string>()
            {
                "00",
                "11",
                "22",
                "33",
                "44",
                "55",
                "66",
                "77",
                "88",
                "99"
            };

            foreach (var item in passwords)
            {

                if (doubledigets.Any(s => item.Contains(s)))
                {
                    bool isbad = false;
                    int character = int.Parse(item[0].ToString());
                    foreach (var c in item.Skip(1))
                    {
                        if (int.Parse(c.ToString()) >= character)
                        {
                            character = int.Parse(c.ToString());
                        }
                        else
                        {
                            isbad = true;
                            break;
                        }
                    }
                    if (!isbad)
                    {

                        foreach (var dd in doubledigets)
                        {
                            var trip = dd + dd[0];
                            if (!item.Contains(trip) && item.Contains(dd))
                            {
                                passedPasswords.Add(item);
                                break;
                            }
                            else if (item.Contains(trip))
                            {
                                Console.WriteLine(item);
                            }
                        }
                    }

                }
            }

            return passedPasswords;
        }
    }
}
