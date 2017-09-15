using System;
using System.Drawing;
using System.Threading;
using System.Text;
using System.IO;

namespace Genetic
{
	class MainClass
	{

		public static void Main (string[] args)
		{
			//Load Image
            //TODO: change path to your soluzione.bmp
			var solutionImage = Bitmap.FromFile ("C:\\Users\\emmanuele.villa.FINCONSGROUP\\Pictures\\soluzione.bmp");
			var solutionBitmap = new Bitmap (solutionImage);
			solutionBitmap.Save ("loaded_image.bmp");

			//set the solution image properties into the fitness calculator
			string fullSolution = GeneticConverter.BitmapToByteString (solutionBitmap);
			string randomImage = GenerateRandomImage (fullSolution.Length);

			FitnessCalc.Width = solutionBitmap.Width;
			FitnessCalc.Height = solutionBitmap.Height;

			//check if the load&save works good
			Individual solution = new Individual ();
			solution.Genes = fullSolution;
			solution.Save ("solution.bmp");

			Console.WriteLine ("Loaded image with size of " + fullSolution.Length);

			int cycles = fullSolution.Length / (Constants.MaxSize);
			if (fullSolution.Length % Constants.MaxSize > 0)
				cycles++;

			Console.WriteLine ("Genes lenght: " + Constants.MaxSize + ", number of cycles needed: " + cycles);

			int count = 0;
			var start = DateTime.Now;

			for (int i = 0; i < cycles; i++) {
				
				Console.WriteLine ();
				Console.WriteLine (string.Format ("Started cycle {0}", i));

				int maxSize = Math.Min (Constants.MaxSize, fullSolution.Length - (i * Constants.MaxSize));

				FitnessCalc.Solution = fullSolution.Substring (i * Constants.MaxSize, maxSize);
				FitnessCalc.InvalidateCache ();

				//generate the population and start the cycle
				Population myPop = new Population (20, maxSize, true);
				int generationCount = 0;
				int oldFitness = 0;

				while (myPop.Fittest.Fitness < FitnessCalc.MaxFitness) { 
					
					generationCount++; 

					int newFitness = myPop.Fittest.Fitness;

					if (newFitness != oldFitness) {
						Console.Write (".");
						var tempSol = randomImage.Remove (i * Constants.MaxSize, maxSize);
						tempSol = randomImage.Insert (i * Constants.MaxSize, myPop.Fittest.Genes);
						var bitmap = GeneticConverter.ByteStringToBitmap (tempSol);
						bitmap.Save (string.Format ("{0}.bmp", count++));
						oldFitness = newFitness;
					}

					myPop = Algorithm.evolvePopulation (myPop);
				}

				var newImage = randomImage.Remove (i * Constants.MaxSize, maxSize);
				newImage = randomImage.Insert (i * Constants.MaxSize, myPop.Fittest.Genes);
				randomImage = new string (newImage.ToCharArray ());
			}
			Console.WriteLine ();
			Console.WriteLine (Math.Round ((DateTime.Now - start).TotalMinutes));

			Console.ReadLine ();
		}

		static string GenerateRandomImage (int size)
		{
			var chromosome = "";
			var random = new Random ();

			for (int i = 0; i < size; i++) {
				int next = random.Next (0, 2);
				chromosome += next;
			}

			return chromosome;
		}

	}
}
