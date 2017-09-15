using System;
using System.Drawing;
using System.Collections.Generic;

namespace Genetic
{
	public class FitnessCalc
	{
		public static string Solution { get; set; }

		public static int Width { get; set; }

		public static int Height { get; set; }

		public static int GetFitness (Individual individual)
		{
			int fitness = 0;

			for (int i = 0; i < individual.Size;) {

				int[] colors = new int [2];

				bool assignFitness = true;

				for (int j = 0; j < 3; j++) {
					colors [0] = GeneticConverter.ByteStringToSubColor (individual.Genes.Substring (i, Constants.COLOR_SIZE));
					colors [1] = GetSolutionAtIndex(i);
					i += Constants.COLOR_SIZE;

					int delta = Math.Abs (colors [0] - colors [1]);
					if (delta > 20)
						assignFitness = false;
				}

				if (assignFitness)
					fitness++;

			}
			return fitness;
		}

		private static Dictionary<int,int> cached = new Dictionary<int, int> ();

		public static void InvalidateCache()
		{
			cached = new Dictionary<int, int> ();
		}

		public static int GetSolutionAtIndex (int index)
		{
			if (!cached.ContainsKey (index)) {
				cached.Add(index, GeneticConverter.ByteStringToSubColor (Solution.Substring (index, Constants.COLOR_SIZE)));
			}
			return cached [index];
		}


		public static int MaxFitness {
			get {
				int maxFitness = Solution.Length / (Constants.COLOR_SIZE * Constants.NUM_COLORS);
				return maxFitness;
			}
		}
	}
}

