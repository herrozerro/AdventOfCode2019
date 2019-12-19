<Query Kind="Statements" />

var str = @"#.......................................
.#......................................
..##....................................
...###..................................
....###.................................
.....####...............................
......#####.............................
......######............................
.......#######..........................
........########........................
.........#########......................
..........#########.....................
...........##########...................
...........############.................
............############................
.............#############..............
..............##############............
...............###############..........
................###############.........
................#################.......
.................##################.....
..................##################....
...................###################..
....................####################
.....................###################
.....................###################
......................##################
.......................#################
........................################
.........................###############
..........................##############
..........................##############
...........................#############
............................############
.............................###########";
//var lines = str.Split(Environment.NewLine);

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));

var lines = File.ReadAllLines("..\\Data\\Day19out.txt");

var grid = new char[lines.Length, lines[0].Length];

for (int row = 0; row < grid.GetLength(0); row++)
{
	for (int column = 0; column < grid.GetLength(1); column++)
	{
		grid[row, column] = lines[row][column];
	}
}

//grid.Dump();

var boxsize = 100;

bool colMin = false;
bool rowMin = false;


for (int i = 0; i < grid.GetLength(0); i++)
{
	//columns
	var colCount = Enumerable.Range(0, grid.GetLength(0)).Select(e => grid[e, i]).Count(e => e == '#');
	if (colCount >= boxsize)
	{
		for (int r = 0; r < grid.GetLength(0); r++)
		{
			if (grid[r, i] != '.')
			{
				grid[r, i] = (grid[r, i] == '#' ? '1' : '2');
			}

		}
	}
}

//grid.Dump();

for (int i = 0; i < grid.GetLength(0); i++)
{
	//rows
	var rowCount = Enumerable.Range(0, grid.GetLength(1)).Select(e => grid[i, e]).Count(e => e != '.');
	if (rowCount >= boxsize)
	{
		for (int c = 0; c < grid.GetLength(1); c++)
		{
			if (grid[i, c] != '.')
			{
				grid[i, c] = (grid[i, c] == '#' ? '1' : '2');
			}

		}
	}
}

//grid.Dump();

for (int i = 0; i < grid.GetLength(1); i++)
{
	var row = Enumerable.Range(0, grid.GetLength(1)).Select(e => grid[i, e]).ToArray();

	int lowerindex = Array.IndexOf(row, '2');

	if (lowerindex > 0)
	{
		//is diagonal filled?
		char diagonal = grid[i - (boxsize - 1) , lowerindex + (boxsize - 1)];

		if (diagonal != '.')
		{
			grid[i , lowerindex] = 'x';
			grid[i - (boxsize - 1) , lowerindex + (boxsize - 1)] = 'x';
			grid[i - (boxsize - 1), lowerindex] = 'O';
			$"y{lowerindex}, x{i - (boxsize - 1)}: answer: {((i - (boxsize - 1))*10000)+lowerindex}".Dump();
			break;
		}
		grid[i , lowerindex] = 'x';
		//grid[i - (boxsize - 1) , lowerindex + (boxsize - 1)] = 'O';
		

	}
}

//grid.Dump();

//Enumerable.Range(0,grid.GetLength(0)).Select(e => grid[e,6]).Count(e => e == '#').Dump();
//Enumerable.Range(0,grid.GetLength(1)).Select(e => grid[6,e]).Count(e => e == '#').Dump();