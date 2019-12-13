using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day12
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 12");

            var comparisonList = new List<KeyValuePair<int, int>>()
            {
                new KeyValuePair<int, int>(0,1),
                new KeyValuePair<int, int>(0,2),
                new KeyValuePair<int, int>(0,3),
                new KeyValuePair<int, int>(1,2),
                new KeyValuePair<int, int>(1,3),
                new KeyValuePair<int, int>(2,3),
            };

            var moons = new Vector3[4, 2] {
                { new Vector3(10, 15, 7), new Vector3(0,0,0) },
                { new Vector3(15, 10, 0), new Vector3(0,0,0) },
                { new Vector3(20,12,3), new Vector3(0,0,0) },
                { new Vector3(0,-3,13), new Vector3(0,0,0) }
            };

            for (int i = 0; i < 1000; i++)
            {
                //Apply Gravity
                foreach (var c in comparisonList)
                {
                    var m1 = moons[c.Key, 0];
                    var m2 = moons[c.Value, 0];

                    var v1 = new Vector3(Math.Sign(m2.X - m1.X), Math.Sign(m2.Y - m1.Y), Math.Sign(m2.Z - m1.Z));

                    moons[c.Key, 1] += v1;
                    moons[c.Value, 1] += v1 * -1;
                }

                //Apply velocity
                for (int j = 0; j < 4; j++)
                {
                    moons[j, 0] += moons[j, 1];
                }
            }

            var tot = 0f;
            for (int j = 0; j < 4; j++)
            {
                var mpot = Math.Abs(moons[j, 0].X) + Math.Abs(moons[j, 0].Y) + Math.Abs(moons[j, 0].Z);
                var mkin = Math.Abs(moons[j, 1].X) + Math.Abs(moons[j, 1].Y) + Math.Abs(moons[j, 1].Z);

                tot += mpot * mkin;
            }
            Console.WriteLine($"Total Energy: {tot}");

            var moons2 = new Vector3[4, 2] {
                { new Vector3(10, 15, 7), new Vector3(0,0,0) },
                { new Vector3(15, 10, 0), new Vector3(0,0,0) },
                { new Vector3(20,12,3), new Vector3(0,0,0) },
                { new Vector3(0,-3,13), new Vector3(0,0,0) }
            };

            var moonsinit = new Vector3[4, 2] {
                { new Vector3(10, 15, 7), new Vector3(0,0,0) },
                { new Vector3(15, 10, 0), new Vector3(0,0,0) },
                { new Vector3(20,12,3), new Vector3(0,0,0) },
                { new Vector3(0,-3,13), new Vector3(0,0,0) }
            };


            long iter = 0;
            long xi = 0;
            long yi = 0;
            long zi = 0;


            while (true)
            {
                //compare all moons2 for velocities
                foreach (var c in comparisonList)
                {
                    var m1 = moons2[c.Key, 0];
                    var m2 = moons2[c.Value, 0];

                    var v1 = new Vector3(Math.Sign(m2.X - m1.X), Math.Sign(m2.Y - m1.Y), Math.Sign(m2.Z - m1.Z));

                    moons2[c.Key, 1] += v1;
                    moons2[c.Value, 1] += v1 *-1;
                }

                //Apply velocity
                for (int j = 0; j < 4; j++)
                {
                    moons2[j, 0] += moons2[j, 1];
                }
                iter++;

                //get axis periods
                if (moons2[0, 1].X == moonsinit[0, 1].X && moons2[1, 1].X == moonsinit[1, 1].X && moons2[2, 1].X == moonsinit[2, 1].X && moons2[3, 1].X == moonsinit[3, 1].X &&
                    moons2[0, 1].X == 0 && moons2[1, 1].X == 0 && moons2[2, 1].X == 0 && moons2[3, 1].X == 0)
                {
                    xi = iter;
                }

                if (moons2[0, 1].Y == moonsinit[0, 1].Y && moons2[1, 1].Y == moonsinit[1, 1].Y && moons2[2, 1].Y == moonsinit[2, 1].Y && moons2[3, 1].Y == moonsinit[3, 1].Y &&
                    moons2[0, 1].Y == 0 && moons2[1, 1].Y == 0 && moons2[2, 1].Y == 0 && moons2[3, 1].Y == 0)
                {
                    yi = iter;
                }

                if (moons2[0, 1].Z == moonsinit[0, 1].Z && moons2[1, 1].Z == moonsinit[1, 1].Z && moons2[2, 1].Z == moonsinit[2, 1].Z && moons2[3, 1].Z == moonsinit[3, 1].Z &&
                    moons2[0, 1].Z == 0 && moons2[1, 1].Z == 0 && moons2[2, 1].Z == 0 && moons2[3, 1].Z == 0)
                {
                    zi = iter;
                }

                if (xi > 0 && yi > 0 && zi > 0)
                {
                    Console.WriteLine($"x {xi}, y {yi}, z{zi}");
                    Console.WriteLine(GetGCF(xi, GetGCF(yi,zi)));
                    break;
                }
            }


            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        static long GetGCF(long a, long b)
        {
            return (a * b) / GetGCD(a, b);
        }

        static long GetGCD(long a, long b)
        {
            while (a != b)
                if (a < b) b = b - a;
                else a = a - b;
            return (a);
        }
    }
}
