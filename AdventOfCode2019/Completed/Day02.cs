using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day02
    {
        public static void RunAllComobos()
        {
            bool isfound = true;
            while (isfound)
            {
                for (int i = 0; i < 99; i++)
                {
                    for (int j = 0;  j < 99;  j++)
                    {
                        code = (strcode ?? "").Split(',').Select<string, int>(int.Parse).ToArray();
                        var fun = mysterfunc(i, j);
                        isfound = 19690720 != fun;
                        Console.WriteLine($"x{i} y{j} value = {fun}");
                        if (isfound == false)
                        {
                            Console.WriteLine($"FOUND! x{i} y{j}");

                        }
                    }
                }
            }
        }

        public static void RunDay()
        {
            Console.WriteLine("Day 2");
            
            Console.WriteLine(string.Concat(code),',');
            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }


        public static int mysterfunc(int i, int j)
        {
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
        static string strcode = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,10,1,19,2,9,19,23,2,13,23,27,1,6,27,31,2,6,31,35,2,13,35,39,1,39,10,43,2,43,13,47,1,9,47,51,1,51,13,55,1,55,13,59,2,59,13,63,1,63,6,67,2,6,67,71,1,5,71,75,2,6,75,79,1,5,79,83,2,83,6,87,1,5,87,91,1,6,91,95,2,95,6,99,1,5,99,103,1,6,103,107,1,107,2,111,1,111,5,0,99,2,14,0,0";
        //public static int[] code = new int[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 };
        static int[] code;

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

        //public static bool MysteryFunc(int x, int y)
        //{
        //    var x2 = 19690720;
        //    var codecopy = (int[])code.Clone();
        //    codecopy[1] = x;
        //    codecopy[2] = y;
        //    bool isrunning = true;
        //    int pos = 0;
        //    while (isrunning)
        //    {
        //        pos = code[CurrentPosition];

        //        switch (pos)
        //        {
        //            case 1:
        //                Op1();
        //                break;
        //            case 2:
        //                Op2();
        //                break;
        //            case 99:
        //            default:
        //                isrunning = false;
        //                break;
        //        }
        //    }

        //    return x2 == code[0];
        //}
    }
}
