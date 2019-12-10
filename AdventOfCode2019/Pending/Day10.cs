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

			var mostHits = new KeyValuePair<string, int>("", 0);
			var lines = Utilities.GetLinesFromFile("Day10.txt").Select(x => x.Replace(",", "").ToArray()).ToArray();

			var arr = new char[lines.GetLength(0), lines.GetLength(0)];

			for (int i = 0; i < lines.GetLength(0); i++)
			{
				for (int j = 0; j < lines[0].GetLength(0); j++)
				{
					arr[i, j] = lines[i][j];
				}
			}

			//from center point get angles and distances to all other points, then sweep through 360 degrees popping off each one.

			Console.WriteLine($"Most Hits is: {mostHits.Key} With {mostHits.Value} Hits");
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
						if (i == 2 && j == 2)
						{
							var s = 1;
						}

						//find slopes
						var xslope = (coordx - j) * -1;
						var yslope = (coordy - i) * -1;

						float slope = 0;

						//just looking for slopes of 1
						if (xslope != 0)
						{
							slope = (float)(coordy - i) / (coordx - j);

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

							var n = Simplify(new int[] { Math.Abs(xslope), Math.Abs(yslope) });

							if (xneg)
							{
								xslope = n[0] * -1;
							}
							else
							{
								xslope = n[0];
							}
							if (yneg)
							{
								yslope = n[1] * -1;
							}
							else
							{
								yslope = n[1];
							}

						}

						var k = 1;
						bool foundRock = false;
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
	}
}
