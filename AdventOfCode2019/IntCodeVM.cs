﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public class IntCodeVM
    {
        private long[] program = new long[1000];
        private int programLength = 0;
        private Queue<long> inputs = new Queue<long>();

        #region Public Access
        public Queue<long> outputs = new Queue<long>();
        public long[] programCode
        {
            get { return program; }
        }
        public Queue<long> programInputs
        {
            get { return inputs; }
        }
        #endregion

        IntCodeVMConfiguration config;

        public void Reset()
        {
            inputCursor = 0;
            outputs.Clear();
            CurPosCursor = 0;
            program = new long[1000000000];
        }

        public void WriteProgram(long[] program)
        {
            programLength = program.Length;
            program.CopyTo(this.program, 0);
        }

        public void WriteMemory(int pos, long value)
        {
            program[pos] = value;
        }

        public void WriteInput(long inp)
        {
            inputs.Enqueue(inp);
        }

        public HALTTYPE RunProgram()
        {
            //CurPosCursor = 0;
            //CurrentPositionPointer = 0;
            bool isrunning = true;
            while (isrunning)
            {
                parseOpCode();

                if (config.Logging)
                {
                    Delegate d;
                    if (opLogging.Any(x => x.Key == DE_OpCode))
                    {
                        d = opLogging.FirstOrDefault(d => d.Key == DE_OpCode)
                            .Value;
                    }
                    else
                    {
                        d = opLogging.FirstOrDefault(d => d.Key == -1)
                            .Value;
                    }
                    d.DynamicInvoke(DE_OpCode, program[CurPosCursor + 1], program[CurPosCursor + 2], program[CurPosCursor + 3], C_FirstParamMode, B_SecondParamMode, A_ThirdParamMode, config.VerboseLogging);
                }

                switch (DE_OpCode)
                {
                    case 1:
                        Op1();
                        break;
                    case 2:
                        Op2();
                        break;
                    case 3:
                        if (inputs.Count == 0)
                        {
                            return HALTTYPE.HALT_WAITING;
                        }
                        Op3();
                        break;
                    case 4:
                        Op4();
                        break;
                    case 5:
                        Op5();
                        break;
                    case 6:
                        Op6();
                        break;
                    case 7:
                        Op7();
                        break;
                    case 8:
                        Op8();
                        break;
                    case 9:
                        Op9();
                        break;
                    case 99:
                        //Console.WriteLine("Program Halted Expectedly");
                        isrunning = false;
                        return HALTTYPE.HALT_EXEPECTED;
                    default:
                        //Console.WriteLine("Program Halted Unexpectedly");
                        isrunning = false;
                        return HALTTYPE.HALT_UNEXPECTED;
                }
            }
            return HALTTYPE.HALT_EXEPECTED;
        }

        #region State Items
        //OpCode
        long DE_OpCode;

        // Parameter Modes
        // 0 = positional mode
        // 1 = immediate mode
        // 2 = reletive mode
        ParameterMode A_ThirdParamMode;
        ParameterMode B_SecondParamMode;
        ParameterMode C_FirstParamMode;

        int inputCursor = 0;
        long CurPosCursor = 0;
        long reletiveModeOffset = 0;

        bool pauseOnOutput = false;
        #endregion

        #region State Methods
        private void parseOpCode()
        {
            var opCode = program[CurPosCursor];
            A_ThirdParamMode = (ParameterMode)(opCode / 10000 % 1000 % 100 % 10); //A mode
            B_SecondParamMode = (ParameterMode)(opCode / 1000 % 100 % 10);         //B mode
            C_FirstParamMode = (ParameterMode)(opCode / 100 % 10);                //C mode
            DE_OpCode = (opCode % 100);                     //Opcode
        }

        private long GetParameterValue(long param, ParameterMode mode)
        {
            switch (mode)
            {
                case ParameterMode.Positional:
                    param = program[param];
                    break;
                case ParameterMode.Relative:
                    param = program[param + reletiveModeOffset];
                    break;
                case ParameterMode.Immediate:
                default:
                    break;
            }

            return param;
        }

        public long GetParameterPosition(long param, ParameterMode mode)
        {
            if (mode == ParameterMode.Relative)
            {
                param += reletiveModeOffset;
            }

            return param;
        }
        #endregion

        #region OpCodes
        //add
        //three parameters
        public void Op1()
        {
            var p1 = program[CurPosCursor + 1];
            var p2 = program[CurPosCursor + 2];
            var p3 = program[CurPosCursor + 3];

            p1 = GetParameterValue(p1, C_FirstParamMode);
            p2 = GetParameterValue(p2, B_SecondParamMode);

            p3 = GetParameterPosition(p3, A_ThirdParamMode);

            program[p3] = p1 + p2;

            if (config.FriendlyLogging || config.Logging)
            {
                Console.WriteLine($" - {p1} + {p2}({p1 + p2}) stored into program[{p3}]");
            }

            CurPosCursor += 4;
        }

        //multiply
        //three parameters
        public void Op2()
        {
            var p1 = program[CurPosCursor + 1];
            var p2 = program[CurPosCursor + 2];
            var p3 = program[CurPosCursor + 3];

            p1 = GetParameterValue(p1, C_FirstParamMode);
            p2 = GetParameterValue(p2, B_SecondParamMode);

            p3 = GetParameterPosition(p3, A_ThirdParamMode);

            program[p3] = p1 * p2;

            if (config.FriendlyLogging || config.Logging)
            {
                Console.WriteLine($" - {p1} * {p2}({p1 * p2}) stored into program[{p3}]");
            }

            CurPosCursor += 4;
        }

        //read input
        //one parameter
        public void Op3()
        {
            var p1 = program[CurPosCursor + 1];

            p1 = GetParameterPosition(p1, C_FirstParamMode);

            program[p1] = inputs.Dequeue();

            if (config.FriendlyLogging || config.Logging)
            {
                Console.WriteLine($" - Input {program[p1]} stored into program[{p1}]");
            }

            inputCursor++;
            CurPosCursor += 2;
        }

        //output
        //one parameter
        public void Op4()
        {
            var p1 = program[CurPosCursor + 1];

            p1 = GetParameterValue(p1, C_FirstParamMode);

            outputs.Enqueue(p1);

            if (config.FriendlyLogging || config.Logging)
            {
                Console.WriteLine($" - Output {p1} Paused on output {pauseOnOutput}");
            }

            CurPosCursor += 2;
        }

        //jump if true 
        //two parameters
        public void Op5()
        {
            var p1 = program[CurPosCursor + 1];
            var p2 = program[CurPosCursor + 2];

            p1 = GetParameterValue(p1, C_FirstParamMode);
            p2 = GetParameterValue(p2, B_SecondParamMode);

            if (p1 != 0)
            {
                CurPosCursor = p2;
            }
            else
            {
                CurPosCursor += 3;
            }

            if (config.FriendlyLogging || config.Logging)
            {
                Console.WriteLine($" - Jump to Program[{p2}] if program[{CurPosCursor + 1}]({p1} - {C_FirstParamMode}) is true, ");
            }

        }

        //jump if false
        //two parameters
        public void Op6()
        {
            var p1 = program[CurPosCursor + 1];
            var p2 = program[CurPosCursor + 2];

            p1 = GetParameterValue(p1, C_FirstParamMode);
            p2 = GetParameterValue(p2, B_SecondParamMode);

            if (p1 == 0)
            {
                CurPosCursor = p2;
            }
            else
            {
                CurPosCursor += 3;
            }

            if (config.FriendlyLogging || config.Logging)
            {
                Console.WriteLine($" - Jump to Program[{p2}] if program[{CurPosCursor + 1}]({p1} - {C_FirstParamMode}) is False, ");
            }
        }

        //is less than
        //three parameters
        public void Op7()
        {
            var p1 = program[CurPosCursor + 1];
            var p2 = program[CurPosCursor + 2];
            var p3 = program[CurPosCursor + 3];

            p1 = GetParameterValue(p1, C_FirstParamMode);
            p2 = GetParameterValue(p2, B_SecondParamMode);
            p3 = GetParameterPosition(p3, A_ThirdParamMode);

            program[p3] = (p1 < p2 ? 1 : 0);
            CurPosCursor += 4;

            if (config.FriendlyLogging || config.Logging)
            {
                Console.WriteLine($" - {p1} < {p2}({(p1 < p2 ? 1 : 0)}) stored into program[{p3}]");
            }
        }

        //equals
        //three parameters
        public void Op8()
        {
            var p1 = program[CurPosCursor + 1];
            var p2 = program[CurPosCursor + 2];
            var p3 = program[CurPosCursor + 3];

            p1 = GetParameterValue(p1, C_FirstParamMode);
            p2 = GetParameterValue(p2, B_SecondParamMode);
            p3 = GetParameterPosition(p3, A_ThirdParamMode);

            program[p3] = (p1 == p2 ? 1 : 0);
            CurPosCursor += 4;

            if (config.FriendlyLogging || config.Logging)
            {
                Console.WriteLine($" - {p1} == {p2}({(p1 == p2 ? 1 : 0)}) stored into program[{p3}]");
            }
        }

        //Adjust Releative Base
        //One Parameter
        public void Op9()
        {
            var p1 = program[CurPosCursor + 1];

            p1 = GetParameterValue(p1, C_FirstParamMode);

            reletiveModeOffset += p1;

            CurPosCursor += 2;

            if (config.FriendlyLogging || config.Logging)
            {
                Console.WriteLine($" - Reletive Mode Offset {reletiveModeOffset - p1} to { reletiveModeOffset }");
            }
        }
        #endregion

        #region Delegates
        private readonly List<KeyValuePair<int, Delegate>> opLogging = new List<KeyValuePair<int, Delegate>>() {
            new KeyValuePair<int, Delegate>(1, new Action<long, long, long, long,ParameterMode, ParameterMode, ParameterMode, bool>((DE, p1, p2, p3, p1m, p2m, p3m, isVerbose) => { var s = $"Op1 add: {DE}, {p1}({p1m}), {p2}({p2m}), {p3}({p3m})"; if (!isVerbose) Console.Write(s); else Console.WriteLine(s); }))
            ,new KeyValuePair<int, Delegate>(2, new Action<long, long, long, long,ParameterMode, ParameterMode, ParameterMode, bool>((DE, p1, p2, p3, p1m, p2m, p3m, isVerbose) => {var s = $"Op2 Multiply: {DE}, {p1}({p1m}), {p2}({p2m}), {p3}({p3m})"; if (!isVerbose) Console.Write(s); else Console.WriteLine(s); }))
            ,new KeyValuePair<int, Delegate>(3, new Action<long, long, long, long,ParameterMode, ParameterMode, ParameterMode, bool>((DE, p1, p2, p3, p1m, p2m, p3m, isVerbose) => {var s = $"Op3 input: {DE}, {p1}({p1m})"; if (!isVerbose) Console.Write(s); else Console.WriteLine(s); }))
            ,new KeyValuePair<int, Delegate>(4, new Action<long, long, long, long,ParameterMode, ParameterMode, ParameterMode, bool>((DE, p1, p2, p3, p1m, p2m, p3m, isVerbose) => {var s = $"Op4 output: {DE}, {p1}({p1m})"; if (!isVerbose) Console.Write(s); else Console.WriteLine(s); }))
            ,new KeyValuePair<int, Delegate>(5, new Action<long, long, long, long,ParameterMode, ParameterMode, ParameterMode, bool>((DE, p1, p2, p3, p1m, p2m, p3m, isVerbose) => {var s = $"Op5 jump if true: {DE}, {p1}({p1m}), {p2}({p2m})"; if (!isVerbose) Console.Write(s); else Console.WriteLine(s); }))
            ,new KeyValuePair<int, Delegate>(6, new Action<long, long, long, long,ParameterMode, ParameterMode, ParameterMode, bool>((DE, p1, p2, p3, p1m, p2m, p3m, isVerbose) => {var s = $"Op6 jump if false: {DE}, {p1}({p1m}), {p2}({p2m})"; if (!isVerbose) Console.Write(s); else Console.WriteLine(s); }))
            ,new KeyValuePair<int, Delegate>(7, new Action<long, long, long, long,ParameterMode, ParameterMode, ParameterMode, bool>((DE, p1, p2, p3, p1m, p2m, p3m, isVerbose) => {var s = $"Op7 less than: {DE}, {p1}({p1m}), {p2}({p2m}), {p3}({p3m})"; if (!isVerbose) Console.Write(s); else Console.WriteLine(s); }))
            ,new KeyValuePair<int, Delegate>(8, new Action<long, long, long, long,ParameterMode, ParameterMode, ParameterMode, bool>((DE, p1, p2, p3, p1m, p2m, p3m, isVerbose) => {var s = $"Op8 is equal: {DE}, {p1}({p1m}), {p2}({p2m}), {p3}({p3m})"; if (!isVerbose) Console.Write(s); else Console.WriteLine(s); }))
            ,new KeyValuePair<int, Delegate>(9, new Action<long, long, long, long,ParameterMode, ParameterMode, ParameterMode, bool>((DE, p1, p2, p3, p1m, p2m, p3m, isVerbose) => {var s = $"Op9 set offset: {DE}, {p1}({p1m})"; if (!isVerbose) Console.Write(s); else Console.WriteLine(s); }))
            ,new KeyValuePair<int, Delegate>(99, new Action<long, long, long, long,ParameterMode, ParameterMode, ParameterMode, bool>((DE, p1, p2, p3, p1m, p2m, p3m, isVerbose) => {var s = $"Execution Halted Successfully"; if (!isVerbose) Console.Write(s); else Console.WriteLine(s); }))
            ,new KeyValuePair<int, Delegate>(-1, new Action<long, long, long, long,ParameterMode, ParameterMode, ParameterMode, bool>((DE, p1, p2, p3, p1m, p2m, p3m, isVerbose) => {var s = $"Execution Halted Unexpectedly"; if (!isVerbose) Console.Write(s); else Console.WriteLine(s); }))
        };
        #endregion

        public IntCodeVM()
        {
            config = new IntCodeVMConfiguration();
        }

        public IntCodeVM(IntCodeVMConfiguration config)
        {
            this.config = config;
        }
    }

    public enum HALTTYPE
    {
        HALT_WAITING = 0,
        HALT_EXEPECTED = 1,
        HALT_UNEXPECTED = -1,
        HALT_OUTPUT = 2
    }
}
