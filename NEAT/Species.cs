using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAT
{
    public class Species
    {
        private double fitness = 0;
        private double topfitness = 0;
        private double staleness = 0;
        private List<Genome> genomes = new List<Genome>();     
        private double averagefitness = 0;
        private double Sumofadjustedfitnesses = 0;
        private double AverageAdjustedFitness = 0;
        public double GetFitness()
        {
            return fitness;
        }
        public void SetFitness(double n)
        {
            fitness = n;
        }
        public double GetTopfitness()
        {
            return topfitness;
        }
        public void SetTopfitness(double n)
        {
            topfitness = n;
        }
        public double GetStaleness()
        {
            return staleness;
        }
        public void SetStaleness(double n)
        {
            staleness = n;
        }
        public List<Genome> GetGenomes()
        {
            return genomes;
        }
        public void SetGenomes(Genome n)
        {
            genomes.Add(n);
        }
        public void ClearGenomes()
        {
            genomes.Clear();
        }
        public double GetAveragefitness()
        {
            return averagefitness;
        }
        public void SetAveragefitness(double n)
        {
            averagefitness = n;
        }
        public double GetAverageAdjustedfitness()
        {
            return AverageAdjustedFitness;
        }
        public void SetAverageAdjustedfitness(double n)
        {
            AverageAdjustedFitness = n;
        }
        public double GetSumofadjustedfitnesses()
        {
            return Sumofadjustedfitnesses;
        }
        public void SetSumofadjustedfitnesses(double n)
        {
            Sumofadjustedfitnesses = n;
        }
     
        private Genome RandomGenome;
        public Genome GetPreviousRandomGenome()
        {
            return RandomGenome;
        }
        public void SetPreviousRandomGenome(Genome n)
        {          
            RandomGenome = Main.copyGENOME(n);
        }

        private int SerialNumber = 0;
        public int GetSerialNumber()
        {
            return SerialNumber;
        }
        public void SetSerialNumber(int x)
        {
            SerialNumber = x;
        }

        private bool AllowedtoReproduce = true;
        public bool IsAllowedtoReproduce()
        {
            return AllowedtoReproduce;
        }
        public void SetAllowedtoReproduce(bool n)
        {
            AllowedtoReproduce = n;
        }

        public double NumberOfSpawns;
        public double GetNumberOfSpawns()
        {
            return NumberOfSpawns;
        }
        public void SetNumberOfSpawns(double n)
        {
            NumberOfSpawns = n;
        }

        public int Age;
        public int GetAge()
        {
            return Age;
        }
        public void SetAge(int n)
        {
            Age = n;
        }
    }
}
