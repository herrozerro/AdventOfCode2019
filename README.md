# AdventOfCode2019

This is just my repo for the 2019 Advent of Code.  

https://adventofcode.com/2019/

Let's see how this goes.

## Log

### Day 1

This one was pretty simple, I knew exactly where I was going once I saw the "Tyranny of the Rocket Equation".  

Part 2 just took a little but longer than needed because I had to remember recursion.

### Day 2

This one took a bit, I understood the concept of running bytecode, but it took a lot of trial and error.  Then I had an off by one that I needed to fix.

Part 2 got needlessly complex because of how I structured my data, I ran into the issue warned about by reusing memory.  So I had to quickly refactor and parse my input each time.

### Day 3

This one just clicked.  Initially I was going to make a huge grid and try to simulate it, but I ended up just storing a list of strings as I went.  IE: "0,0" "1,0" "2,0" etc... 

Luckily I didn't need to do any complex math on my points and just needed to intersect the two lists for matches, sum up the x/y to get the closest point.

Part 2 was simple as well.  All I had to do was get my matches again and get the index of each one in both lists and sum together their indexes to get the step count.

### Day 4

This one was just a miserable time.  Bugs, and a few logical errors plagued me.  It took entirely too long to figure out part 2's logic.  But it was still a fun exercise.

### Day 5

This on was fun!  I expanded the IntCodeVM by making it an actual VM class and cleaned it up.  I really hope this one gets resued during the course of the game. 

### Day 6

This one was fairly easy, a variation on Day 3.  Logic worked practically the same way by finding the ancestors and most common ancestor.
