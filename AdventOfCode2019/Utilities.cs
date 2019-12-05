﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Utilities
    {
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

        public static int[] SplitIntArrayFromString(this string strArray, char delimiter)
        {
            return strArray.Split(delimiter).Select<string, int>(int.Parse).ToArray();
        }
    }
}