using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2019
{
    public static class Day10
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 10");

            P1();
			P2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

		public static void P1()
		{

			var mostHits = new KeyValuePair<string, int>("",0);
			var lines = Utilities.GetLinesFromFile("Day10.txt").Select(x => x.Replace(",", "").ToArray()).ToArray();

			var arr = new char[lines.GetLength(0), lines.GetLength(0)];

			for (int i = 0; i < lines.GetLength(0); i++)
			{
				for (int j = 0; j < lines[0].GetLength(0); j++)
				{
					arr[i, j] = lines[i][j];
				}
			}

			for (int i = 0; i < arr.GetLength(0); i++)
			{
				for (int j = 0; j < arr.GetLength(1); j++)
				{
					if (arr[i,j] == '#')
					{
						var hits = SearchRadius(CopyArray(arr), i, j);

						if (hits > mostHits.Value)
						{
							mostHits = new KeyValuePair<string, int>( $"x{j} y{i}", hits);
						}

						Console.WriteLine($"x{j} y{i} Hits {hits}");
					}
				}
			}

			Console.WriteLine($"Most Hits is: {mostHits.Key} With {mostHits.Value} Hits");
		}

		public static void P2()
		{
			var s = Utilities.GetLinesFromFile("Day10.txt").Select(x => x.Replace(",", "").ToArray()).ToArray();

			var arr = new char[s.GetLength(0), s.GetLength(0)];

			for (int i = 0; i < s.GetLength(0); i++)
			{
				for (int j = 0; j < s[0].GetLength(0); j++)
				{
					arr[i, j] = s[i][j];
				}
			}
			int[] centerpoint = new int[] { 20, 23 };

			arr[centerpoint[0], centerpoint[1]] = '*';

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
			var targets = angles.OrderBy(a => a.Key).ToList();
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
			Console.WriteLine($"Last ateroid hit: {lasthit.coords} answer is: {lasthit.answer}");
		}

		public static int SearchRadius(char[,] field, int coordy, int coordx)
		{
			int hits = 0;

			field[coordy, coordx] = '*';

			for (int i = 0; i < field.GetLength(0); i++)
			{
				for (int j = 0; j < field.GetLength(1); j++)
				{
					if (field[i, j] == '#')
					{
						//for breaking on a specific point
						if (i == 2 && j == 2)
						{
							var s = 1;
						}

						//find slopes
						var xslope = (coordx - j) * -1;
						var yslope = (coordy - i) * -1;

						float slope = 0;

						//Get slopes, and simplify for searching.
						if (xslope != 0)
						{
							slope = (float)(coordy - i) / (coordx - j);

							//Get if either x/y are negative
							var xneg = false;
							var yneg = false;
							if (xslope < 0)
							{
								xneg = true;
							}
							if (yslope < 0)
							{
								yneg = true;
							}

							//Simplify
							var n = Simplify(new int[] { Math.Abs(xslope), Math.Abs(yslope) });
							xslope = n[0];
							yslope = n[1];

							//put negatives back
							if (xneg)
							{
								xslope = xslope * -1;
							}
							if (yneg)
							{
								yslope = yslope * -1;
							}

						}

						var k = 1;
						bool foundRock = false;
						//Search Entire slope from coords
						while (true)
						{
							//get next search coord, starting point + slope * iteration
							var searchx = coordx + (xslope * k);
							var searchy = coordy + (yslope * k);
							//flat x slope
							if (xslope == 0)
							{
								if (yslope > 0)
								{
									searchy = coordy + k;
								}
								else
								{
									searchy = coordy - k;
								}

							}
							//flat y slope
							if (yslope == 0)
							{
								if (xslope > 0)
								{
									searchx = coordx + k;
								}
								else
								{
									searchx = coordx - k;
								}

							}
							//diagonal slope
							if (Math.Abs(slope) == 1)
							{
								if (xslope > 0)
								{
									searchx = coordx + k;
								}
								else
								{
									searchx = coordx - k;
								}

								if (yslope > 0)
								{
									searchy = coordy + k;
								}
								else
								{
									searchy = coordy - k;
								}
							}

							//is my next spot in bounds?
							if (searchx >= 0 && searchy >= 0 && searchx < field.GetLength(0) && searchy < field.GetLength(1))
							{
								//is the next spot an asteroid?
								if (field[searchy, searchx] == '#' || field[searchy, searchx] == '!')
								{
									//if we havent found a rock yet mark it with !
									if (!foundRock)
									{
										field[searchy, searchx] = '!';
										hits++;
										foundRock = true;
									}
									//if we've found a rock already, Block it with a B
									else
									{
										field[searchy, searchx] = 'B';
									}
								}
								k++;
							}
							else
							{
								break;
							}
						}
					}
				}
			}

			//field.Dump();
			hits = 0;
			for (int i = 0; i < field.GetLength(0); i++)
			{
				for (int j = 0; j < field.GetLength(1); j++)
				{
					if (field[i, j] == '!')
					{
						hits++;
					}
				}
			}
			return hits;
		}

		static int[] Simplify(int[] numbers)
		{
			int gcd = GCD(numbers);
			for (int i = 0; i < numbers.Length; i++)
				numbers[i] /= gcd;
			return numbers;
		}
		static int GCD(int a, int b)
		{
			while (b > 0)
			{
				int rem = a % b;
				a = b;
				b = rem;
			}
			return a;
		}
		static int GCD(int[] args)
		{
			// using LINQ:
			return args.Aggregate((gcd, arg) => GCD(gcd, arg));
		}


		public static char[,] CopyArray(char[,] arr)
		{
			var arr2 = new char[arr.GetLength(0), arr.GetLength(1)];
			for (int i = 0; i < arr.GetLength(0); i++)
			{
				for (int j = 0; j < arr.GetLength(1); j++)
				{
					arr2[i, j] = arr[i,j];
				}
			}
			return arr2;
		}

		static float Distance(int[] a, int[] b)
		{
			return (float)Math.Sqrt(Math.Pow(a[1] - b[0], 2) + Math.Pow(a[0] - b[1], 2));
		}

		
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
}
