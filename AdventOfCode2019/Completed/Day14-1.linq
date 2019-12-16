<Query Kind="Program" />

void Main()
{
	//5816882988 wrong
	Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));

	var lines = File.ReadAllLines("..\\Data\\Day14t2.txt").ToList();

	List<reaction> ls = new List<UserQuery.reaction>();

	var fuel = lines.First(l => l.EndsWith("FUEL"));

	var fuelReaction = new reaction(fuel, lines, 1);

	fuelReaction.Dump();

	var oreInputs = lines.Where(l => l.Contains("ORE") && !l.EndsWith("ORE"))
	.Select(l => new { p = l.Split(" ")[4], f = int.Parse(l.Split(" ")[3]), o = int.Parse(l.Split(" ")[0]) });
	//oreInputs.Dump();
	//oreReactions.Dump();
	oreReactionsCount.Dump();

	var unore = UnusedOre.Select(uo =>
	new unuesedOre()
	{
		ore = uo.Key,
		unused = (int)uo.Value,
		factor = int.Parse(lines.First(l => l.EndsWith(uo.Key)).Split("=>")[1].Trim().Split(" ")[0]),
	}).ToList().Dump("unused");
	
	var orerebate = 0;

	decimal total = 0;
	foreach (var or in oreReactionsCount)
	{
		var io = oreInputs.First(i => i.p == or.Key);
		var m = Math.Ceiling((decimal)or.Value / io.f);
		$"{or.Value}*{io.o} - tot: {or.Value * io.o}".Dump();
		total += m * io.o;
	}

	//return unused
	while (unore.Any(x => x.unused > x.factor))
	{
		for (int i = 0; i < unore.Count(); i++)
		{
			var o = unore[i];
			var react = lines.First(l => l.EndsWith(o.ore));
			var revVals = react.Split("=>")[0];
			var revs = revVals.Split(",");
			var cycles = o.unused / o.factor;
			for (int j = 0; j < cycles; j++)
			{
				foreach (var rv in revs)
				{
					var ore = rv.Trim().Split(" ")[1];
					var refund = int.Parse(rv.Trim().Split(" ")[0]);
					
					if (ore == "ORE")
					{
						orerebate += refund;
					}
					else
					{
						unore.First(u => u.ore == ore).unused += refund;
					}
					
					
				}
				
			}
			o.unused -= cycles * o.factor;
		}
	}
	unore.Dump("unused");
	
	total.Dump();
	
	orerebate.Dump();
	
	(total - orerebate).Dump();
}


public class unuesedOre
{
	public string ore { get; set; }
	public int unused { get; set; }
	public int factor { get; set; }
}

static Dictionary<string, decimal> UnusedOre = new Dictionary<string, decimal>();

static Dictionary<string, decimal> oreReactions = new Dictionary<string, decimal>();
static Dictionary<string, decimal> oreReactionsCount = new Dictionary<string, decimal>();


class reaction
{
	public string ID { get; set; }
	public long output { get; set; }
	public List<string> reactions { get; set; } = new List<string>();

	public decimal reactionsNeeded { get; set; }
	public decimal amountNeeded { get; set; } = 0;
	public decimal excessAmt { get; set; }
	public List<reaction> ParentReactions { get; set; } = new List<reaction>();

	public reaction(string inp, List<string> lines, decimal needed)
	{
		var inout = inp.Split("=>");
		ID = inout.Last().Trim().Split(" ")[1].Trim();
		output = int.Parse(inout.Last().Trim().Split(" ")[0].Trim());
		amountNeeded = needed;
		reactionsNeeded = Math.Ceiling((decimal)amountNeeded / output);

		excessAmt = (output * reactionsNeeded) - amountNeeded;

		if (!UnusedOre.ContainsKey(ID))
		{
			UnusedOre.Add(ID, excessAmt);
		}
		else
		{
			UnusedOre[ID] += excessAmt;
		}

		foreach (var element in inout[0].Split(","))
		{
			reactions.Add(element.Trim());
		}

		foreach (var r in reactions)
		{

			var prID = r.Split(" ")[1];
			if (prID == "ORE")
			{
				if (!oreReactions.ContainsKey(ID))
				{
					oreReactions.Add(ID, reactionsNeeded);
				}
				else
				{
					oreReactions[ID] += reactionsNeeded;
				}

				if (!oreReactionsCount.ContainsKey(ID))
				{

					oreReactionsCount.Add(ID, amountNeeded);
				}
				else
				{
					oreReactionsCount[ID] += amountNeeded;
				}

				continue;

			}
			var prNeeded = int.Parse(r.Split(" ")[0]);
			var pReact = lines.First(l => l.EndsWith(prID));
			ParentReactions.Add(new reaction(pReact, lines, prNeeded * reactionsNeeded));
		}



	}
}