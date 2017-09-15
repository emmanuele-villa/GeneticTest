using System;
using System.Drawing;

namespace Genetic
{
	public class Individual
	{
		public string Genes;

		private int fitness = 0;

		public void GenerateIndividual (int geneLenght)
		{
			Genes = GenerateRandomImage (geneLenght);
		}

		public static string GenerateRandomImage (int geneLenght)
		{
			var chromosome = "";
			var random = new Random ();

			for (int i = 0; i < geneLenght; i++) {
				int next = random.Next (0, 2);
				chromosome += next;
			}

			return chromosome;
		}

		public void Save (string filename)
		{
			var bitmap = GeneticConverter.ByteStringToBitmap (Genes);
			bitmap.Save (filename);
		}

		public int Size { 
			get {
				return Genes.Length;
			}
		}

		public int Fitness {
			get {
				if (fitness == 0) {
					fitness = FitnessCalc.GetFitness (this);
				}
				return fitness;
			}
		}

		public override String ToString ()
		{
			return Genes;
		}
	}
}

