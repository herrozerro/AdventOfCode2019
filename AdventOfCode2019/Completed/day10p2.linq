<Query Kind="Program" />

void Main()
{
	var s = GetLinesFromFile().Select(x => x.Replace(",", "").ToArray()).ToArray();

	var arr = new char[s.GetLength(0), s.GetLength(0)];

	for (int i = 0; i < s.GetLength(0); i++)
	{
		for (int j = 0; j < s[0].GetLength(0); j++)
		{
			arr[i, j] = s[i][j];
		}
	}
	int[] centerpoint = new int[] { 20,23 };

	arr[centerpoint[0], centerpoint[1]] = '*';
	arr.Dump();

	var angles = new Dictionary<float, List<Asteroid>>();

	for (int i = 0; i < s.GetLength(0); i++)
	{
		for (int j = 0; j < s[0].GetLength(0); j++)
		{
			if (arr[i, j] == '#')
			{
				var angle = (float)(Math.Atan2((centerpoint[0] - i), (centerpoint[1] - j)) * (180 / (Math.PI)));

				var x1 = (angle < 0 ? 360 : 0);
				angle = angle + (angle < 0 ? 360 : 0);
				angle = angle + (angle < 90 ? 360 : 0);
				
				
				//Console.WriteLine($"x{j} y{i} angle: {angle}");
				if (!angles.ContainsKey(angle))
				{
					angles[angle] = new List<Asteroid>();
				}

				angles[angle].Add(new Asteroid() { coords = $"x{j} y{i}", dist = Distance(centerpoint, new int[] { j, i }), x = j, y = i });

			}
		}
	}
	var targets = angles.OrderBy(a => a.Key).ToList().Dump();
	Asteroid lasthit = new Asteroid();
	int countdown = 200;
	int it = 0;
	while (countdown > 0)
	{
		if (targets[it].Value.Count > 0)
		{
			var tdist = targets[it].Value.Min(x => x.dist);
			var t = targets[it].Value.First(v => v.dist == tdist);
			lasthit = t;
			Console.WriteLine($"PEW! {t.coords}");
			targets[it].Value.Remove(t);
			countdown--;
		}
		it++;

		if (it >= targets.Count())
		{
			it = 0;
		}
	}
	lasthit.Dump();
}

// Define other methods, classes and namespaces here
public string[] GetLinesFromFile()
{
	var lines = System.IO.File.ReadAllLines(@"C:\Users\jmbradbu\source\Github\p\AdventOfCode2019\AdventOfCode2019\Data\Day10.txt");

	return lines;
}

float Distance(int[] a, int[] b)
{
	return (float)Math.Sqrt(Math.Pow(a[1] - b[0], 2) + Math.Pow(a[0] - b[1], 2));
}

public class Asteroid
{
	public string coords { get; set; }
	public float dist { get; set; }
	public int x { get; set; }
	public int y { get; set; }
	public int answer
	{
		get { return (x * 100) + y; }
	}
}