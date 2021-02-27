using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeScaffolding;

namespace KaratPrepViaAoC2016.Days
{
	[Challenge(4, "Security Through Obscurity")]
	class Day04 : ChallengeBase
	{
		class RoomRecord
		{
			public string FullText {get; private set;}
			public string EncryptedName {get; private set;}
			public string RawSectorId {get; private set;}
			public int SectorId {get; private set;}
			public string Checksum {get; private set;}
			public string RealChecksum {get; private set;}
			public bool IsReal { get; private set; }
			public string RealName {get; private set;}

			public static RoomRecord Parse(string rawText)
			{
				return new RoomRecord(rawText);
			}

			public RoomRecord(string rawText)
			{
				FullText = rawText.Trim();

				var parts = FullText.Split('-');

				EncryptedName = string.Join('-', parts.Take(parts.Length - 1));

				var suffix = parts.Last();
				var suffixParts = suffix.Split('[');

				RawSectorId = suffixParts[0];
				Checksum = suffixParts[1].TrimEnd(']');
				SectorId = int.Parse(RawSectorId);

				var letters = EncryptedName.Replace("-", "");
				var letterCounts = new Dictionary<char, int>();
				foreach (var l in letters)
				{
					if (!letterCounts.ContainsKey(l))
						letterCounts[l] = 0;
					letterCounts[l] = 1 + letterCounts[l];
				}

				RealChecksum = new string(
						letterCounts
							.OrderByDescending(x => x.Value)
							.ThenBy(x => x.Key)
							.Select(x => x.Key)
							.ToArray()
					)
					.Substring(0, 5);
				IsReal = RealChecksum == Checksum;

				RealName = IsReal ? Decrypt() : null;
			}

			const string alphabet = "abcdefghijklmnopqrstuvwxyz";

			private string Decrypt()
			{
				StringBuilder sb = new StringBuilder();

				foreach (var c in EncryptedName)
				{
					if (c == '-')
						sb.Append(' ');
					else
						sb.Append(alphabet[(SectorId + alphabet.IndexOf(c)) % 26]);
				}

				return sb.ToString();
			}
		}

		static IEnumerable<RoomRecord> ParseRoomRecords(string input)
		{
			return input.ToLines().Select(RoomRecord.Parse);
		}

		public override object Part1(string input)
		{
			return ParseRoomRecords(input).Where(x => x.IsReal).Select(x => x.SectorId).Sum();
		}

		public override object Part2(string input)
		{
			var candidates = ParseRoomRecords(input)
				.Where(x => x.IsReal)
				.Where(x => x.RealName.Contains("north pole") || x.RealName.Contains("northpole"))
				.ToList();
			return candidates.Single().SectorId;
		}
	}
}
