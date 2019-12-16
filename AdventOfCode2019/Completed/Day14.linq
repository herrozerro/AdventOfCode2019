<Query Kind="Program" />

void Main()
{
	Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));

	var lines = File.ReadAllLines("..\\Data\\Day14t1.txt").ToList();



	List<reaction> ls = new List<UserQuery.reaction>();

	var fuel = lines.First(l => l.EndsWith("FUEL"));

	var fuelReaction = new reaction(fuel, lines);

	fuelReaction.Dump();
	
	int oreneeded = 0;
	
	foreach (var element in sequence)
	{
		fuel
	}
}



class reaction
{
	public string ID { get; set; }

	public List<string> reactions { get; set; } = new List<string>();

	public List<reaction> ParentReactions { get; set; } = new List<reaction>();

	public long reactionsNeeded { get; set; } = 0;

	public reaction(string inp, List<string> lines)
	{
		var inout = inp.Split("=>");
		ID = inout.Last().Split(" ")[1].Trim();
		reactionsNeeded = long.Parse(inout.Last().Trim().Split(" ")[0].Trim());
		foreach (var element in inout[0].Split(","))
		{
			reactions.Add(element.Trim());
		}


		if (ID != "ORE")
		{
			foreach (var r in reactions)
			{
				var s = lines.FirstOrDefault(l => l.EndsWith(r.Split(" ")[1]));
				if (r.Contains("ORE"))
				{
					s = r;
				}
				Console.WriteLine(s);
				ParentReactions.Add(new reaction(s, lines));
			}
		}


	}
}
