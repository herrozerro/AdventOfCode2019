using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day08
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 8");
            var lines = Utilities.GetStringFromFile("Day8.txt");

            var layerCount = lines.Length / 150;

            int skip = 0;

            List<KeyValuePair<int, int>> zerosinlayers = new List<KeyValuePair<int, int>>();

            var layers = new List<KeyValuePair<int, string>>();

            for (int i = 0; i < layerCount; i++)
            {
                layers.Add(new KeyValuePair<int, string>( i + 1, lines.Substring(skip, 150) ));
                skip += 150;
            }

            var leastzeros = layers.Select(x=>new { zeroes = x.Value.Count(x => x == '0'), ones  = x.Value.Count(x => x == '1'), twos = x.Value.Count(x => x == '2'), x  }).OrderBy(x=>x.zeroes);

            char[] finaloutput = new char[150];

            foreach (var layer in layers)
            {
                for (int i = 0; i < 150; i++)
                {
                    if (finaloutput[i] != ' ' && finaloutput[i] != '█')
                    {
                        switch (layer.Value[i])
                        {
                            case '0':
                                finaloutput[i] = ' ';
                                break;
                            case '1':
                                finaloutput[i] = '█';
                                break;
                            case '2':
                            default:
                                break;
                        }
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine(string.Join("", finaloutput.ToList().Skip(i * 25).Take(25).ToList()));
            }

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }


        public static void part1()
        {

        }
    }
}
