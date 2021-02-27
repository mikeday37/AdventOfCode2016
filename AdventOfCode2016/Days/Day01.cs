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
					Distance = int.Parse(raw[1..])
				};
		}

		public override object Part1(string input)
		{
			int x = 0, y = 0, dx = 0, dy = -1;

			foreach (var i in ParseInstructions(input))
			{
				if (i.Direction == Direction.Left)
					(dx, dy) = (dy, -dx);
				else
					(dx, dy) = (-dy, dx);

				x += dx * i.Distance;
				y += dy * i.Distance;
			}

			return x + y;
		}

		public override object Part2(string input)
		{
			return -1;
		}
	}
}
