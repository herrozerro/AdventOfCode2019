using AdventOfCode2019;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019Tests
{
    public class Day03Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Part1()
        {
            var c1path = "R75,D30,R83,U83,L12,D49,R71,U7,L72".Split(',').ToList();
            var c2path = "U62,R66,U55,R34,D71,R55,D58,R83".Split(',').ToList();

            var c1 = new Circuit();
            c1.runPath(c1path);
            var c2 = new Circuit();
            c2.runPath(c2path);

            var matches = c1.path.Intersect(c2.path).Where(x=>x!="0,0").ToList().Select(x => x.Split(',').Select<string, int>(x=> Math.Abs(int.Parse(x)))).ToList();

            var sums = matches.Select(x=>x.Sum()).ToList();

            Assert.AreEqual(sums.Min(), 159);

            c1path = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51".Split(',').ToList();
            c2path = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7".Split(',').ToList();

            c1 = new Circuit();
            c1.runPath(c1path);
            c2 = new Circuit();
            c2.runPath(c2path);

            matches = c1.path.Intersect(c2.path).Where(x => x != "0,0").ToList().Select(x => x.Split(',').Select<string, int>(x => Math.Abs(int.Parse(x)))).ToList();

            sums = matches.Select(x => x.Sum()).ToList();

            Assert.AreEqual(sums.Min(), 135);
        }

        [Test]
        public void Part2()
        {
            var lines = new string[] { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" };

            var circuit1 = new Circuit();
            circuit1.runPath(lines[0].Split(',').ToList());

            var circuit2 = new Circuit();
            circuit2.runPath(lines[1].Split(',').ToList());

            var matches = circuit1.path.Intersect(circuit2.path).ToList();


            var matchlist = new List<KeyValuePair<string, int>>();
            foreach (var match in matches.Where(x=>x != "0,0"))
            {
                var cind1 = circuit1.path.FindIndex(x => x == match);
                var cind2 = circuit2.path.FindIndex(x => x == match);
                matchlist.Add(new KeyValuePair<string, int>(match, cind1 + cind2));
            }
            var ms1 = matchlist.OrderBy(x => x.Value).ToList();

            Assert.AreEqual(ms1.First().Value, 610);

            lines = new string[] { "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7" };

            circuit1 = new Circuit();
            circuit1.runPath(lines[0].Split(',').ToList());

            circuit2 = new Circuit();
            circuit2.runPath(lines[1].Split(',').ToList());

            matches = circuit1.path.Intersect(circuit2.path).ToList();


            matchlist = new List<KeyValuePair<string, int>>();
            foreach (var match in matches.Where(x => x != "0,0"))
            {
                var cind1 = circuit1.path.FindIndex(x => x == match);
                var cind2 = circuit2.path.FindIndex(x => x == match);
                matchlist.Add(new KeyValuePair<string, int>(match, cind1 + cind2));
            }
            ms1 = matchlist.OrderBy(x => x.Value).ToList();

            Assert.AreEqual(ms1.First().Value, 410);
        }
    }
}