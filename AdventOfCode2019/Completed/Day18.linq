<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var map = GetMap();

	var targets = GetTargets(map).ToList().Dump();
	
	

	var results = new List<AllNodes>();
	var dLoc = new int[2] { 35, 37 };
	//var fLoc = new int[2] { 3, 63 };
	var Center = new int[2] { 40, 40 };

	for (int i = 0; i < targets.Count(); i++)
	{
		for (int j = i + 1; j < targets.Count(); j++)
		{
			var s = targets[i];
			int[] sLoc = new int[2];
			s.Value.CopyTo(sLoc, 0);
			var t = targets[j];
			int[] tLoc = new int[2];
			t.Value.CopyTo(tLoc, 0);
			
			var map2 = GetMap();
			
			
			
			//map2.Dump();
			
			var hunter = new seeker(tLoc, 0, map2, sLoc, Facing.north, GetDoors());

			var result = hunter.Seek();

			results.Add(new AllNodes()
			{
				nodea = targets[i].Key,
				nodeb = targets[j].Key,
				DoorsBlocking = result.DoorsInWay.ToArray(),
				steps = result.Steps
				//stepPlaces = result.StepPath 
			});
		}
	}


	results.Dump();
	
	results.Where(r => r.nodea == 'm' && r.DoorsBlocking.Length == 0).Dump();
}

class AllNodes
{
	public char nodea { get; set; }
	public char nodeb { get; set; }
	public int steps { get; set; }
	public char[] DoorsBlocking { get; set; }
	public List<KeyValuePair<int, int>> stepPlaces {get;set;}
	
	public bool IsValid(List<char> keys)
	{
		var haskeys = DoorsBlocking.All(db => keys.Contains(db.ToString().ToLower()[0]));
		return haskeys;
	}
}

public char[] GetDoors()
{
	return "abcdefghijklmnopqrstuvwxyz".ToUpper().ToArray();
}
public char[] GetKeys()
{
	return "abcdefghijklmnopqrstuvwxyz@".ToArray();
}

List<KeyValuePair<char, int[]>> GetTargets(char[,] map)
{
	List<KeyValuePair<char, int[]>> targets = new List<KeyValuePair<char, int[]>>();
	var keys = GetKeys();

	for (int i = 0; i < map.GetLength(0); i++)
	{
		for (int j = 0; j < map.GetLength(1); j++)
		{
			if (keys.Contains(map[i, j]))
			{
				targets.Add(new KeyValuePair<char, int[]>(map[i, j], new int[] { i, j }));
			}
		}
	}
	return targets;
}

// Define other methods, classes and namespaces here
char[,] GetMap(bool firstmap = false)
{
	Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));

	var s = File.ReadAllLines("..\\Data\\Day18.txt");

	var map = new char[s[0].Length, s.Length];

	for (int i = 0; i < s.Length; i++)
	{
		for (int j = 0; j < s[i].Length; j++)
		{
			map[i, j] = s[i][j];
		}
	}
	return map;
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

public class seekerResult
{
	public int Steps { get; set; }
	public List<KeyValuePair<int, int>> StepPath { get; set; }
	public List<char> DoorsInWay { get; set; }
	public bool FoundTarget { get; set; }
}

public class seeker
{
	public static int MaxSteps = 0;
	int steps { get; set; } = 0;
	int[] target { get; set; }
	Facing currentFacing = Facing.north;
	int[] CurrentPos { get; set; }
	public char[,] grid;
	public List<char> doorsInWay { get; set; } = new List<char>();
	char[] doors;
	List<KeyValuePair<int, int>> stepPlaces = new List<KeyValuePair<int, int>>();

	List<seeker> childSeekers = new List<seeker>();

	public seekerResult Seek()
	{

		while (true)
		{
			if (doors.Contains(grid[CurrentPos[0], CurrentPos[1]]))
			{
				doorsInWay.Add(grid[CurrentPos[0], CurrentPos[1]]);
			}

			grid[CurrentPos[0], CurrentPos[1]] = '8';
			var fls = GetForwardOpenFacings();

			if (CurrentPos[0] == target[0] && CurrentPos[1] == target[1])
			{
				stepPlaces.Add(new KeyValuePair<int, int>(CurrentPos[0], CurrentPos[1]));
				break;
			}

			switch (fls.Count)
			{
				case 1:
					Move(fls.First());
					currentFacing = fls.First();
					break;
				case 0:
					foreach (var sp in stepPlaces)
					{
						grid[sp.Key, sp.Value] = '.';
					}
					return new seekerResult
					{
						FoundTarget = false,
						DoorsInWay = null,
						StepPath = null,
						Steps = steps
					};
				default:
					stepPlaces.Add(new KeyValuePair<int, int>(CurrentPos[0], CurrentPos[1]));
					foreach (var f in fls)
					{
						var newPos = GetDirPos(f);
						grid[newPos[0], newPos[1]] = '8';
						childSeekers.Add(new seeker(target, steps + 1, grid, newPos, f, doors));
					}
					foreach (var s in childSeekers)
					{
						var result = s.Seek();
						if (result.FoundTarget)
						{
							result.DoorsInWay.AddRange(doorsInWay);
							result.StepPath.AddRange(stepPlaces);
							return result;
						}
						else
						{
							if (result.Steps > seeker.MaxSteps)
							{
								seeker.MaxSteps = result.Steps;
							}
						}
					}
					break;
			}
		}

		return new seekerResult
		{
			FoundTarget = true,
			DoorsInWay = new List<char>(doorsInWay),
			StepPath = stepPlaces,
			Steps = steps
		};
	}


	int[] GetDirPos(Facing f)
	{
		int[] newPos = new int[2];
		CurrentPos.CopyTo(newPos, 0);
		switch (f)
		{
			case Facing.north:
				newPos[0]--;
				break;
			case Facing.south:
				newPos[0]++;
				break;
			case Facing.west:
				newPos[1]--;
				break;
			case Facing.east:
				newPos[1]++;
				break;
			default:
				break;
		}
		return newPos;
	}

	void Move(Facing f)
	{
		stepPlaces.Add(new KeyValuePair<int, int>(CurrentPos[0], CurrentPos[1]));

		switch (f)
		{
			case Facing.north:
				CurrentPos[0]--;
				break;
			case Facing.south:
				CurrentPos[0]++;
				break;
			case Facing.west:
				CurrentPos[1]--;
				break;
			case Facing.east:
				CurrentPos[1]++;
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
				nextPos[0]--;
				break;
			case Facing.south:
				nextPos[0]++;
				break;
			case Facing.west:
				nextPos[1]--;
				break;
			case Facing.east:
				nextPos[1]++;
				break;
			default:
				break;
		}


		var charAtPosition = grid[nextPos[0], nextPos[1]];

		if (target[1] == 40 && target[0] == 40 && charAtPosition == '@')
		{
			return true;
		}

		return (charAtPosition != '#' && charAtPosition != '@' && charAtPosition != '8');
	}

	List<Facing> GetForwardOpenFacings()
	{
		List<Facing> fls = new List<Facing>();
		fls.Add(Facing.east);
		fls.Add(Facing.south);
		fls.Add(Facing.west);
		fls.Add(Facing.north);

		if (steps > 0)
		{
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
		//Util.ClearResults();
		//grid.Dump();
		//Thread.Sleep(10);
	}

	public seeker(int[] target, int startingSteps, char[,] grid, int[] pos, Facing initialFacing, char[] Doors)
	{
		steps = startingSteps;
		this.target = target;
		this.grid = grid;
		CurrentPos = pos;
		currentFacing = initialFacing;
		doors = Doors;
	}
}