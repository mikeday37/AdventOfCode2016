using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeScaffolding;

namespace KaratPrepViaAoC2016.Days
{
	[Challenge(3, "Squares With Three Sides")]
	class Day03 : ChallengeBase
	{
		class Triangle
		{
			public int A {get;set;}
			public int B {get;set;}
			public int C {get;set;}
			public Triangle(int a, int b, int c)
			{
				A = a; B = b; C = c;
			}
			public bool IsValid {get{
				var s = new int[]{A,B,C};
				Array.Sort(s);
				return s[0] + s[1] > s[2];
			}}
		}

		static IEnumerable<Triangle> ParseTriangles(string input)
		{
			foreach (var line in input.ToLines())
			{
				var parts = line.Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x.Trim())).ToArray();
				yield return new Triangle(parts[0], parts[1], parts[2]);
			}
		}

		static IEnumerable<Triangle> ParseTrianglesByColumn(string input)
		{
			List<Triangle> buffer = new List<Triangle>();
			foreach (var t in ParseTriangles(input))
			{
				buffer.Add(t);
				if (buffer.Count == 3)
				{
					yield return new Triangle(buffer[0].A, buffer[1].A, buffer[2].A);
					yield return new Triangle(buffer[0].B, buffer[1].B, buffer[2].B);
					yield return new Triangle(buffer[0].C, buffer[1].C, buffer[2].C);
					buffer.Clear();
				}
			}
		}

		public override object Part1(string input)
		{
			return ParseTriangles(input).Where(x => x.IsValid).Count();
		}

		public override object Part2(string input)
		{
			return ParseTrianglesByColumn(input).Where(x => x.IsValid).Count();
		}
	}
}
