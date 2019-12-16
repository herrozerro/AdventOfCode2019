<Query Kind="Program">
  <Output>DataGrids</Output>
</Query>

void Main()
{
	Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));

	var lines = File.ReadAllLines("..\\Data\\Day14.txt").ToList();

	var reactors = new List<reactor>();

	foreach (var l in lines)
	{
		var r = new reactor();
		var inputs = l.Split("=>")[0].Trim();
		var output = l.Split("=>").Select(s => s.Trim()).ToArray()[1];
		r.output = output.Split(" ")[1];
		r.outputAmount = int.Parse(output.Split(" ")[0]);

		r.inputStr = inputs;

		if (inputs.Contains("ORE"))
		{
			r.oreRate = int.Parse(inputs.Split(" ")[0]);
		}

		reactors.Add(r);
	}
	for (int i = 0; i < reactors.Count(); i++)
	{
		reactors[i].GetInputs(reactors);
	}

	var start = reactors.First(r => r.output == "FUEL");

	start.React(1);
	//reactors.OrderBy(r => r.inputs.Count()).Dump();
	reactors.Sum(r => r.oreConsumed).Dump();
	
	reactors.ForEach(r => r.Reset());

	start.React(1);
	//reactors.OrderBy(r => r.inputs.Count()).Dump();
	reactors.Sum(r => r.oreConsumed).Dump();
	long final = 0;
	//Part 2
	for (long i = 8840000; i < 1000000000000; i++)
	{
		//Util.ClearResults();
		reactors.ForEach(r => r.Reset());
		start.React(i);
		if (reactors.Sum(r => (long)r.oreConsumed) >= 1000000000000)
		{
			$"{i} results in {reactors.Sum(r => (long)r.oreConsumed)}".Dump();
			final = i;
			break;
		}
		
		//$"{i} results in {reactors.Sum(r => (long)r.oreConsumed)}".Dump();
	}
}

class reactor
{
	public string output { get; set; }
	public long outputAmount { get; set; }
	public long oreConsumed { get; set; }
	public long oreRate { get; set; }
	public long excess = 0;
	public string inputStr { get; set; }
	public List<KeyValuePair<int, reactor>> inputs { get; set; } = new List<KeyValuePair<int, reactor>>();
	public long AmountNeeded {get; set;}
	public long AmountGenerated {get;set;}
	
	public void GetInputs(List<reactor> reactors)
	{
		var strs = inputStr.Split(", ");
		foreach (var s in strs)
		{
			if (s.Contains("ORE"))
			{
				return;
			}
			var r = reactors.FirstOrDefault(re => re.output == s.Split(" ")[1]);
			inputs.Add(new KeyValuePair<int, reactor>(int.Parse(s.Split(" ")[0]), r));
		}
	}

	public void Reset(){
		AmountGenerated = 0;
		oreConsumed = 0;
		AmountNeeded = 0;
		excess = 0;
	}

	public void React(long needed)
	{
		AmountNeeded += needed;
		
		if (output == "GPVTF")
		{
			//1.Dump();
		}
		if (needed <= excess)
		{
			excess -= needed;
			return;
		}
		
		needed -= excess;
		excess = 0;
		
		var reactionsNeeded = (long)Math.Ceiling((decimal)needed / outputAmount);
		
		if (inputs.Count() > 0)
		{
			foreach (var inp in inputs)
			{
				inp.Value.React(inp.Key * reactionsNeeded);
			}
		}
		else
		{
			oreConsumed += reactionsNeeded * oreRate;
		}
		
		var con = ((long)Math.Ceiling((decimal)needed / outputAmount) * outputAmount);
		AmountGenerated += con;
		
		excess += (con - needed) > 0 ? con - needed : 0;
	}
}