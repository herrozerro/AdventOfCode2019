using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    public class IntCodeVM
    {
        public List<long> outputs = new List<long>();

        private long[] program = new long[0];

        public long[] programCode
        {
            get { return program; }
        }

        public long[] inputs;
        int inputCursor = 0;

        public int RunProgram(long[] programCode, long[] input, bool pauseOnOutput = false)
        {
            inputCursor = 0;
            this.inputs = input;

            program = new long[program.Length + 10000];
            programCode.CopyTo(program, 0);

            //CurrentPositionPointer = 0;
            bool isrunning = true;
            while (isrunning)
            {
                var x = program[CurrentPositionPointer];

                parseOpCode(x);

                switch (DE_OpCode)
                {
                    case 1:
                        Console.WriteLine($"Op1 add: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode}), {program[CurrentPositionPointer + 3]}({A_ThirdParamMode})");
                        Op1();
                        break;
                    case 2:
                        Console.WriteLine($"Op2 multiply: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode}), {program[CurrentPositionPointer + 3]}({A_ThirdParamMode})");
                        Op2();
                        break;
                    case 3:
                        Console.WriteLine($"Op3 input: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode})");
                        Op3();
                        break;
                    case 4:
                        Console.WriteLine($"Op4 output: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode})");
                        Op4();
                        if (pauseOnOutput)
                        {
                            isrunning = false;
                            return 0;
                        }
                        break;
                    case 5:
                        Console.WriteLine($"Op5 jump-if-true: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode})");
                        Op5();
                        break;
                    case 6:
                        Console.WriteLine($"Op6 jump-if-false: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode})");
                        Op6();
                        break;
                    case 7:
                        Console.WriteLine($"Op7 less than: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode}), {program[CurrentPositionPointer + 3]}({A_ThirdParamMode})");
                        Op7();
                        break;
                    case 8:
                        Console.WriteLine($"Op8 equals: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode}), {program[CurrentPositionPointer + 2]}({B_SecondParamMode}), {program[CurrentPositionPointer + 3]}({A_ThirdParamMode})");
                        Op8();
                        break;
                    case 9:
                        Console.WriteLine($"Op9 Set Offset: {DE_OpCode}, {program[CurrentPositionPointer + 1]}({C_FirstParamMode})");
                        op9();
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

            return 1;
        }

        long CurrentPositionPointer = 0;
        long DE_OpCode;
        
        // 0 = positional mode
        // 1 = 
        // 2 = reletive mode
        long A_ThirdParamMode;
        long B_SecondParamMode;
        long C_FirstParamMode;

        long reletiveModeOffset = 0;

        long[] extMemeory = new long[10000];

        private void parseOpCode(long opCode)
        {
            A_ThirdParamMode = (opCode / 10000 % 1000 % 100 % 10); //A mode
            B_SecondParamMode = (opCode / 1000 % 100 % 10);         //B mode
            C_FirstParamMode = (opCode / 100 % 10);                //C mode
            DE_OpCode = (opCode % 100);                     //Opcode
        }

        //add
        //three parameters
        public void Op1()
        {
            var p1 = program[CurrentPositionPointer + 1];
            var p2 = program[CurrentPositionPointer + 2];
            var p3 = program[CurrentPositionPointer + 3];

            p1 = GetParameterValue(p1, C_FirstParamMode);
            p2 = GetParameterValue(p2, B_SecondParamMode);
            
            p3 = GetParameterPosition(p3, A_ThirdParamMode);

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

            p1 = GetParameterValue(p1, C_FirstParamMode);
            p2 = GetParameterValue(p2, B_SecondParamMode);
            
            p3 = GetParameterPosition(p3, A_ThirdParamMode);

            program[p3] = p1 * p2;
            CurrentPositionPointer += 4;
        }

        //read input
        //one parameter
        public void Op3()
        {
            var p1 = program[CurrentPositionPointer + 1];

            p1 = GetParameterPosition(p1, C_FirstParamMode);

            program[p1] = inputs[inputCursor];

            inputCursor++;

            CurrentPositionPointer += 2;
        }

        //output
        //one parameter
        public void Op4()
        {
            var p1 = program[CurrentPositionPointer + 1];

            p1 = GetParameterValue(p1, C_FirstParamMode);

            outputs.Add(p1);
            CurrentPositionPointer += 2;
        }

        //jump if true 
        //two parameters
        public void Op5()
        {
            var p1 = program[CurrentPositionPointer + 1];
            var p2 = program[CurrentPositionPointer + 2];

            p1 = GetParameterValue(p1, C_FirstParamMode);
            p2 = GetParameterValue(p2, B_SecondParamMode);

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

            p1 = GetParameterValue(p1, C_FirstParamMode);
            p2 = GetParameterValue(p2, B_SecondParamMode);

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
            
            p1 = GetParameterValue(p1, C_FirstParamMode);
            p2 = GetParameterValue(p2, B_SecondParamMode);
            p3 = GetParameterPosition(p3, A_ThirdParamMode);

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

            p1 = GetParameterValue(p1, C_FirstParamMode);
            p2 = GetParameterValue(p2, B_SecondParamMode);
            p3 = GetParameterPosition(p3, A_ThirdParamMode);

            program[p3] = (p1 == p2 ? 1 : 0);
            CurrentPositionPointer += 4;
        }

        //Adjust Releative Base
        //One Parameter
        public void op9()
        {
            var p1 = program[CurrentPositionPointer + 1];

            p1 = GetParameterValue(p1, C_FirstParamMode);

            reletiveModeOffset += p1;

            CurrentPositionPointer += 2;
        }

        public long GetParameterValue(long param, long mode)
        {
            switch (mode)
            {
                case 0:
                    param = program[param];
                    break;
                case 2:
                    param = program[param + reletiveModeOffset];
                    break;
                case 1:
                default:
                    break; 
            }

            return param;
        }

        public long GetParameterPosition(long param, long mode)
        {
            if (mode  == 2)
            {
                param += reletiveModeOffset;
            }

            return param;
        }
    }
}
