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
                //compare all moons for velocities
                foreach (var c in comparisonList)
                {
                    var m1 = moons[c.Key, 0];
                    var v1 = moons[c.Key, 1];
                    var m2 = moons[c.Value, 0];
                    var v2 = moons[c.Value, 1];

                    //x comparison
                    if (m1.X != m2.X)
                    {
                        v1.X += m1.X < m2.X ? 1 : -1;
                        v2.X += m2.X < m1.X ? 1 : -1;
                    }

                    //y comparison
                    if (m1.Y != m2.Y)
                    {
                        v1.Y += m1.Y < m2.Y ? 1 : -1;
                        v2.Y += m2.Y < m1.Y ? 1 : -1;
                    }

                    //z comparison
                    if (m1.Z != m2.Z)
                    {
                        v1.Z += m1.Z < m2.Z ? 1 : -1;
                        v2.Z += m2.Z < m1.Z ? 1 : -1;
                    }

                    moons[c.Key, 1] = v1;
                    moons[c.Value, 1] = v2;
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
            //var moons2 = new Vector3[4, 2] {
            //    { new Vector3(-1, 0, 2), new Vector3(0,0,0) },
            //    { new Vector3(2, -10, -7), new Vector3(0,0,0) },
            //    { new Vector3(4,-8,8), new Vector3(0,0,0) },
            //    { new Vector3(3,5,-1), new Vector3(0,0,0) }
            //};

            //var moonsinit = new Vector3[4, 2] {
            //    { new Vector3(-1, 0, 2), new Vector3(0,0,0) },
            //    { new Vector3(2, -10, -7), new Vector3(0,0,0) },
            //    { new Vector3(4,-8,8), new Vector3(0,0,0) },
            //    { new Vector3(3,5,-1), new Vector3(0,0,0) }
            //};

            Int64 iter = 0;
            Int64 m1o = 0;
            Int64 m2o = 0;
            Int64 m3o = 0;
            Int64 m4o = 0;

            Int64 xi = 0;
            Int64 yi = 0;
            Int64 zi = 0;


            while (true)
            {
                //compare all moons2 for velocities
                foreach (var c in comparisonList)
                {
                    var m1 = moons2[c.Key, 0];
                    var v1 = moons2[c.Key, 1];
                    var m2 = moons2[c.Value, 0];
                    var v2 = moons2[c.Value, 1];

                    //x comparison
                    if (m1.X != m2.X)
                    {
                        v1.X += m1.X < m2.X ? 1 : -1;
                        v2.X += m2.X < m1.X ? 1 : -1;
                    }

                    //y comparison
                    if (m1.Y != m2.Y)
                    {
                        v1.Y += m1.Y < m2.Y ? 1 : -1;
                        v2.Y += m2.Y < m1.Y ? 1 : -1;
                    }

                    //z comparison
                    if (m1.Z != m2.Z)
                    {
                        v1.Z += m1.Z < m2.Z ? 1 : -1;
                        v2.Z += m2.Z < m1.Z ? 1 : -1;
                    }

                    moons2[c.Key, 1] = v1;
                    moons2[c.Value, 1] = v2;
                }

                //Apply velocity
                for (int j = 0; j < 4; j++)
                {
                    moons2[j, 0] += moons2[j, 1];
                }
                iter++;



                //if (moons2[0, 1] == new Vector3(0, 0, 0) && m1o == 0)
                //{
                //    m1o = iter;
                //    Console.WriteLine(iter);
                //}
                //if (moons2[1, 1] == new Vector3(0, 0, 0) && m2o == 0)
                //{
                //    m2o = iter;
                //    Console.WriteLine(iter);
                //}
                //if (moons2[2, 1] == new Vector3(0, 0, 0) && m3o == 0)
                //{
                //    m3o = iter;
                //    Console.WriteLine(iter);
                //}
                //if (moons2[3, 1] == new Vector3(0, 0, 0) && m4o == 0)
                //{
                //    m4o = iter;
                //    Console.WriteLine(iter);
                //}

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

                //if (m1o > 0 && m2o > 0 && m3o > 0 && m4o > 0)
                //{
                //    Console.WriteLine(iter);
                //}

                if (moons2[0,1] == new Vector3(0,0,0) && moons2[1, 1] == new Vector3(0, 0, 0)
                    && moons2[2, 1] == new Vector3(0, 0, 0) && moons2[3, 1] == new Vector3(0, 0, 0))
                {
                    Console.WriteLine(iter);
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
