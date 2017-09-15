using System;
using System.Text;

namespace Genetic
{
	public class Algorithm
	{
		private const double UNIFORM_RATE = 0.5;
		private const double MUTATION_RATE = 0.02;
		private const int TOURNAMENT_SIZE = 5;
		private const bool ELITISM_ACTIVE = true;

		private static Random rand = new Random ();

		public static Population evolvePopulation (Population pop)
		{
			Population newPopulation = new Population (pop.Size, pop.Fittest.Genes.Length, false);

			int elitismOffset = 0;

			if (ELITISM_ACTIVE) {
				newPopulation.saveIndividual (0, pop.Fittest);
				elitismOffset = 1;
			}
				
			for (int i = elitismOffset; i < pop.Size; i++) {
				Individual indiv1 = TournamentSelection (pop);
				Individual indiv2 = TournamentSelection (pop);
				Individual newIndiv = Crossover (indiv1, indiv2);
				newPopulation.saveIndividual (i, newIndiv);
			}

			for (int i = elitismOffset; i < newPopulation.Size; i++) {
				Mutate (newPopulation.GetIndividual (i));
			}

			return newPopulation;
		}

		private static Individual Crossover (Individual indiv1, Individual indiv2)
		{
			int index = rand.Next (0, indiv1.Genes.Length);

			Individual newSol = new Individual ();

			newSol.Genes = indiv1.Genes.Substring (0, index) + indiv2.Genes.Substring (index, indiv2.Genes.Length - index);

			return newSol;
		}

		private static void Mutate (Individual indiv)
		{
			for (int i = 0; i < indiv.Size; i++) {
				if (rand.NextDouble () <= MUTATION_RATE) {
					int next = rand.Next (0, 2);
					var builder = new StringBuilder (indiv.Genes);
					builder [i] = next.ToString ().ToCharArray () [0];
					indiv.Genes = builder.ToString ();
				}
			}
		}

		private static Individual TournamentSelection (Population pop)
		{
			Population tournament = new Population (TOURNAMENT_SIZE, pop.Fittest.Genes.Length, false);

			for (int i = 0; i < TOURNAMENT_SIZE; i++) {
				int randomId = (int)(rand.NextDouble () * pop.Size);
				tournament.saveIndividual (i, pop.GetIndividual (randomId));
			}

			Individual fittest = tournament.Fittest;
			return fittest;
		}
	}
}

