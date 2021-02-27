using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeScaffolding;

namespace KaratPrepViaAoC2016.Days
{
	[Challenge(2, "Bathroom Security")]
	class Day02 : ChallengeBase
	{
		static readonly IReadOnlyList<Point> directionOffsets = new Point[]{
			new Point( 0, -1), // 0 = up
			new Point( 1,  0), // 1 = right
			new Point( 0,  1), // 2 = down
			new Point(-1,  0)  // 3 = left
		};

		static string GetCode(string rawProcedure, IReadOnlyList<string> numpad, Point start)
		{
			Point p = start;
			Func<char, int> offsetToDir = c => {
				switch (c)
				{
					case 'U': return 0;
					case 'R': return 1;
					case 'D': return 2;
					case 'L': return 3;
					default: throw new Exception("Unknown direction: " + c);
				}
			};

			string code = "";

			foreach (var line in rawProcedure.ToLines())
			{
				foreach (var c in line.Trim())
				{
					var old = p;
					p += directionOffsets[offsetToDir(c)];
					if (numpad[p.Y][p.X] == '.')
						p = old;
				}
				code += numpad[p.Y][p.X];
			}
			
			return code;
		}

		public override object Part1(string input)
		{
			return GetCode(input, new string[]{
				".....",
				".123.",
				".456.",
				".789.",
				"....."
			}, new Point(2,2));
		}

		public override object Part2(string input)
		{
			return GetCode(input, new string[]{
				".......",
				"...1...",
				"..234..",
				".56789.",
				"..ABC..",
				"...D...",
				"......."
			}, new Point(1,3));
		}
	}
}
