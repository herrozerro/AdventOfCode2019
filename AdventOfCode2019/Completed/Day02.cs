using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day02
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 2");
            
            var lines = Utilities.GetStringFromFile("Day2.txt");

            var output = mysterfunc(12,2, lines);
            Console.WriteLine(output);
            
            var outputstr = RunAllComobos(lines);
            Console.WriteLine(outputstr);
            
            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }
        
        public static string RunAllComobos(string strcode)
        {
            bool isfound = true;
            while (isfound)
            {
                for (int i = 0; i < 99; i++)
                {
                    for (int j = 0;  j < 99;  j++)
                    {
                        var fun = mysterfunc(i, j, strcode);
                        isfound = 19690720 != fun;
                        Console.WriteLine($"x{i} y{j} value = {fun}");
                        if (isfound == false)
                        {
                            Console.WriteLine($"FOUND! x{i} y{j}");
                            return $"{i}{j}";
                        }
                    }
                }
            }
            return "NOT FOUND";
        }

        public static int mysterfunc(int i, int j, string strcode)
        {
            code = (strcode ?? "").Split(',').Select<string, int>(int.Parse).ToArray();
            CurrentPosition = 0;
            bool isrunning = true;
            int x = 0;
            code[1] = i;
            code[2] = j;
            while (isrunning)
            {
                x = code[CurrentPosition];

                switch (x)
                {
                    case 1:
                        Op1();
                        break;
                    case 2:
                        Op2();
                        break;
                    case 99:
                    default:
                        isrunning = false;
                        break;
                }
            }

            return code[0];
        }
        
        public static int[] code;
        public static int CurrentPosition = 0;

        public static void Op1()
        {
            var x1pos = code[CurrentPosition + 1];
            var x2pos = code[CurrentPosition + 2];
            var xstorepos = code[CurrentPosition + 3];
            code[xstorepos] = code[x1pos] + code[x2pos];
            CurrentPosition += 4;
        }

        public static void Op2()
        {
            var x1pos = code[CurrentPosition + 1];
            var x2pos = code[CurrentPosition + 2];
            var xstorepos = code[CurrentPosition + 3];
            code[xstorepos] = code[x1pos] * code[x2pos];
            CurrentPosition += 4;
        }
    }
}
