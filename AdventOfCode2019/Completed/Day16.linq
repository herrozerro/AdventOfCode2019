<Query Kind="Program" />

void Main()
{
	string inpSig = "59758034323742284979562302567188059299994912382665665642838883745982029056376663436508823581366924333715600017551568562558429576180672045533950505975691099771937719816036746551442321193912312169741318691856211013074397344457854784758130321667776862471401531789634126843370279186945621597012426944937230330233464053506510141241904155782847336539673866875764558260690223994721394144728780319578298145328345914839568238002359693873874318334948461885586664697152894541318898569630928429305464745641599948619110150923544454316910363268172732923554361048379061622935009089396894630658539536284162963303290768551107950942989042863293547237058600513191659935";
	int offest = 5975803;
	//string inpSig ="12345678";
	string realSig = "";
	for (int i = 0; i < 5000; i++)
	{
		realSig += inpSig;
	}
	string output = "";
	
	for (int j = 0; j < 100; j++)
	{
		for (int i = 0; i < inpSig.Length; i++)
		{
			var pattern = GetPattern(i + 1, inpSig.Length);
			var s = ProcessSignal2(inpSig, i);
			output += s;
		}
		inpSig = output;
		output = "";
		inpSig.Dump();
	}
	Util.ClearResults();
	inpSig.Dump("Part 1");
	
	var r = ProcessSignal4(realSig, 100);
	//Util.ClearResults();
	r = r + r;
	r.Substring(offest, 8).Dump("Part 2");
}

// Define other methods, classes and namespaces here

public List<int> GetPattern(int phase, int length)
{
	var pat = new List<int>();
	var basePat = new List<int>() { 0, 1, 0, -1 };
	bool isGenerating = true;
	while (isGenerating)
	{
		foreach (var bp in basePat)
		{
			for (int i = 0; i < phase; i++)
			{
				pat.Add(bp);
				if (pat.Count >= length + 1)
				{
					pat.RemoveAt(0);
					return pat;
				}
			}
		}
	}
	return pat;
}

public string ProcessSignal(string inp, List<int> pattern, int offset)
{

	List<int> outputInts = new List<int>();

	for (int i = offset; i < inp.Length; i++)
	{
		for (int j = 0; j < offset + 1; j++)
		{
			if (pattern[i] == 0)
			{
				continue;
			}
			var d = int.Parse(inp[i].ToString());
			var m = pattern[i];
			outputInts.Add((d * m));
			i++;
			if (i >= inp.Length)
			{
				break;
			}
		}
		i += offset;
	}


	var sum = outputInts.Sum();

	return sum.ToString().Last().ToString();
}

public string ProcessSignal2(string inp, int offset)
{
	List<int> iteration = inp.ToList().Select(x => int.Parse(x.ToString())).ToList();
	long product = 0;
	var pat = offset + 1;
	var patIndex = 0;
	var mode = 1;
	for (int i = offset; i < inp.Length; i++)
	{
		patIndex++;
		switch (mode)
		{
			case 1: //add
				var x = iteration[i];
				product += x;
				if (patIndex == pat)
				{
					mode++;
					patIndex = 0;
				}
				break;
			case 0: //0 so skip to next pattern
			case 2: //0 so skip to next pattern
				i += offset;
				mode++;
				patIndex = 0;
				break;
			case 3: //subtract
				var y = iteration[i];
				product -= y;
				if (patIndex == pat)
				{
					mode = 0;
					patIndex = 0;
				}
				break;
		}
		
	}


	return product.ToString().Last().ToString();
}

public string ProcessSignal3(string inp, int iterations)
{
	var siglen = inp.Length;
	var fh = inp.Substring(0,(inp.Length/2)-1);
	inp = inp.Substring((inp.Length/2));
	for (int i = 0; i < iterations; i++)
	{
		List<int> iteration = inp.ToList().Select(x => int.Parse(x.ToString())).ToList();
		iteration.Reverse();
		inp = "";
		int prevIteration = 1;
		foreach (var iter in iteration.Skip(1))
		{
			var prevsequence = iteration.Take(prevIteration);

			var sums = prevsequence.Sum().ToString().Last().ToString();

			inp = sums + inp;

			prevIteration++;
			if (prevIteration == siglen/2)
			{
				break;
			}
		}
		inp = iteration.Sum().ToString().Last().ToString() + inp;
		(fh + inp).Dump();
	}
	return (fh + inp);
}

public string ProcessSignal4(string inp, int iterations)
{
	List<int> iteration = inp.ToList().Select(x => int.Parse(x.ToString())).ToList();
	for (int i = 0; i < iterations; i++)
	{
		var sum = 0;
		for (int j = iteration.Count()-1; j >= 0; j--)
		{
			sum += iteration[j];
			
			iteration[j] = sum%10;
		}
		
		//iteration.Dump();
	}
	return string.Join("",iteration);
}