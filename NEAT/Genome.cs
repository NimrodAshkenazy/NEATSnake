using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAT
{
    public class Genome
    {
        private List<Gene> genes = new List<Gene>();
        private List<Node> network = new List<Node>();
        private double fitness = 0;
        private double adjustedfitness = 0;     
        private double[] mutationrates = new double[7] { GlobalData.MutateConnectionsChance, GlobalData.LinkMutationChance,GlobalData.BiasMutationChance, GlobalData.NodeMutationChance, GlobalData.EnableMutationChance, GlobalData.DisableMutationChance,GlobalData.StepSize };
        //number references : connection(0),link(1),bias(2),node(3),enable(4),disable(5),step(6).//
        public double[] GetMutationrates()
        {
            return mutationrates;
        }
        public void SetMutationrate(double n, int m)
        {
            mutationrates[m] = n;
        }
        public List<Gene> GetGenes()
        {
            return genes;
        }
        public void SetGenes(Gene n)
        {
            genes.Add(n);
        }
        public void ClearGenes()
        {
            genes.Clear();
        }
        public double GetFitness()
        {
            return fitness;
        }
        public void SetFitness(double n)
        {
            fitness = n;
        }
        public double GetAdjustedfitness()
        {
            return adjustedfitness;
        }
        public void SetAdjustedfitness(double n)
        {
            adjustedfitness = n;
        }
        public List<Node> GetNodes()
        {
            return network;
        }
        public void SetNetwork(Node n)
        {
            network.Add(n);
        }
        public void ClearNetwork()
        {
            network.Clear();
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
    }
}
