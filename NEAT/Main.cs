using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NEAT
{
    public class Main
    {
        public static void SaveNET(string path, Pool pool)
        {
            string path1 = path;
            int number = 1;
            start:
            if (!File.Exists(path1))
            {
                File.Create(path1).Dispose();
                using (TextWriter tw = new StreamWriter(path1))
                {
                    string s = "";
                    s = s + "Pool" + "-";
                    s = s + pool.GetGeneration().ToString() + "/";
                    s = s + pool.GetMaxfitness().ToString() + "/";
                    s = s + pool.GetInnovation().ToString() + "/";
                    s = s + " (";
                    int count1 = 0;
                    foreach (Species species in pool.GetSpecies())
                    {
                        s = s + "\n";
                        s = s + "Species" + count1.ToString() + "-";
                        count1++;
                        s = s + species.GetTopfitness().ToString() + "/";
                        s = s + species.GetStaleness().ToString() + "/";
                        s = s + species.GetAveragefitness().ToString() + "/";
                        s = s + species.GetSumofadjustedfitnesses().ToString() + "/";
                        s = s + species.GetSerialNumber().ToString() + "/";
                        s = s + species.IsAllowedtoReproduce().ToString() + "/";
                        s = s + " (";
                        int count2 = 0;
                        foreach (Genome genome in species.GetGenomes())
                        {
                            s = s + "\n";
                            s = s + "Genome" + count2.ToString() + "-";
                            count2++;
                            s = s + genome.GetFitness().ToString() + "/";
                            s = s + genome.GetAdjustedfitness().ToString() + "/";
                            for (int i = 0; i < genome.GetMutationrates().Length; i++)
                            {
                                s = s + genome.GetMutationrates()[i].ToString() + "/";
                            }
                            s = s + " (";
                            int count3 = 0;
                            foreach (Gene gene in genome.GetGenes())
                            {
                                s = s + "Gene" + count3.ToString() + "-";
                                count3++;
                                s = s + gene.GetWeight().ToString() + "/";
                                s = s + gene.GetInto().ToString() + "/";
                                s = s + gene.GetOuto().ToString() + "/";
                                s = s + gene.GetInnovation().ToString() + "/";
                                s = s + gene.IsEnabled().ToString() + "/ ";
                            }
                            int count4 = 0;
                            foreach (Node node in genome.GetNodes())
                            {
                                s = s + "Node" + count4.ToString() + "-";
                                count4++;
                                s = s + node.GetValue().ToString() + "/";
                                s = s + node.GetSerialNumber().ToString() + "/";
                                s = s + node.GetType() + "/";
                                s = s + node.GetFunction() + "/ ";
                            }
                            s = s + ")";
                        }
                        s = s + "\n";
                        s = s + ")";
                    }
                    s = s + "\n";
                    s = s + ")";
                    tw.WriteLine(s);
                    tw.Close();
                }
            }
            else
            {
                Console.WriteLine("This destination already contains a file with the same name. ");
                Console.WriteLine("Do you want to rename " + path + " to " + path.Substring(0, path.Length - 4) + "(" + number.ToString() + ").txt ?");
                // if (Console.ReadLine().ToLower() == "yes")
                // {
                path1 = path.Substring(0, path.Length - 4) + "(" + number.ToString() + ").txt";
                number++;
                goto start;
                //  }
            }
        }
        public static Pool LoadNET(string path)
        {
            Pool pool = new Pool();
            if (File.Exists(path))
            {
                StreamReader sr = File.OpenText(path);
                string s = sr.ReadToEnd().ToLower();
                string temp = "";
                string temp2 = "";
                string SpeciesNum = "";
                string GenomeNum = "";
                string GeneNum = "";
                string NodeNum = "";
                int counter = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s.Length > i + 4 && s[i] == 'p' && s.Substring(i, 4) == "pool")
                    {
                        i = i + 4;
                        while (s[i] != '-') i++;
                        while (s[i] != ' ')
                        {
                            i++;
                            temp = temp + s[i];
                        }
                        for (int m = 0; m < temp.Length; m++)
                        {
                            if (temp[m] == '/')
                            {
                                if (counter == 0) pool.SetGeneration(double.Parse(temp2));
                                if (counter == 1) pool.SetMaxfitness(double.Parse(temp2));
                                if (counter == 2) pool.SetInnovation(int.Parse(temp2));
                                counter++;
                                temp2 = "";
                            }
                            if (temp[m] != '/')
                            {
                                temp2 += temp[m];
                            }
                        }
                        temp = "";
                        temp2 = "";
                        counter = 0;

                    }
                    if (s.Length > i + 7 && s[i] == 's' && s.Substring(i, 7) == "species")
                    {
                        i = i + 7;
                        pool.SetSpecies(new Species());
                        SpeciesNum = "";
                        while (s[i] != '-')
                        {
                            SpeciesNum = SpeciesNum + s[i];
                            i++;
                        }
                        while (s[i] != ' ')
                        {
                            i++;
                            temp = temp + s[i];
                        }
                        for (int m = 0; m < temp.Length; m++)
                        {
                            if (temp[m] == '/')
                            {
                                if (counter == 0) pool.GetSpecies()[int.Parse(SpeciesNum)].SetTopfitness(double.Parse(temp2));
                                if (counter == 1) pool.GetSpecies()[int.Parse(SpeciesNum)].SetStaleness(int.Parse(temp2));
                                if (counter == 2) pool.GetSpecies()[int.Parse(SpeciesNum)].SetAveragefitness(double.Parse(temp2));
                                if (counter == 3) pool.GetSpecies()[int.Parse(SpeciesNum)].SetSumofadjustedfitnesses(double.Parse(temp2));
                                if (counter == 4) pool.GetSpecies()[int.Parse(SpeciesNum)].SetSerialNumber(int.Parse(temp2));
                                if (counter == 5) pool.GetSpecies()[int.Parse(SpeciesNum)].SetAllowedtoReproduce(bool.Parse(temp2));
                                counter++;
                                temp2 = "";
                            }
                            if (temp[m] != '/')
                            {
                                temp2 += temp[m];
                            }
                        }
                        temp = "";
                        temp2 = "";
                        counter = 0;
                    }
                    if (s.Length > i + 6 && s[i] == 'g' && s.Substring(i, 6) == "genome")
                    {
                        i = i + 6;
                        GenomeNum = "";
                        pool.GetSpecies()[int.Parse(SpeciesNum)].SetGenomes(new Genome());
                        while (s[i] != '-')
                        {
                            GenomeNum = GenomeNum + s[i];
                            i++;
                        }
                        while (s[i] != ' ')
                        {
                            i++;
                            temp = temp + s[i];
                        }
                        for (int m = 0; m < temp.Length; m++)
                        {
                            if (temp[m] == '/')
                            {
                                if (counter == 0) pool.GetSpecies()[int.Parse(SpeciesNum)].GetGenomes()[int.Parse(GenomeNum)].SetFitness(double.Parse(temp2));
                                if (counter == 1) pool.GetSpecies()[int.Parse(SpeciesNum)].GetGenomes()[int.Parse(GenomeNum)].SetAdjustedfitness(double.Parse(temp2));
                                if (counter > 1 && counter < 9)
                                {
                                    pool.GetSpecies()[int.Parse(SpeciesNum)].GetGenomes()[int.Parse(GenomeNum)].SetMutationrate(double.Parse(temp2), counter - 2);
                                }
                                counter++;
                                temp2 = "";
                            }
                            if (temp[m] != '/')
                            {
                                temp2 += temp[m];
                            }
                        }
                        temp = "";
                        temp2 = "";
                        counter = 0;
                    }
                    if (s.Length > i + 4 && s[i] == 'g' && s.Substring(i, 4) == "gene")
                    {
                        GeneNum = "";
                        i = i + 4;
                        pool.GetSpecies()[int.Parse(SpeciesNum)].GetGenomes()[int.Parse(GenomeNum)].SetGenes(new Gene());
                        while (s[i] != '-')
                        {
                            GeneNum = GeneNum + s[i];
                            i++;
                        }
                        while (s[i] != ' ')
                        {
                            i++;
                            temp = temp + s[i];
                        }
                        for (int m = 0; m < temp.Length; m++)
                        {
                            if (temp[m] == '/')
                            {
                                if (counter == 0) pool.GetSpecies()[int.Parse(SpeciesNum)].GetGenomes()[int.Parse(GenomeNum)].GetGenes()[int.Parse(GeneNum)].SetWeight(double.Parse(temp2));
                                if (counter == 1) pool.GetSpecies()[int.Parse(SpeciesNum)].GetGenomes()[int.Parse(GenomeNum)].GetGenes()[int.Parse(GeneNum)].SetInto(int.Parse(temp2));
                                if (counter == 2) pool.GetSpecies()[int.Parse(SpeciesNum)].GetGenomes()[int.Parse(GenomeNum)].GetGenes()[int.Parse(GeneNum)].SetOuto(int.Parse(temp2));
                                if (counter == 3) pool.GetSpecies()[int.Parse(SpeciesNum)].GetGenomes()[int.Parse(GenomeNum)].GetGenes()[int.Parse(GeneNum)].SetInnovation(int.Parse(temp2));
                                if (counter == 4) pool.GetSpecies()[int.Parse(SpeciesNum)].GetGenomes()[int.Parse(GenomeNum)].GetGenes()[int.Parse(GeneNum)].SetEnabled(bool.Parse(temp2));
                                counter++;
                                temp2 = "";
                            }
                            if (temp[m] != '/')
                            {
                                temp2 += temp[m];
                            }
                        }
                        temp = "";
                        temp2 = "";
                        counter = 0;
                    }
                    if (s.Length > i + 4 && s[i] == 'n' && s.Substring(i, 4) == "node")
                    {
                        i = i + 4;
                        NodeNum = "";
                        pool.GetSpecies()[int.Parse(SpeciesNum)].GetGenomes()[int.Parse(GenomeNum)].SetNetwork(new Node());
                        while (s[i] != '-')
                        {
                            NodeNum = NodeNum + s[i];
                            i++;
                        }
                        while (s[i] != ' ')
                        {
                            i++;
                            temp = temp + s[i];
                        }
                        for (int m = 0; m < temp.Length; m++)
                        {
                            if (temp[m] == '/')
                            {
                                if (counter == 0) pool.GetSpecies()[int.Parse(SpeciesNum)].GetGenomes()[int.Parse(GenomeNum)].GetNodes()[int.Parse(NodeNum)].SetValue(double.Parse(temp2));
                                if (counter == 1) pool.GetSpecies()[int.Parse(SpeciesNum)].GetGenomes()[int.Parse(GenomeNum)].GetNodes()[int.Parse(NodeNum)].SetSerialNumber(int.Parse(temp2));
                                if (counter == 2) pool.GetSpecies()[int.Parse(SpeciesNum)].GetGenomes()[int.Parse(GenomeNum)].GetNodes()[int.Parse(NodeNum)].SetType(temp2);
                                if (counter == 3) pool.GetSpecies()[int.Parse(SpeciesNum)].GetGenomes()[int.Parse(GenomeNum)].GetNodes()[int.Parse(NodeNum)].SetFunction(temp2);
                                counter++;
                                temp2 = "";
                            }
                            if (temp[m] != '/')
                            {
                                temp2 += temp[m];
                            }
                        }
                        temp = "";
                        temp2 = "";
                        counter = 0;
                    }
                }
            }
            return pool;
        }

        static Species copySPECIES(Species species)
        {
            Species species1 = new Species();
            foreach (Genome genome in species.GetGenomes()) species1.SetGenomes(copyGENOME(genome));
            species1.SetAveragefitness(species.GetAveragefitness());
            if (species.GetPreviousRandomGenome() != null) species1.SetPreviousRandomGenome(copyGENOME(species.GetPreviousRandomGenome()));
            species1.SetSerialNumber(species.GetSerialNumber());
            species1.SetTopfitness(species.GetTopfitness());
            species1.SetSumofadjustedfitnesses(species.GetSumofadjustedfitnesses());
            species1.SetStaleness(species.GetStaleness());
            return species1;
        }
        public static Genome copyGENOME(Genome genome)
        {
            Genome genome1 = new Genome();
            foreach (Gene gene in genome.GetGenes()) genome1.SetGenes(copyGENE(gene));
            foreach (Node node in genome.GetNodes()) genome1.SetNetwork(copyNODE(node));
            genome1.SetFitness(genome.GetFitness());
            genome1.SetAdjustedfitness(genome.GetAdjustedfitness());
            for (int i = 0; i < genome.GetMutationrates().Length; i++)
            {
                genome1.SetMutationrate(genome.GetMutationrates()[i], i);
            }

            return genome1;
        }
        static Gene copyGENE(Gene gene)
        {
            Gene gene1 = new Gene();
            gene1.SetInto(gene.GetInto());
            gene1.SetOuto(gene.GetOuto());
            gene1.SetWeight(gene.GetWeight());
            gene1.SetEnabled(gene.IsEnabled());
            gene1.SetInnovation(gene.GetInnovation());
            gene1.SetX1(gene.GetX1());
            gene1.SetY1(gene.GetY1());
            gene1.SetX2(gene.GetX2());
            gene1.SetY2(gene.GetY2());
            return gene1;
        }
        static Node copyNODE(Node node)
        {
            Node node1 = new Node();
            node1.SetValue(node.GetValue());
            node1.SetSerialNumber(node.GetSerialNumber());
            node1.SetType(node.GetType());
            node1.SetFunction(node.GetFunction());
            node1.SetX(node.GetX());
            node1.SetY(node.GetY());
            return node1;
        }

        static int NewInnovation()
        {
            GlobalData.Innovation = GlobalData.Innovation + 1;
            return GlobalData.Innovation;
        }
        static int NewSpecies()
        {
            GlobalData.speciesSerialNum = GlobalData.speciesSerialNum + 1;
            return GlobalData.speciesSerialNum;
        }

        static double Randomnumber(Random random, double minimum, double maximum)
        {
            return random.NextDouble() * (minimum - maximum) + maximum;
        }

        public static void CleanNodesValue(Genome genome)
        {
            foreach (Node node in genome.GetNodes())
            {
                if (node.GetType().ToLower() != "bias") node.SetValue(0);
            }
        }
        public static void StepNetwork(Genome genome, int stepnum)
        {
            double[] Sa = new double[genome.GetNodes().Count()];
            for (int step = 0; step < stepnum; step++)
            {
                for (int i = 0; i < genome.GetNodes().Count(); i++)
                {
                    double S = 0;
                    for (int m = 0; m < genome.GetGenes().Count(); m++)
                    {
                        if (genome.GetGenes()[m].GetOuto() == i && genome.GetGenes()[m].IsEnabled())
                        {

                            {
                                S = S + genome.GetNodes()[genome.GetGenes()[m].GetInto()].GetValue() * genome.GetGenes()[m].GetWeight();
                            }

                        }
                    }
                    Sa[i] = S;
                }
                for (int i1 = 0; i1 < genome.GetNodes().Count(); i1++)
                {
                    if (genome.GetNodes()[i1].GetType().ToLower() == "hidden" || genome.GetNodes()[i1].GetType().ToLower() == "output")
                    {
                        if (genome.GetNodes()[i1].GetFunction().ToLower() == "sigmoid") genome.GetNodes()[i1].SetValue(SigmoidFunction(Sa[i1]));
                        else if (genome.GetNodes()[i1].GetFunction().ToLower() == "gaussian") genome.GetNodes()[i1].SetValue(GaussianFunction(Sa[i1]));
                        else if (genome.GetNodes()[i1].GetFunction().ToLower() == "sin") genome.GetNodes()[i1].SetValue(SinFunction(Sa[i1]));
                        else if (genome.GetNodes()[i1].GetFunction().ToLower() == "mod") genome.GetNodes()[i1].SetValue(ModuloFunction(Sa[i1]));
                    }
                }
            }
        }

        static double SigmoidFunction(double x)
        {
            return 2 / (1 + Math.Exp(-4.9 * x)) - 1;
        }
        static double GaussianFunction(double x)
        {
            double a = 1;
            double b = 0;
            double c = 0.25;
            return a * Math.Pow(Math.E, -(x - b) * (x - b) / (2 * c * c));
        }
        static double SinFunction(double x)
        {
            return Math.Sin(Math.PI * x);
        }
        static double ModuloFunction(double x)
        {
            return x % 1;
        }
        static string RandomFunction(Random random)
        {
            List<string> function = new List<string>();
            function.Add("Sigmoid");
            //function.Add("Gaussian");
            //function.Add("Sin");
            //function.Add("Mod");
            return function[random.Next(0, function.Count())];
        }

        static void MutateGenome(Genome genome, Random random)
        {
            //    Random random = new Random();
            for (int i = 0; i < genome.GetMutationrates().Length; i++)
            {
                if (random.Next(0, 2) == 1)
                {
                    genome.SetMutationrate(genome.GetMutationrates()[i] * 0.95, i);
                }
                else
                {
                    genome.SetMutationrate(genome.GetMutationrates()[i] * 1.05263, i);
                }
            }
            //number references : connection(0),link(1),bias(2),node(3),enable(4),disable(5),step(6).//     
            double temp = genome.GetMutationrates()[0];
            if (temp > random.NextDouble())
            {
                PointMutate(genome, random);
            }
            temp = genome.GetMutationrates()[1];
            while (temp > 0)
            {
                if (temp > random.NextDouble())
                {
                    LinkMutate(genome, false, random);
                }
                temp--;
            }
            temp = genome.GetMutationrates()[2];
            while (temp > 0)
            {
                if (temp > random.NextDouble())
                {
                    LinkMutate(genome, true, random);
                }
                temp--;
            }
            temp = genome.GetMutationrates()[3];
            while (temp > 0)
            {
                if (temp > random.NextDouble())
                {
                    NodeMutate(genome, random);
                }
                temp--;
            }
            temp = genome.GetMutationrates()[4];
            while (temp > 0)
            {
                if (temp > random.NextDouble())
                {
                    EnableDisableMutate(genome, true, random);
                }
                temp--;
            }
            temp = genome.GetMutationrates()[5];
            while (temp > 0)
            {
                if (temp > random.NextDouble())
                {
                    EnableDisableMutate(genome, false, random);
                }
                temp--;
            }
        }
        static void PointMutate(Genome genome, Random random)
        {
            //    Random random = new Random();
            double StepSize = genome.GetMutationrates()[6];
            foreach (Gene gene in genome.GetGenes())
            {
                if (random.NextDouble() < GlobalData.PerturbChance)
                {
                    gene.SetWeight(gene.GetWeight() + Randomnumber(random, -StepSize, StepSize));
                    if (gene.GetWeight() > GlobalData.MutationPower) gene.SetWeight(GlobalData.MutationPower);
                    if (gene.GetWeight() < -GlobalData.MutationPower) gene.SetWeight(-GlobalData.MutationPower);
                }
                else gene.SetWeight(Randomnumber(random, -2, 2));
            }
        }
        static void LinkMutate(Genome genome, bool ForceBias, Random random)
        {
            //  Random random = new Random();
            List<int[]> possiblyconnections = new List<int[]>();
            Gene gene = new Gene();
            gene.SetInnovation(NewInnovation());

            for (int into = 0; into < genome.GetNodes().Count(); into++)
            {
                for (int outo = 0; outo < genome.GetNodes().Count(); outo++)
                {
                    int[] r = new int[2];
                    r[0] = into;
                    r[1] = outo;
                    possiblyconnections.Add(r);

                }
            }
            foreach (Gene gene1 in genome.GetGenes())
            {
                for (int i = 0; i < possiblyconnections.Count(); i++)
                {
                    if (possiblyconnections[i] != null)
                    {
                        if (gene1.GetInto() == possiblyconnections[i][0] && gene1.GetOuto() == possiblyconnections[i][1]) possiblyconnections[i] = null;
                    }
                }
            }
            foreach (Node node in genome.GetNodes())
            {
                for (int i = 0; i < possiblyconnections.Count(); i++)
                {
                    if (possiblyconnections[i] != null)
                    {
                        if (node.GetType().ToLower() == "input" && possiblyconnections[i][1] == node.GetSerialNumber()) possiblyconnections[i] = null;
                        else if (node.GetType().ToLower() == "output" && possiblyconnections[i][0] == node.GetSerialNumber()) possiblyconnections[i] = null;
                        else if (node.GetType().ToLower() == "bias" && possiblyconnections[i][1] == node.GetSerialNumber()) possiblyconnections[i] = null;
                    }
                }
            }
            int rIn = 0;
            int rOut = 0;
            possiblyconnections.RemoveAll(item => item == null);
            if (possiblyconnections.Count() > 0)
            {
                int link = random.Next(0, possiblyconnections.Count());
                rIn = possiblyconnections[link][0];
                rOut = possiblyconnections[link][1];
            }
            else return;
            gene.SetInto(rIn);
            gene.SetOuto(rOut);
            if (ForceBias)
            {
                if (!genome.GetNodes().Any(e => e.GetType().ToLower() == "bias"))
                {
                    Node bias = new Node();
                    bias.SetSerialNumber(genome.GetNodes().Count());
                    bias.SetType("Bias");
                    bias.SetValue(1);
                    gene.SetInto(genome.GetNodes().Count());
                    genome.SetNetwork(bias);
                }
                else
                {
                    int nodenum = genome.GetNodes().Find(e => e.GetType().ToLower() == "bias").GetSerialNumber();
                    gene.SetInto(nodenum);
                }
            }
            gene.SetWeight(Randomnumber(random, -2, 2));
            genome.SetGenes(gene);
        }
        static void NodeMutate(Genome genome, Random random)
        {
            // Random random = new Random();
            List<Gene> genelist = new List<Gene>();
            foreach (Gene gene in genome.GetGenes())
            {
                if (gene.IsEnabled())
                {
                    genelist.Add(gene);
                }
            }
            if (genelist.Count() > 0)
            {
                Gene gene = genelist[random.Next(0, genelist.Count())];
                gene.SetEnabled(false);
                Gene gene1 = copyGENE(gene);
                gene1.SetInnovation(NewInnovation());
                gene1.SetWeight(1);
                gene1.SetEnabled(true);
                gene1.SetOuto(genome.GetNodes().Count());
                Gene gene2 = copyGENE(gene);
                gene2.SetInnovation(NewInnovation());
                gene2.SetEnabled(true);
                gene2.SetInto(genome.GetNodes().Count());

                Node node = new Node();
                node.SetSerialNumber(genome.GetNodes().Count());
                node.SetType("Hidden");
                node.SetFunction(RandomFunction(random));
                genome.SetNetwork(node);
                genome.SetGenes(gene1);
                genome.SetGenes(gene2);
            }
        }
        static void EnableDisableMutate(Genome genome, bool Enable, Random random)
        {
            //       Random random = new Random();
            List<Gene> genelist = new List<Gene>();
            foreach (Gene gene in genome.GetGenes())
            {
                if (gene.IsEnabled() != Enable)
                {
                    genelist.Add(gene);
                }
            }
            if (genelist.Count != 0)
            {
                genelist[random.Next(0, genelist.Count())].SetEnabled(Enable);
            }
        }

        static Genome RandomGenome(Random random)
        {
            Genome genome = new Genome();
            for (int i = 0; i < GlobalData.InputsNum; i++)
            {
                Node node = new Node();
                node.SetSerialNumber(i);
                node.SetType("Input");
                node.SetFunction(RandomFunction(random));
                genome.SetNetwork(copyNODE(node));
            }
            for (int i = 0; i < GlobalData.OutputsNum; i++)
            {
                Node node = new Node();
                node.SetType("Output");
                node.SetFunction(RandomFunction(random));
                node.SetSerialNumber(i + GlobalData.InputsNum);
                genome.SetNetwork(copyNODE(node));
            }
            for (int i = 0; i < GlobalData.HiddenNodes; i++)
            {
                Node node = new Node();
                node.SetType("Hidden");
                node.SetFunction(RandomFunction(random));
                node.SetSerialNumber(i + GlobalData.InputsNum + GlobalData.OutputsNum);
                genome.SetNetwork(copyNODE(node));
            }
            int link = GlobalData.averageStatingGenes + random.Next(-2, 3);
            for (int i = 0; i < link; i++)
            {
                LinkMutate(genome, false, random);
            }
            return genome;
        }

        public static Genome Crossover(Genome Parent1, Genome Parent2)
        {
            Random random = new Random();
            Genome offspring = new Genome();
            Parent1.GetGenes().Sort((x, y) => x.GetInnovation().CompareTo(y.GetInnovation()));
            Parent2.GetGenes().Sort((x, y) => x.GetInnovation().CompareTo(y.GetInnovation()));
            int best;
            if (Parent1.GetFitness() > Parent2.GetFitness()) { best = 0; goto bestend; }
            if (Parent1.GetFitness() < Parent2.GetFitness()) { best = 1; goto bestend; }

            if (Parent1.GetGenes().Count() > Parent2.GetGenes().Count()) best = 0;
            else if (Parent1.GetGenes().Count() < Parent2.GetGenes().Count()) best = 1;
            else best = random.Next(0, 2);

            bestend:
            int genenum1 = 0;
            int genenum2 = 0;
            while (genenum1 < Parent1.GetGenes().Count() && genenum2 < Parent2.GetGenes().Count())
            {
                if (Parent1.GetGenes()[genenum1].GetInnovation() == Parent2.GetGenes()[genenum2].GetInnovation())
                {
                    if (random.Next(0, 2) == 1) offspring.SetGenes(Parent1.GetGenes()[genenum1]);
                    else offspring.SetGenes(Parent2.GetGenes()[genenum2]);

                    genenum1++;
                    genenum2++;
                }
                else if (Parent1.GetGenes()[genenum1].GetInnovation() < Parent2.GetGenes()[genenum2].GetInnovation())
                {
                    if (best == 0) offspring.SetGenes(Parent1.GetGenes()[genenum1]);
                    genenum1++;
                }
                else if (Parent1.GetGenes()[genenum1].GetInnovation() > Parent2.GetGenes()[genenum2].GetInnovation())
                {
                    if (best == 1) offspring.SetGenes(Parent2.GetGenes()[genenum2]);
                    genenum2++;
                }
            }
            if (genenum1 >= Parent1.GetGenes().Count() && genenum2 >= Parent2.GetGenes().Count()) goto End;
            else if (best == 0)
            {
                while (genenum1 < Parent1.GetGenes().Count())
                {
                    offspring.SetGenes(Parent1.GetGenes()[genenum1]);
                    genenum1++;
                }
            }
            else if (best == 1)
            {
                while (genenum2 < Parent2.GetGenes().Count())
                {
                    offspring.SetGenes(Parent2.GetGenes()[genenum2]);
                    genenum2++;
                }
            }
            End:
            int nodenum1 = 0;
            int nodenum2 = 0;
            while (nodenum1 < Parent1.GetNodes().Count() && nodenum2 < Parent2.GetNodes().Count())
            {
                if (Parent1.GetNodes()[nodenum1].GetSerialNumber() == Parent2.GetNodes()[nodenum2].GetSerialNumber())
                {
                    if (best == 0)
                    {
                        offspring.SetNetwork(Parent1.GetNodes()[nodenum1]);
                    }
                    else if (best == 1)
                    {
                        offspring.SetNetwork(Parent2.GetNodes()[nodenum2]);
                    }
                    nodenum1++;
                    nodenum2++;
                }
                else if (Parent1.GetNodes()[nodenum1].GetSerialNumber() > Parent2.GetNodes()[nodenum2].GetSerialNumber())
                {
                    offspring.SetNetwork(Parent2.GetNodes()[nodenum2]);
                    nodenum2++;
                }
                else if (Parent1.GetNodes()[nodenum1].GetSerialNumber() < Parent2.GetNodes()[nodenum2].GetSerialNumber())
                {
                    offspring.SetNetwork(Parent1.GetNodes()[nodenum1]);
                    nodenum1++;
                }
            }
            while (nodenum1 < Parent1.GetNodes().Count())
            {
                offspring.SetNetwork(Parent1.GetNodes()[nodenum1]);
                nodenum1++;
            }
            while (nodenum2 < Parent2.GetNodes().Count())
            {
                offspring.SetNetwork(Parent2.GetNodes()[nodenum2]);
                nodenum2++;
            }
            //RemoveNodesAndGenes(offspring);
            return offspring;
        }

        static void CulltheGenomes(Species species)
        {
            species.GetGenomes().Sort((x, y) => x.GetFitness().CompareTo(y.GetFitness()));
            species.GetGenomes().RemoveRange(0, species.GetGenomes().Count() / 2);
        }
        static void CulltheGenomesAdjustedfitness(Species species)
        {
            species.GetGenomes().Sort((x, y) => x.GetAdjustedfitness().CompareTo(y.GetAdjustedfitness()));
            species.GetGenomes().RemoveRange(0, species.GetGenomes().Count() / 2);
        }

        static void AssignGenomesToSpecies(List<Genome> genomeLIST, List<Species> speciesList)
        {
            if (genomeLIST.Count() == 0) return;
            int i = 0;
            if (!(speciesList.Count > 0))
            {
                Species spec = new Species();
                spec.SetSerialNumber(NewSpecies());
                speciesList.Add(spec);
                speciesList[speciesList.Count() - 1].SetGenomes(genomeLIST[0]);
                genomeLIST.Remove(genomeLIST[0]);
            }
            while (genomeLIST.Count() > 0)
            {
                for (int m = 0; m < genomeLIST.Count(); m++)
                {
                    if (genomeLIST[m] != null)
                    {
                        if (speciesList[i].GetPreviousRandomGenome() != null)
                        {
                            if (sh(speciesList[i].GetPreviousRandomGenome(), genomeLIST[m]) == 1)
                            {
                                speciesList[i].SetGenomes(genomeLIST[m]);
                                genomeLIST[m] = null;
                            }
                        }
                        else
                        {
                            if (sh(speciesList[i].GetGenomes()[0], genomeLIST[m]) == 1)
                            {
                                speciesList[i].SetGenomes(genomeLIST[m]);
                                genomeLIST[m] = null;
                            }
                        }
                    }
                }
                genomeLIST.RemoveAll(item => item == null);
                i++;
                if (speciesList.Count() < GlobalData.MaxSpeicesNum)
                {
                    if (i >= speciesList.Count() && genomeLIST.Count() > 0)
                    {
                        Species spec1 = new Species();
                        spec1.SetSerialNumber(NewSpecies());
                        speciesList.Add(spec1);
                        speciesList[speciesList.Count() - 1].SetGenomes(genomeLIST[0]);
                        genomeLIST.Remove(genomeLIST[0]);
                    }
                }
                else
                {
                    for (int m = 0; m < genomeLIST.Count(); m++)
                    {
                        int? place = null;
                        double Min = 100000000;
                        for (int i1 = 0; i1 < speciesList.Count(); i1++)
                        {
                            if (speciesList[i1].GetPreviousRandomGenome() != null)
                            {
                                double C = CompatibilityDistance(speciesList[i1].GetPreviousRandomGenome(), genomeLIST[m]);
                                if (C < Min)
                                {
                                    Min = C;
                                    place = i1;
                                }
                            }
                            else
                            {
                                double C = CompatibilityDistance(speciesList[i1].GetGenomes()[0], genomeLIST[m]);
                                if (C < Min)
                                {
                                    Min = C;
                                    place = i1;
                                }
                            }
                        }
                        if (place != null)
                        {
                            speciesList[place.Value].SetGenomes(genomeLIST[m]);
                            genomeLIST[m] = null;
                        }
                    }
                    return;
                }
            }
        }

        static void AssignRandomGenome(List<Species> SpeciesList, List<List<Species>> generation)
        {
            if (generation.Count() < 1) return;
            Random random = new Random();
            for (int i = 0; i < generation[generation.Count() - 1].Count(); i++)
            {
                int ran = random.Next(0, generation[generation.Count() - 1][i].GetGenomes().Count());
                if (SpeciesList.Count() > i) SpeciesList[i].SetPreviousRandomGenome(generation[generation.Count() - 1][i].GetGenomes()[ran]);
            }
        }

        static void DropoffSpecies(List<Species> SpeciesList, List<List<Species>> generation)
        {
            if (generation.Count() <= GlobalData.DropoffAge) return;
            SpeciesList.Sort((x, y) => x.GetSerialNumber().CompareTo(y.GetSerialNumber()));
            foreach (List<Species> species in generation)
            {
                species.Sort((x, y) => x.GetSerialNumber().CompareTo(y.GetSerialNumber()));
            }
            int speciesnum1 = 0;
            int speciesnum2 = 0;
            while (speciesnum1 < SpeciesList.Count() && speciesnum2 < generation[generation.Count() - 1 - GlobalData.DropoffAge].Count())
            {
                if (SpeciesList[speciesnum1].GetSerialNumber() == generation[generation.Count() - 1 - GlobalData.DropoffAge][speciesnum2].GetSerialNumber())
                {
                    if (SpeciesList[speciesnum1].GetTopfitness()<= generation[generation.Count() - 1 - GlobalData.DropoffAge][speciesnum2].GetTopfitness())
                    {
                        SpeciesList[speciesnum1].SetAllowedtoReproduce(false);
                    }
                    else SpeciesList[speciesnum1].SetAllowedtoReproduce(true);

                    speciesnum1++;
                    speciesnum2++;
                }
                else if (SpeciesList[speciesnum1].GetSerialNumber() > generation[generation.Count() - 1 - GlobalData.DropoffAge][speciesnum2].GetSerialNumber()) speciesnum2++;
                else speciesnum1++;
                /*if(SpeciesList[speciesnum1].GetSerialNumber() < generation[generation.Count() - 1 - GlobalData.DropoffAge][speciesnum2].GetSerialNumber())*/
            }
        }

        static void PopulateEachSpecies(List<Species> speciesList)
        {
            Random random = new Random();
            List<Genome> genomelist = new List<Genome>();
            int SpawnedSoFar = 0;
            int genomeP = GlobalData.GenomeStatingSize;
            int spawnNUM = genomeP;
            double S = 0;
            foreach (var species in speciesList)
            {
                S += species.GetNumberOfSpawns();
            }
            double S1 = (GlobalData.GenomeStatingSize / S);
            for (int i = 0; i < speciesList.Count(); i++)
            {
                if (!speciesList[i].IsAllowedtoReproduce())
                {
                    speciesList[i] = null;
                    continue;
                }
                if (SpawnedSoFar < spawnNUM)
                {
                    double numberofSpawns = speciesList[i].GetNumberOfSpawns() * S1;

                    while (numberofSpawns > 0)
                    {
                        Genome Child = new Genome();
                        numberofSpawns--;
                        if (speciesList[i].GetGenomes().Count() == 1)
                        {
                            Child = copyGENOME(speciesList[i].GetGenomes()[0]);
                        }
                        else
                        {
                            Genome genome = copyGENOME(Spawn(speciesList[i]));
                            if (GlobalData.CrossoverChance > random.NextDouble())
                            {
                                Genome genome2 = copyGENOME(Spawn(speciesList[i]));
                                int attempNum = 5;
                                while (genome.Equals(genome2) && attempNum > 0)
                                {
                                    genome2 = copyGENOME(Spawn(speciesList[i]));
                                    attempNum--;
                                }
                                if (!genome.Equals(genome2))
                                {
                                    Child = Crossover(genome, genome2);
                                }
                            }
                            else
                            {
                                Child = copyGENOME(genome);
                            }
                        }
                        MutateGenome(Child, random);
                        genomelist.Add(Child);
                        SpawnedSoFar++;
                    }
                }
                speciesList[i].GetGenomes().Sort((x, y) => x.GetFitness().CompareTo(y.GetFitness()));
                if (speciesList[i].GetGenomes().Count() >= 1) speciesList[i].GetGenomes().RemoveRange(0, speciesList[i].GetGenomes().Count() - 1);
            }
            speciesList.RemoveAll(item => item == null);
            AssignGenomesToSpecies(genomelist, speciesList);
        }  //-fix

        static void setSwapnNum(Species species)
        {
            double NumberofSpawns = 0;
            for (int i = 0; i < species.GetGenomes().Count(); i++)
            {
                NumberofSpawns = NumberofSpawns + species.GetGenomes()[i].GetAdjustedfitness() / species.GetAverageAdjustedfitness();
                species.GetGenomes()[i].SetNumberOfSpawns(species.GetGenomes()[i].GetAdjustedfitness() / species.GetAverageAdjustedfitness());
            }
            species.SetNumberOfSpawns(NumberofSpawns);
        }
        static Genome Spawn(Species species)
        {
            double cumulative = 0.0;
            double r = new Random().NextDouble();
            for (int i = 0; i < species.GetGenomes().Count(); i++)
            {
                cumulative += species.GetGenomes()[i].GetNumberOfSpawns() / species.GetNumberOfSpawns();
                if (r <= cumulative)
                {
                    return species.GetGenomes()[i];
                }
            }
            return new Genome();
        }

        static Genome BreedSpecies(Species species, Random random)
        {
            Genome genome = new Genome();

            if (GlobalData.CrossoverChance > random.NextDouble())
            {
                species.GetGenomes().Sort((x, y) => x.GetAdjustedfitness().CompareTo(y.GetAdjustedfitness()));
                genome = Crossover(species.GetGenomes()[random.Next((int)(species.GetGenomes().Count * (100 - GlobalData.SurvivalThreshold * 100) / 100), species.GetGenomes().Count)], species.GetGenomes()[random.Next((int)(species.GetGenomes().Count * (100 - GlobalData.SurvivalThreshold * 100) / 100), species.GetGenomes().Count)]);
            }
            else
            {
                genome = copyGENOME(species.GetGenomes()[random.Next(0, species.GetGenomes().Count())]);
                MutateGenome(genome, random);
            }
            return genome;

        }

        static double CompatibilityDistance(Genome genome, Genome genome1)
        {
            if (genome.GetGenes().Count() == 0 && genome1.GetGenes().Count() == 0) return 0;
            int Excess = 0;
            int Disjoint = 0;
            double W = 0;
            int count = 0;
            int N = 0;
            if (GlobalData.NormalizeCompatibilityDistance)
            {
                if (genome.GetGenes().Count() < genome1.GetGenes().Count()) N = genome1.GetGenes().Count();
                else if (genome.GetGenes().Count() > genome1.GetGenes().Count()) N = genome.GetGenes().Count();
                else N = genome.GetGenes().Count();
            }
            else N = 1;
            genome.GetGenes().Sort((x, y) => x.GetInnovation().CompareTo(y.GetInnovation()));
            genome1.GetGenes().Sort((x, y) => x.GetInnovation().CompareTo(y.GetInnovation()));
            int genenum1 = 0;
            int genenum2 = 0;
            while (genenum1 < genome.GetGenes().Count() && genenum2 < genome1.GetGenes().Count())
            {
                if (genome.GetGenes()[genenum1].GetInnovation() == genome1.GetGenes()[genenum2].GetInnovation())
                {
                    count++;
                    W = W + Math.Abs(genome.GetGenes()[genenum1].GetWeight() - genome1.GetGenes()[genenum2].GetWeight());
                    genenum1++;
                    genenum2++;
                }
                else if (genome.GetGenes()[genenum1].GetInnovation() < genome1.GetGenes()[genenum2].GetInnovation())
                {
                    Excess++;
                    genenum1++;
                }
                else if (genome.GetGenes()[genenum1].GetInnovation() > genome1.GetGenes()[genenum2].GetInnovation())
                {
                    Disjoint++;
                    genenum2++;
                }
            }
            if (genenum1 >= genome.GetGenes().Count() && genenum2 >= genome1.GetGenes().Count()) goto end;
            else if (genenum1 >= genome.GetGenes().Count())
            {
                while (genenum2 < genome1.GetGenes().Count())
                {
                    Disjoint++;
                    genenum2++;
                }
            }
            else
            {
                while (genenum1 < genome.GetGenes().Count())
                {
                    Excess++;
                    genenum1++;
                }
            }
            end:
            if (count == 0) return GlobalData.ExcessCoefficient * Excess / N + GlobalData.DisjointCoefficient * Disjoint / N;
            return GlobalData.ExcessCoefficient * Excess / N + GlobalData.DisjointCoefficient * Disjoint / N + GlobalData.WeightDifferenceCoefficient * W / count;
        }
        static int sh(Genome genome, Genome genome1)
        {
            if (CompatibilityDistance(genome, genome1) < GlobalData.CompatibilityThreshold) return 1;
            else return 0;
        }
        static void adjustedfitnessGenome(Genome genome, Species species)
        {
            int S = 0;
            foreach (Genome genomej in species.GetGenomes())
            {
                S = S + sh(genome, genomej);
            }
            genome.SetAdjustedfitness(genome.GetFitness() / S);
        }

        static void Averagefitness(Species species)
        {
            double S = 0;
            foreach (Genome genome in species.GetGenomes())
            {
                S = S + genome.GetFitness();
            }
            species.SetAveragefitness(S / (species.GetGenomes().Count()));
        }
        static void AverageAdjustedFitness(Species species)
        {
            double S = 0;
            foreach (Genome genome in species.GetGenomes())
            {
                S = S + genome.GetAdjustedfitness();
            }
            species.SetAverageAdjustedfitness(S / (species.GetGenomes().Count()));
        }
        static void SumofAdjustedFitness(Species species)
        {
            double S = 0;
            foreach (Genome genome in species.GetGenomes())
            {
                S = S + genome.GetAdjustedfitness();
            }
            species.SetSumofadjustedfitnesses(S);
        }

        public static void SetInputsValues(Genome genome, List<double> inputs)
        {
            List<Node> Inputs = genome.GetNodes().FindAll(n => n.GetType().ToLower() == "input").ToList();
            if (Inputs.Count() != inputs.Count()) throw new Exception("Number of arguments doesn't equal to the number of inputs");
            Inputs.Sort((x, y) => x.GetSerialNumber().CompareTo(y.GetSerialNumber()));
            for (int i = 0; i < Inputs.Count(); i++)
            {
                Inputs[i].SetValue(inputs[i]);
            }
        }
        public static void SetInputsValues(Genome genome, params double[] inputs)
        {
            List<Node> Inputs = genome.GetNodes().FindAll(n => n.GetType().ToLower() == "input").ToList();
            if (Inputs.Count() != inputs.Length) throw new Exception("Number of arguments doesn't equal to the number of inputs");
            Inputs.Sort((x, y) => x.GetSerialNumber().CompareTo(y.GetSerialNumber()));
            for (int i = 0; i < Inputs.Count(); i++)
            {
                Inputs[i].SetValue(inputs[i]);
            }
        }
        public static List<double> GetOutputsValues(Genome genome)
        {
            List<Node> Outputs = genome.GetNodes().FindAll(n => n.GetType().ToLower() == "output").ToList();
            Outputs.Sort((x, y) => x.GetSerialNumber().CompareTo(y.GetSerialNumber()));
            List<double> outputsValues = new List<double>();
            foreach (var output in Outputs)
            {
                outputsValues.Add(output.GetValue());
            }
            if(outputsValues.Count()!=GlobalData.OutputsNum) throw new Exception("Outputs number doesn't equal the number of outputs");
            return outputsValues;
        }

        public static void EvolveNeat(Pool pool, Func<Genome, double> FitnessFunction, Func<Pool, bool> StopCondition)
        {
            GlobalData.CompatibilityThreshold = GlobalData.CompatibilityThresholdStart;
            List<List<Species>> generation = new List<List<Species>>();
            Random random = new Random();
            if (pool.GetSpecies().Count() == 0) goto start;
            else
            {
                pool.SetMaxfitness(-100000000000);
            }
            goto start1;
            start:
            List<Genome> genomesList = new List<Genome>();
            for (int i = 0; i < GlobalData.GenomeStatingSize; i++)
            {       
                genomesList.Add(copyGENOME(RandomGenome(random)));
            }
            AssignGenomesToSpecies(genomesList, pool.GetSpecies());
            start1:
            while (!StopCondition(pool))
            {
               
                foreach (Species species in pool.GetSpecies())
                {
                    foreach (Genome genome in species.GetGenomes())
                    {
                        genome.SetFitness(FitnessFunction(genome));
                        adjustedfitnessGenome(genome, species);
                        if (species.Age < GlobalData.YouthThreshold) genome.SetAdjustedfitness(genome.GetAdjustedfitness() * GlobalData.YouthBonus);
                        if (species.Age > GlobalData.OldAgeThreshold) genome.SetAdjustedfitness(genome.GetAdjustedfitness() * GlobalData.OldAgePenalty);
                        if (pool.GetMaxfitness() < genome.GetFitness())
                        {
                            species.SetTopfitness(genome.GetFitness());
                            pool.SetMaxfitness(genome.GetFitness());
                        }
                    }
                    Averagefitness(species);
                    AverageAdjustedFitness(species);
                    SumofAdjustedFitness(species);
                    // CulltheGenomes(species);
                    CulltheGenomesAdjustedfitness(species);
                    setSwapnNum(species);
                }
                AssignRandomGenome(pool.GetSpecies(), generation);
                List<Species> SpeciesList = new List<Species>();
                foreach (Species species in pool.GetSpecies())
                {
                    SpeciesList.Add(copySPECIES(species));
                } 
                generation.Add(SpeciesList);
                DropoffSpecies(pool.GetSpecies(), generation);
                PopulateEachSpecies(pool.GetSpecies());
              
                if (!GlobalData.NormalizeCompatibilityDistance) GlobalData.CompatibilityThreshold = GlobalData.CompatibilityThreshold + GlobalData.CompatibilityModifier;
                pool.SetGeneration(pool.GetGeneration() + 1);
                if (pool.GetSpecies().Count() == 0) { pool.SetMaxfitness(-100000000000); /*pool.SetGeneration(0);*/ goto start; }
            }
        }
        public static void stepGeneration(Pool pool, Func<Genome, double> FitnessFunction)
        {
            GlobalData.CompatibilityThreshold = GlobalData.CompatibilityThresholdStart;
            List<List<Species>> generation = new List<List<Species>>();
            Random random = new Random();
            if (pool.GetSpecies().Count() == 0) goto start;
            else
            {
                pool.SetMaxfitness(-10000000);
            }
            goto start1;
            start:
            List<Task> task1 = new List<Task>();
            List<Genome> genomesList = new List<Genome>();
            for(int i = 0; i < GlobalData.GenomeStatingSize; i++)
            {     
               genomesList.Add(copyGENOME(RandomGenome(random)));
            }
            AssignGenomesToSpecies(genomesList, pool.GetSpecies());
            start1:
            foreach (Species species in pool.GetSpecies())
            {
                foreach (Genome genome in species.GetGenomes())
                {
                    genome.SetFitness(FitnessFunction(genome));
                    adjustedfitnessGenome(genome, species);
                    if (species.Age < GlobalData.YouthThreshold) genome.SetAdjustedfitness(genome.GetAdjustedfitness() * GlobalData.YouthBonus);
                    if (species.Age > GlobalData.OldAgeThreshold) genome.SetAdjustedfitness(genome.GetAdjustedfitness() * GlobalData.OldAgePenalty);
                    if (pool.GetMaxfitness() < genome.GetFitness())
                    {
                        species.SetTopfitness(genome.GetFitness());
                        pool.SetMaxfitness(genome.GetFitness());
                    }
                }
                Averagefitness(species);
                AverageAdjustedFitness(species);
                SumofAdjustedFitness(species);

                // CulltheGenomes(species);
                CulltheGenomesAdjustedfitness(species);
                setSwapnNum(species);
            }
            AssignRandomGenome(pool.GetSpecies(), generation);
            List<Species> SpeciesList = new List<Species>();
            foreach (Species species in pool.GetSpecies())
            {
                SpeciesList.Add(copySPECIES(species));
            }
            generation.Add(SpeciesList);
            DropoffSpecies(pool.GetSpecies(), generation);
            PopulateEachSpecies(pool.GetSpecies());

            GlobalData.CompatibilityThreshold = GlobalData.CompatibilityThreshold + GlobalData.CompatibilityModifier;
            pool.SetGeneration(pool.GetGeneration() + 1);
        }
    }

}
