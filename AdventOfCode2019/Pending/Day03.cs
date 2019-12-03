using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2019;

namespace AdventOfCode2019
{
    public static class Day03
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 3");

            Console.WriteLine();

            var lines = Program.GetLinesFromFile("Day3.txt");

            var circuit1 = new Circuit();
            circuit1.runPath(lines[0].Split(',').ToList());

            var circuit2 = new Circuit();
            circuit2.runPath(lines[1].Split(',').ToList());

            var matches = circuit1.path.Intersect(circuit2.path).ToList();


            var matchlist = new List<KeyValuePair<string,int>>();
            foreach (var match in matches)
            {
                var cind1 = circuit1.path.FindIndex(x => x == match);
                var cind2 = circuit2.path.FindIndex(x => x == match);
                matchlist.Add(new KeyValuePair<string, int>(match,cind1 + cind2));
            }
            var ms1 = matchlist.OrderBy(x => x.Value).ToList();


            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }


        
    }

    public class Circuit
    {
        public List<string> path = new List<string>() { "0,0" };

        public void runPath(List<string> pathPattern)
        {
            foreach (var segment in pathPattern)
            {
                var direction = segment[0];
                var steps = int.Parse(segment.Remove(0, 1));
                switch (direction)
                {
                    case 'U':
                        for (int i = 0; i < Math.Abs(steps); i++)
                        {
                            path.Add($"{path.Last().Split(',')[0]},{int.Parse(path.Last().Split(',')[1]) + 1}");
                        }
                        break;
                    case 'D':
                        for (int i = 0; i < Math.Abs(steps); i++)
                        {
                            path.Add($"{path.Last().Split(',')[0]},{int.Parse(path.Last().Split(',')[1]) - 1}");
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < Math.Abs(steps); i++)
                        {
                            path.Add($"{int.Parse(path.Last().Split(',')[0]) + 1},{path.Last().Split(',')[1]}");
                        }
                        break;
                    case 'L':
                        for (int i = 0; i < Math.Abs(steps); i++)
                        {
                            path.Add($"{int.Parse(path.Last().Split(',')[0]) - 1},{path.Last().Split(',')[1]}");
                        }
                        break;
                }


            }
        }
    }
}
