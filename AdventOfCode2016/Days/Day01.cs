using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeScaffolding;

namespace AdventOfCode2016.Days
{
	[Challenge(01, "No Time for a Taxicab")]
	class Day01 : ChallengeBase
	{
		enum Direction
		{
			Left, Right
		}

		class Instruction
		{
			public Direction Direction {get;set;}
			public int Distance {get;set;}
		}

		IEnumerable<Instruction> ParseInstructions(string input)
		{
			foreach (var raw in input.Trim().Split(", "))
				yield return new Instruction{
					Direction = raw[0] == 'L' ? Direction.Left : Direction.Right,
					Distance = int.Parse(raw.Substring(1))
				};
		}

		struct Location
		{
			public int X, Y;
			public override int GetHashCode()
			{
				return HashCode.Combine(X, Y);
			}
			public override bool Equals(object obj)
			{
				return obj is Location && Equals((Location) obj);
			}
			public bool Equals(Location l)
			{
				return X == l.X && Y == l.Y;
			}
		}
		
		IEnumerable<Location> FollowInstructions(string input)
		{
			int x = 0, y = 0, dx = 0, dy = -1;

			foreach (var i in ParseInstructions(input))
			{
				if (i.Direction == Direction.Left)
				{
					var oldDx = dx;
					dx = dy;
					dy = -oldDx;
				}
				else
				{
					var oldDx = dx;
					dx = -dy;
					dy = oldDx;
				}

				for (int n = 1; n <= i.Distance; n++)
				{
					x += dx;
					y += dy;
					yield return new Location{X = x, Y = y};
				}
			}
		}

		public override object Part1(string input)
		{
			var end = FollowInstructions(input).Last();
			return end.X + end.Y;
		}

		public override object Part2(string input)
		{
			HashSet<Location> locations = new HashSet<Location>();
			foreach (var loc in FollowInstructions(input))
				if (locations.Contains(loc))
					return loc.X + loc.Y;
				else
					locations.Add(loc);
			return "no location is visited twice";
		}
	}
}
