using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using AdventOfCodeScaffolding;

namespace KaratPrepViaAoC2016.Days
{
	[Challenge(5, "How About a Nice Game of Chess?")]
	class Day05 : ChallengeBase
	{
        static readonly MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();

		static string GetMD5(string input)
        {
			var inBytes = ASCIIEncoding.ASCII.GetBytes(input);
            var outBytes = md5Provider.ComputeHash(inBytes);
			var md5 = Convert.ToHexString(outBytes);
			return md5;
        }

		public override object Part1(string rawInput)
		{
			string input = rawInput.Trim();
			string output = "";
			int index = -1;
			while (output.Length < 8)
			{
				string md5;
				do
				{
					++index;
					md5 = GetMD5(input + index.ToString());
				}
				while (!md5.StartsWith("00000"));

				output += md5.ToLower()[5];
			}
			return output;
		}

		public override object Part2(string rawInput)
		{
			string input = rawInput.Trim();
			char[] output = new char[8];
			Array.Fill(output, ' ');

			int index = -1;
			int placed = 0;
			while (placed < 8)
			{
				string md5;
				do
				{
					++index;
					md5 = GetMD5(input + index.ToString());
				}
				while (!md5.StartsWith("00000"));

				char rawPos = md5[5];
				if (!char.IsDigit(rawPos))
					continue;
				int pos = int.Parse(rawPos.ToString());
				if (pos > 7)
					continue;
				if (output[pos] != ' ')
					continue;
				output[pos] = md5.ToLower()[6];
				placed++;
			}
			return new string(output);
		}
	}
}
