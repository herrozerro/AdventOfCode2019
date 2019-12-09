using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day07
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 7");
            part1();
            part2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void part1()
        {
            long output = 0;
            var outputs = new List<long>();
            var phasesettings = new long[] { 0, 1, 2, 3, 4 };

            var range = Enumerable.Range(0, 44444).ToList().Select(x => x.ToString("00000")).Where(x => !x.Contains("5") && !x.Contains("6") && !x.Contains("7") && !x.Contains("8") && !x.Contains("9")).ToList();
            range = range.Where(x => x.Count(s => s == '0') == 1 && x.Count(s => s == '1') == 1 && x.Count(s => s == '2') == 1 && x.Count(s => s == '3') == 1 && x.Count(s => s == '4') == 1).ToList();
            long finaloutput = 0;

            foreach (var r in range)
            {
                for (int i = 0; i < 5; i++)
                {
                    phasesettings = r.ToList()
                        .Select(x => long.Parse(x.ToString()))
                        .ToArray();
                    var code = Utilities.GetStringFromFile("Day7.txt").SplitLongArrayFromString(',');
                    //var code = new int[] { 3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0 };
                    var amp = new IntCodeVM();
                    amp.RunProgram(code, new long[] { phasesettings[i], output });
                    output = amp.outputs.Last();
                    outputs.Add(output);
                }

                if (finaloutput < outputs.Last())
                {
                    finaloutput = outputs.Last();
                }
                outputs.Clear();
                output = 0;
            }
            Console.WriteLine(finaloutput);
        }

        public static void part2()
        {
            long output = 0;
            var outputs = new List<long>();
            var phasesettings = new long[] { 0, 1, 2, 3, 4 };

            var range = Enumerable.Range(0, 99999).ToList().Select(x => x.ToString("00000")).Where(x => !x.Contains("0") && !x.Contains("1") && !x.Contains("2") && !x.Contains("3") && !x.Contains("4")).ToList();
            range = range.Where(x => x.Count(s => s == '5') == 1 && x.Count(s => s == '6') == 1 && x.Count(s => s == '7') == 1 && x.Count(s => s == '8') == 1 && x.Count(s => s == '9') == 1).ToList();
            long finaloutput = 0;

            foreach (var r in range)
            {
                phasesettings = r.ToList()
                        .Select(x => long.Parse(x.ToString()))
                        .ToArray();
                //phasesettings = new int[] { 9, 8, 7, 6, 5 };
                var amp1 = new IntCodeVM();
                var amp2 = new IntCodeVM();
                var amp3 = new IntCodeVM();
                var amp4 = new IntCodeVM();
                var amp5 = new IntCodeVM();

                var code1 = Utilities.GetStringFromFile("Day7.txt").SplitLongArrayFromString(',');
                var code2 = Utilities.GetStringFromFile("Day7.txt").SplitLongArrayFromString(',');
                var code3 = Utilities.GetStringFromFile("Day7.txt").SplitLongArrayFromString(',');
                var code4 = Utilities.GetStringFromFile("Day7.txt").SplitLongArrayFromString(',');
                var code5 = Utilities.GetStringFromFile("Day7.txt").SplitLongArrayFromString(',');

                //var code1 = new int[] { 3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5 };
                //var code2 = new int[] { 3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5 };
                //var code3 = new int[] { 3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5 };
                //var code4 = new int[] { 3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5 };
                //var code5 = new int[] { 3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5 };

                bool initSetup = true;
                //var code = Utilities.GetStringFromFile("Day7.txt").SplitIntArrayFromString(',');
                while (true)
                {
                    var isdone = 0;

                    long[] inp1 = initSetup ? new long[] { phasesettings[0], output } : new long[] { output };
                    isdone += amp1.RunProgram(code1, inp1, true);
                    output = amp1.outputs.Last();
                    //outputs.Add(output);

                    long[] inp2 = initSetup ? new long[] { phasesettings[1], output } : new long[] { output };
                    isdone += amp2.RunProgram(code2, inp2, true);
                    output = amp2.outputs.Last();
                    //outputs.Add(output);

                    long[] inp3 = initSetup ? new long[] { phasesettings[2], output } : new long[] { output };
                    isdone += amp3.RunProgram(code3, inp3, true);
                    output = amp3.outputs.Last();
                    //outputs.Add(output);

                    long[] inp4 = initSetup ? new long[] { phasesettings[3], output } : new long[] { output };
                    isdone += amp4.RunProgram(code4, inp4, true);
                    output = amp4.outputs.Last();
                    //outputs.Add(output);

                    long[] inp5 = initSetup ? new long[] { phasesettings[4], output } : new long[] { output };
                    isdone += amp5.RunProgram(code5, inp5, true);
                    output = amp5.outputs.Last();
                    outputs.Add(output);

                    if (initSetup)
                    {
                        initSetup = false;
                    }

                    if (isdone != 0)
                    {
                        break;
                    }
                }
                    
                    

                if (finaloutput < outputs.Last())
                {
                    finaloutput = outputs.Last();
                }
                outputs.Clear();
                output = 0;
            }

            Console.WriteLine(finaloutput);
        }
    }
}
