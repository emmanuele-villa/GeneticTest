using System;

namespace Genetic
{
	public class Constants
	{
		public const int COLOR_SIZE = 4;
		public const int MAX_GENES_NUMBER = 10;
		public const int NUM_COLORS = 3;

		public static int MaxSize {
			get {
				return COLOR_SIZE * MAX_GENES_NUMBER * NUM_COLORS;
			}
		}
	}
}

