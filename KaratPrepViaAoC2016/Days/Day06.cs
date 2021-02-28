using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeScaffolding;

namespace KaratPrepViaAoC2016.Days
{
	[Challenge(6, "Signals and Noise")]
	class Day06 : ChallengeBase
	{
		class OccurenceCounter<T>
		{
			private readonly Dictionary<T, int> counterDict = new Dictionary<T, int>();

			public void Increment(T t)
			{
				if (counterDict.ContainsKey(t))
					counterDict[t] = 1 + counterDict[t];
				else
					counterDict[t] = 0;
			}

			public T GetSingleMostCommon()
			{
				var maxCount = counterDict.Values.Max();
				return counterDict.Single(x => x.Value == maxCount).Key;
			}

			public T GetSingleLeastCommon()
			{
				var maxCount = counterDict.Values.Min();
				return counterDict.Single(x => x.Value == maxCount).Key;
			}
		}

		OccurenceCounter<char>[] CountCharsByPosition(string input)
		{
			var messages = input.ToLines().ToList();
			var len = messages[0].Length;
			var counters = Enumerable.Range(0, len).Select(x => new OccurenceCounter<char>()).ToArray();
			foreach (var message in messages)
				foreach (var x in message.Select((x,i) => new {c = x, index = i}))
					counters[x.index].Increment(x.c);
			return counters;
		}

		public override object Part1(string input)
		{
			var counters = CountCharsByPosition(input);
			return new string(counters.Select(x => x.GetSingleMostCommon()).ToArray());
		}

		public override object Part2(string input)
		{
			var counters = CountCharsByPosition(input);
			return new string(counters.Select(x => x.GetSingleLeastCommon()).ToArray());
		}
	}
}
