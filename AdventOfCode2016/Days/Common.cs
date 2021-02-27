using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
	/// <summary>
	/// Common extension methods used by multiple days.
	/// </summary>
	public static class CommonExtensions
	{
		/// <summary>
		/// Returns an enumeration of single-line strings, by default omitting blank lines.
		/// </summary>
		public static IEnumerable<string> ToLines(this string input, bool omitBlanks = true)
		{
			foreach (var rawLine in input.Split('\n'))
			{
				var line = rawLine.TrimEnd('\r');
				if (omitBlanks && string.IsNullOrWhiteSpace(line))
					continue;
				yield return line;
			}
		}
	}
}
