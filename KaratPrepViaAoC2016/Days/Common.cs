using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaratPrepViaAoC2016.Days
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

	public struct Point
	{
		public int X {get; private set;}
		public int Y {get; private set;}

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public Point(Point p)
			: this(p.X, p.Y)
		{
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y);
		}

		public override bool Equals(object obj)
		{
			return obj is Point && Equals((Point) obj);
		}

		public bool Equals(Point p)
		{
			return X == p.X && Y == p.Y;
		}

		public static bool operator ==(Point left, Point right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Point left, Point right)
		{
			return !(left == right);
		}

		public static Point operator +(Point left, Point right)
		{
			return new Point(left.X + right.X, left.Y + right.Y);
		}

		public static Point operator -(Point left, Point right)
		{
			return new Point(left.X - right.X, left.Y - right.Y);
		}

		public static Point operator -(Point right)
		{
			return new Point(-right.X, -right.Y);
		}

		public static Point operator *(Point left, int n)
		{
			return new Point(left.X * n, left.Y * n);
		}

		public static Point operator *(int n, Point right)
		{
			return right * n;
		}

		/// <summary>
		/// Returns this point clipped to rectangle indicated by the given corners.
		/// Clipping treats the corner coordinates as inclusive.
		/// </summary>
		public Point Clip(Point a, Point b)
		{
			var min = new Point(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));
			var max = new Point(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));
			return new Point(
				Math.Max(min.X, Math.Min(max.X, this.X)),
				Math.Max(min.Y, Math.Min(max.Y, this.Y))
			);
		}
	}
}
