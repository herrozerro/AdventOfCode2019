using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2019;

namespace AdventOfCode2019
{
    public static class Day05
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 5");

            var code = Utilities.GetStringFromFile("Day5.txt").SplitIntArrayFromString(',');

            //code = new int[] { 3, 0, 4, 0, 99 };

            var vm = new IntCodeVM();

            //vm.RunProgram(code, 1);

            code = Utilities.GetStringFromFile("Day5.txt").SplitIntArrayFromString(',');
            vm.RunProgram(code, 5);

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }
    }

    public class IntCodeVM
    {
        private int[] program = new int[0];


        public int[] RunProgram(int[] programCode, int input)
        {
            this.input = input;
            program = programCode;
            CurrentPositionPointer = 0;
            bool isrunning = true;
            while (isrunning)
            {
                var x = program[CurrentPositionPointer];

                parseOpCode(x);

                switch (DE)
                {
                    case "01":
                        //Console.WriteLine($"Op1: {DE}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode}), {program[CurrentPositionPointer + 3]}({A_ThirdParamMode})");
                        Op1();
                        break;
                    case "02":
                        //Console.WriteLine($"Op2: {DE}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode}), {program[CurrentPositionPointer + 3]}({A_ThirdParamMode})");
                        Op2();
                        break;
                    case "03":
                        //Console.WriteLine($"Op3: {DE}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode})");
                        Op3();
                        break;
                    case "04":
                        //Console.WriteLine($"Op4: {DE}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode})");
                        Op4();
                        break;
                    case "05":
                        //Console.WriteLine($"Op5 jump-if-true: {DE}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode})");
                        Op5();
                        break;
                    case "06":
                        //Console.WriteLine($"Op6 jump-if-false: {DE}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode})");
                        Op6();
                        break;
                    case "07":
                        //Console.WriteLine($"Op7 less than: {DE}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode}), {program[CurrentPositionPointer + 3]}({A_ThirdParamMode})");
                        Op7();
                        break;
                    case "08":
                        //Console.WriteLine($"Op8 equals: {DE}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode}), {program[CurrentPositionPointer + 3]}({A_ThirdParamMode})");
                        Op8();
                        break;
                    case "99":
                        Console.WriteLine("Program Halted Expectedly");
                        isrunning = false;
                        break;
                    default:
                        Console.WriteLine("Program Halted Unexpectedly");
                        isrunning = false;
                        break;
                }
            }

            return program;
        }

        public int CurrentPositionPointer = 0;

        public int input = 1;

        private string DE = "";
        private string C_FirstParamMode = "";
        private string B_SecondParamMode = "";
        private string A_ThirdParamMode = "";

        private void parseOpCode(int opCode)
        {
            var fullCode = opCode.ToString("00000");
            DE = fullCode[3].ToString() + fullCode[4].ToString();
            C_FirstParamMode = fullCode[2].ToString();
            B_SecondParamMode = fullCode[1].ToString();
            A_ThirdParamMode = fullCode[0].ToString();
        }

        //add
        public void Op1()
        {
            var p1 = program[CurrentPositionPointer + 1];
            var p2 = program[CurrentPositionPointer + 2];
            var p3 = program[CurrentPositionPointer + 3];

            if (C_FirstParamMode == "0")
            {
                p1 = program[p1];
            }
            if (B_SecondParamMode == "0")
            {
                p2 = program[p2];
            }

            program[p3] = p1 + p2;
            CurrentPositionPointer += 4;
        }

        //multiply
        public void Op2()
        {
            var p1 = program[CurrentPositionPointer + 1];
            var p2 = program[CurrentPositionPointer + 2];
            var p3 = program[CurrentPositionPointer + 3];

            if (C_FirstParamMode == "0")
            {
                p1 = program[p1];
            }
            if (B_SecondParamMode == "0")
            {
                p2 = program[p2];
            }

            program[p3] = p1 * p2;
            CurrentPositionPointer += 4;
        }

        //read input
        public void Op3()
        {
            Console.WriteLine("Input Read!");
            var p1 = program[CurrentPositionPointer + 1];
            program[p1] = input;
            CurrentPositionPointer += 2;
        }

        //output
        public void Op4()
        {
            var p1 = program[CurrentPositionPointer + 1];

            if (C_FirstParamMode == "0")
            {
                p1 = program[p1];
            }

            Console.WriteLine(p1);
            CurrentPositionPointer += 2;
        }

        //jump if true 
        //two parameters
        public void Op5()
        {
            var p1 = program[CurrentPositionPointer + 1];
            var p2 = program[CurrentPositionPointer + 2];

            if (C_FirstParamMode == "0")
            {
                p1 = program[p1];
            }
            if (B_SecondParamMode == "0")
            {
                p2 = program[p2];
            }

            if (p1 != 0)
            {
                CurrentPositionPointer = p2;
            }
            else
            {
                CurrentPositionPointer += 3;
            }

        }

        //jump if false
        //two parameters
        public void Op6()
        {
            var p1 = program[CurrentPositionPointer + 1];
            var p2 = program[CurrentPositionPointer + 2];

            if (C_FirstParamMode == "0")
            {
                p1 = program[p1];
            }
            if (B_SecondParamMode == "0")
            {
                p2 = program[p2];
            }

            if (p1 == 0)
            {
                CurrentPositionPointer = p2;
            }
            else
            {
                CurrentPositionPointer += 3;
            }
        }

        //is less than
        //three parameters
        public void Op7()
        {
            var p1 = program[CurrentPositionPointer + 1];
            var p2 = program[CurrentPositionPointer + 2];
            var p3 = program[CurrentPositionPointer + 3];

            if (C_FirstParamMode == "0")
            {
                p1 = program[p1];
            }
            if (B_SecondParamMode == "0")
            {
                p2 = program[p2];
            }

            program[p3] = (p1 < p2 ? 1 : 0);
            CurrentPositionPointer += 4;
        }

        //equals
        //three parameters
        public void Op8()
        {
            var p1 = program[CurrentPositionPointer + 1];
            var p2 = program[CurrentPositionPointer + 2];
            var p3 = program[CurrentPositionPointer + 3];

            if (C_FirstParamMode == "0")
            {
                p1 = program[p1];
            }
            if (B_SecondParamMode == "0")
            {
                p2 = program[p2];
            }

            program[p3] = (p1 == p2 ? 1 : 0);
            CurrentPositionPointer += 4;
        }
    }
}
