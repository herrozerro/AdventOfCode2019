﻿using System;
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
            var vm = new IntCodeVMDay5();
            vm.RunProgram(code, 1);
            Console.WriteLine(vm.outputs.Last());


            code = Utilities.GetStringFromFile("Day5.txt").SplitIntArrayFromString(',');
            vm.RunProgram(code, 5);
            Console.WriteLine(vm.outputs.Last());

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }
    }

    public class IntCodeVMDay5
    {
        public List<int> outputs = new List<int>();

        private int[] program = new int[0];

        public int[] programCode
        {
            get { return program; }
        }

        int input = 1;

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

                switch (DE_OpCode)
                {
                    case 1:
                        //Console.WriteLine($"Op1: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode}), {program[CurrentPositionPointer + 3]}({A_ThirdParamMode})");
                        Op1();
                        break;
                    case 2:
                        //Console.WriteLine($"Op2: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode}), {program[CurrentPositionPointer + 3]}({A_ThirdParamMode})");
                        Op2();
                        break;
                    case 3:
                        //Console.WriteLine($"Op3: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode})");
                        Op3();
                        break;
                    case 4:
                        //Console.WriteLine($"Op4: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode})");
                        Op4();
                        break;
                    case 5:
                        //Console.WriteLine($"Op5 jump-if-true: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode})");
                        Op5();
                        break;
                    case 6:
                        //Console.WriteLine($"Op6 jump-if-false: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode})");
                        Op6();
                        break;
                    case 7:
                        //Console.WriteLine($"Op7 less than: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode}), {program[CurrentPositionPointer + 3]}({A_ThirdParamMode})");
                        Op7();
                        break;
                    case 8:
                        //Console.WriteLine($"Op8 equals: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode}), {program[CurrentPositionPointer + 3]}({A_ThirdParamMode})");
                        Op8();
                        break;
                    case 99:
                        //Console.WriteLine("Program Halted Expectedly");
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

        int CurrentPositionPointer = 0;
        int DE_OpCode;
        int A_ThirdParamMode;
        int B_SecondParamMode;
        int C_FirstParamMode;

        private void parseOpCode(int opCode)
        {
            A_ThirdParamMode =  (opCode / 10000 % 1000 % 100 % 10); //A mode
            B_SecondParamMode = (opCode / 1000 % 100 % 10);         //B mode
            C_FirstParamMode =  (opCode / 100 % 10);                //C mode
            DE_OpCode =         (opCode % 100);                     //Opcode
        }

        //add
        //three parameters
        public void Op1()
        {
            var p1 = program[CurrentPositionPointer + 1];
            var p2 = program[CurrentPositionPointer + 2];
            var p3 = program[CurrentPositionPointer + 3];

            if (C_FirstParamMode == 0)
            {
                p1 = program[p1];
            }
            if (B_SecondParamMode == 0)
            {
                p2 = program[p2];
            }

            program[p3] = p1 + p2;
            CurrentPositionPointer += 4;
        }

        //multiply
        //three parameters
        public void Op2()
        {
            var p1 = program[CurrentPositionPointer + 1];
            var p2 = program[CurrentPositionPointer + 2];
            var p3 = program[CurrentPositionPointer + 3];

            if (C_FirstParamMode == 0)
            {
                p1 = program[p1];
            }
            if (B_SecondParamMode == 0)
            {
                p2 = program[p2];
            }

            program[p3] = p1 * p2;
            CurrentPositionPointer += 4;
        }

        //read input
        //one parameter
        public void Op3()
        {
            var p1 = program[CurrentPositionPointer + 1];
            program[p1] = input;
            CurrentPositionPointer += 2;
        }

        //output
        //one parameter
        public void Op4()
        {
            var p1 = program[CurrentPositionPointer + 1];

            if (C_FirstParamMode == 0)
            {
                p1 = program[p1];
            }

            outputs.Add(p1);
            CurrentPositionPointer += 2;
        }

        //jump if true 
        //two parameters
        public void Op5()
        {
            var p1 = program[CurrentPositionPointer + 1];
            var p2 = program[CurrentPositionPointer + 2];

            if (C_FirstParamMode == 0)
            {
                p1 = program[p1];
            }
            if (B_SecondParamMode == 0)
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

            if (C_FirstParamMode == 0)
            {
                p1 = program[p1];
            }
            if (B_SecondParamMode == 0)
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

            if (C_FirstParamMode == 0)
            {
                p1 = program[p1];
            }
            if (B_SecondParamMode == 0)
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

            if (C_FirstParamMode == 0)
            {
                p1 = program[p1];
            }
            if (B_SecondParamMode == 0)
            {
                p2 = program[p2];
            }

            program[p3] = (p1 == p2 ? 1 : 0);
            CurrentPositionPointer += 4;
        }
    }
}
