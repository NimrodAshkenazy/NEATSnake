using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAT
{
    public class Pool
    {
        private List<Species> species = new List<Species>();
        private double generation = 0;
        private double maxfitness = -100000000000;
        private int innovation = 0;
        public List<Species> GetSpecies()
        {
            return species;
        }
        public void SetSpecies(Species n)
        {
            species.Add(n);
        }
        public void ClearSpecies()
        {
            species.Clear();
        }
        public double GetGeneration()
        {
            return generation;
        }
        public void SetGeneration(double n)
        {
            generation = n;
        }
        public double GetMaxfitness()
        {
            return maxfitness;
        }
        public void SetMaxfitness(double n)
        {
            maxfitness = n;
        }
        public int GetInnovation()
        {
            return innovation;
        }
        public void SetInnovation(int n)
        {
            innovation=n;
        }
    }
}
