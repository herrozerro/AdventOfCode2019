using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventOfCode2019
{
    public static class Day15
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 15");

            P1();
            P2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        static char[,] grid = new char[50, 50];
        static int[] curPos = new int[] { 25, 25 };
        static Facing currentFacing = Facing.north;
        static bool stepSuccess;
        static StepType stepType = StepType.forward;
        static bool complete = false;



        static void P1()
        {
            var lines = Utilities.GetStringFromFile("Day15.txt").SplitLongArrayFromString(',');

            var vm = new IntCodeVM(new IntCodeVMConfiguration() { });
            vm.WriteProgram(lines);
            grid[25, 25] = 'S';

            Draw(grid);



            vm.WriteInput((long)Facing.north);

            //vm.WriteMemory(0, 1);
            while (vm.RunProgram() == HALTTYPE.HALT_WAITING)
            {

                processOutputs(vm);

                if (complete)
                {
                    break;
                }

                //what type of step was taken?
                switch (stepType)
                {
                    case StepType.forward:
                        //was step forward successful?  if not turn left
                        if (!stepSuccess)
                        {
                            TurnDirection(Direction.left);
                        }
                        //attempt to turn right now.
                        stepType = StepType.right;
                        vm.WriteInput((long)GetTurnDirection(Direction.right));
                        break;
                    case StepType.right:
                        //if step was successful change facing and try to turn right again
                        if (stepSuccess)
                        {
                            TurnDirection(Direction.right);
                            stepType = StepType.right;
                            vm.WriteInput((long)GetTurnDirection(Direction.right));
                        }
                        //step unsuccessful, continue forward.
                        else
                        {
                            vm.WriteInput((long)currentFacing);
                            stepType = StepType.forward;
                        }
                        break;
                    default:
                        break;
                }

                //Draw(grid);

            }

            Draw(grid);

            int[] target = new int[] { 0, 0 };

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 'O')
                    {
                        target = new int[] { i, j };
                    }
                }
            }

            var s = new seeker(target, 0, grid, new int[] { 25, 25 }, Facing.north);

            var result = s.Seek();

            Console.WriteLine($"Min Steps to get to Oxygen Control: {result.Value}");
        }

        static void P2()
        {
            grid = new char[50, 50];
            curPos = new int[] { 25, 25 };
            currentFacing = Facing.north;
            stepType = StepType.forward;
            complete = false;

            var lines = Utilities.GetStringFromFile("Day15.txt").SplitLongArrayFromString(',');

            var vm = new IntCodeVM(new IntCodeVMConfiguration() { });
            vm.WriteProgram(lines);
            grid[25, 25] = 'S';

            Draw(grid);



            vm.WriteInput((long)Facing.north);

            //vm.WriteMemory(0, 1);
            while (vm.RunProgram() == HALTTYPE.HALT_WAITING)
            {

                processOutputs(vm);

                if (complete)
                {
                    break;
                }

                //what type of step was taken?
                switch (stepType)
                {
                    case StepType.forward:
                        //was step forward successful?  if not turn left
                        if (!stepSuccess)
                        {
                            TurnDirection(Direction.left);
                        }
                        //attempt to turn right now.
                        stepType = StepType.right;
                        vm.WriteInput((long)GetTurnDirection(Direction.right));
                        break;
                    case StepType.right:
                        //if step was successful change facing and try to turn right again
                        if (stepSuccess)
                        {
                            TurnDirection(Direction.right);
                            stepType = StepType.right;
                            vm.WriteInput((long)GetTurnDirection(Direction.right));
                        }
                        //step unsuccessful, continue forward.
                        else
                        {
                            vm.WriteInput((long)currentFacing);
                            stepType = StepType.forward;
                        }
                        break;
                    default:
                        break;
                }

                //Draw(grid);

            }

            Draw(grid);

            int[] target = new int[] { 0, 0 };

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 'O')
                    {
                        target = new int[] { i, j };
                    }
                }
            }

            var s = new seeker(new int[] { 0,0 }, 0, grid, target, Facing.south);

            var result = s.Seek();

            Console.WriteLine($"Max time to fill compartment: {seeker.MaxSteps}");
        }


        static Facing GetTurnDirection(Direction dir)
        {
            Facing f = currentFacing;
            switch (currentFacing)
            {
                case Facing.north:
                    f = (dir == Direction.left ? Facing.west : Facing.east);
                    break;
                case Facing.south:
                    f = (dir == Direction.left ? Facing.east : Facing.west);
                    break;
                case Facing.west:
                    f = (dir == Direction.left ? Facing.south : Facing.north);
                    break;
                case Facing.east:
                    f = (dir == Direction.left ? Facing.north : Facing.south);
                    break;
            }
            return f;
        }

        static void TurnDirection(Direction dir)
        {
            switch (currentFacing)
            {
                case Facing.north:
                    currentFacing = (dir == Direction.left ? Facing.west : Facing.east);
                    break;
                case Facing.south:
                    currentFacing = (dir == Direction.left ? Facing.east : Facing.west);
                    break;
                case Facing.west:
                    currentFacing = (dir == Direction.left ? Facing.south : Facing.north);
                    break;
                case Facing.east:
                    currentFacing = (dir == Direction.left ? Facing.north : Facing.south);
                    break;
            }
        }

        static void processOutputs(IntCodeVM vm)
        {
            while (vm.outputs.Count > 0)
            {
                var tile = vm.outputs.Dequeue();
                var nextPos = new int[2];
                curPos.CopyTo(nextPos, 0);
                //assume step success
                stepSuccess = true;

                char writeTile = '.';
                switch (tile)
                {
                    case 0:
                        writeTile = '█';
                        stepSuccess = false; //step unsuccessful
                        break;
                    case 1:
                        writeTile = '.';
                        break;
                    case 2:
                        writeTile = 'O';
                        break;
                    default:
                        break;
                }

                Facing f = currentFacing;

                if (stepType == StepType.right)
                {
                    f = GetTurnDirection(Direction.right);
                }

                switch (f)
                {
                    case Facing.north:
                        nextPos[1]--;
                        break;
                    case Facing.south:
                        nextPos[1]++;
                        break;
                    case Facing.west:
                        nextPos[0]--;
                        break;
                    case Facing.east:
                        nextPos[0]++;
                        break;
                    default:
                        break;
                }

                if (nextPos[0] == 25 && nextPos[1] == 25)
                {
                    complete = true;
                    return;
                }

                grid[nextPos[0], nextPos[1]] = writeTile;

                if (writeTile != '█')
                {
                    curPos = nextPos;
                }



            }
        }

        static void Draw(char[,] grid)
        {
            Console.Clear();
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                var row = "";
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    row = row + (grid[j, i] == '\0' ? 'X' : grid[j, i]);
                }
                Console.WriteLine(row);
            }
            Thread.Sleep(100);
            //Console.WriteLine($"Score: {score} ---- Blockes Remaining: {blockcount}");
        }
    }

    

    public enum Facing
    {
        north = 1,
        south = 2,
        west = 3,
        east = 4
    }
    public enum StepType
    {
        forward = 1,
        right = 2
    }
    public enum Direction
    {
        left,
        right
    }
    
    public class seeker
    {
        public static int MaxSteps = 0;
        int steps { get; set; } = 0;
        int[] target { get; set; }
        Facing currentFacing = Facing.north;
        int[] CurrentPos { get; set; }
        char[,] grid;

        List<seeker> childSeekers = new List<seeker>();

        public KeyValuePair<bool, int> Seek()
        {

            while (true)
            {
                grid[CurrentPos[0], CurrentPos[1]] = 'Z';
                var fls = GetForwardOpenFacings();

                if (CurrentPos[0] == target[0] && CurrentPos[1] == target[1])
                {
                    break;
                }

                switch (fls.Count)
                {
                    case 1:
                        Move(fls.First());
                        currentFacing = fls.First();
                        break;
                    case 0:
                        return new KeyValuePair<bool, int>(false, steps);
                    default:
                        foreach (var f in fls)
                        {
                            childSeekers.Add(new seeker(target, steps+1, grid, GetDirPos(f), f));
                        }
                        foreach (var s in childSeekers)
                        {
                            var result = s.Seek();
                            if (result.Key)
                            {
                                return result;
                            }
                            else
                            {
                                if (result.Value > seeker.MaxSteps)
                                {
                                    seeker.MaxSteps = result.Value;
                                }
                            }
                        }
                        break;
                }
            }

            return new KeyValuePair<bool, int>(true, steps);
        }


        int[] GetDirPos(Facing f)
        {
            int[] newPos = new int[2];
            CurrentPos.CopyTo(newPos, 0);
            switch (f)
            {
                case Facing.north:
                    newPos[1]--;
                    break;
                case Facing.south:
                    newPos[1]++;
                    break;
                case Facing.west:
                    newPos[0]--;
                    break;
                case Facing.east:
                    newPos[0]++;
                    break;
                default:
                    break;
            }
            return newPos;
        }

        void Move(Facing f)
        {
            
            switch (f)
            {
                case Facing.north:
                    CurrentPos[1]--;
                    break;
                case Facing.south:
                    CurrentPos[1]++;
                    break;
                case Facing.west:
                    CurrentPos[0]--;
                    break;
                case Facing.east:
                    CurrentPos[0]++;
                    break;
                default:
                    break;
            }
            
            steps++;
            Draw(grid);
        }

        bool IsFacingOpen(Facing f)
        {
            var nextPos = new int[2];
            CurrentPos.CopyTo(nextPos, 0);

            switch (f)
            {
                case Facing.north:
                    nextPos[1]--;
                    break;
                case Facing.south:
                    nextPos[1]++;
                    break;
                case Facing.west:
                    nextPos[0]--;
                    break;
                case Facing.east:
                    nextPos[0]++;
                    break;
                default:
                    break;
            }

            var charAtPosition = grid[nextPos[0], nextPos[1]];

            return (charAtPosition == '.' || charAtPosition == 'O');
        }

        List<Facing> GetForwardOpenFacings()
        {
            List<Facing> fls = new List<Facing>();
            fls.Add(Facing.east);
            fls.Add(Facing.south);
            fls.Add(Facing.west);
            fls.Add(Facing.north);

            switch (currentFacing)
            {
                case Facing.north:
                    fls.Remove(Facing.south);
                    break;
                case Facing.south:
                    fls.Remove(Facing.north);
                    break;
                case Facing.west:
                    fls.Remove(Facing.east);
                    break;
                case Facing.east:
                    fls.Remove(Facing.west);
                    break;
                default:
                    break;
            }

            var openFacing = new List<Facing>();

            foreach (var f in fls)
            {
                if (IsFacingOpen(f))
                {
                    openFacing.Add(f);
                }
            }

            return openFacing;
        }

        Facing GetTurnDirection(Direction dir)
        {
            Facing f = currentFacing;
            switch (currentFacing)
            {
                case Facing.north:
                    f = (dir == Direction.left ? Facing.west : Facing.east);
                    break;
                case Facing.south:
                    f = (dir == Direction.left ? Facing.east : Facing.west);
                    break;
                case Facing.west:
                    f = (dir == Direction.left ? Facing.south : Facing.north);
                    break;
                case Facing.east:
                    f = (dir == Direction.left ? Facing.north : Facing.south);
                    break;
            }
            return f;
        }
        void TurnDirection(Direction dir)
        {
            switch (currentFacing)
            {
                case Facing.north:
                    currentFacing = (dir == Direction.left ? Facing.west : Facing.east);
                    break;
                case Facing.south:
                    currentFacing = (dir == Direction.left ? Facing.east : Facing.west);
                    break;
                case Facing.west:
                    currentFacing = (dir == Direction.left ? Facing.south : Facing.north);
                    break;
                case Facing.east:
                    currentFacing = (dir == Direction.left ? Facing.north : Facing.south);
                    break;
            }
        }

        void Draw(char[,] grid)
        {
            Console.Clear();
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                var row = "";
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    row = row + (grid[j, i] == '\0' ? 'X' : grid[j, i]);
                }
                Console.WriteLine(row);
            }
            Thread.Sleep(10);
            //Console.WriteLine($"Score: {score} ---- Blockes Remaining: {blockcount}");
        }

        public seeker(int[] target, int startingSteps, char[,] grid, int[] pos, Facing initialFacing)
        {
            steps = startingSteps;
            this.target = target;
            this.grid = grid;
            CurrentPos = pos;
            currentFacing = initialFacing;
        }
    }
}
