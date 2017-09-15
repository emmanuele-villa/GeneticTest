using System;

namespace Genetic
{
	public class Population
	{
		Individual[] individuals;

		public Population (int populationSize, int geneLenght, bool initialise)
		{
			
			individuals = new Individual[populationSize];

			if (initialise) {
				for (int i = 0; i < Size; i++) {
					Individual newIndividual = new Individual ();
					newIndividual.GenerateIndividual (geneLenght);
					saveIndividual (i, newIndividual);
				}
			}
		}

		public Individual GetIndividual (int index)
		{
			return individuals [index];
		}

		public Individual Fittest {
			get {
				Individual fittest = individuals [0];

				for (int i = 0; i < Size; i++) {
					if (fittest.Fitness <= GetIndividual (i).Fitness) {
						fittest = GetIndividual (i);
					}
				}
				return fittest;
			}
		}

		public int Size { 
			get { 
				return individuals.Length; 
			} 
		}

		public void saveIndividual (int index, Individual indiv)
		{
			individuals [index] = indiv;
		}
	}
}

