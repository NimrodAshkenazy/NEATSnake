using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAT
{
    public class GlobalData
    {
        private static int Inputs;
        private static int Outputs;
        public static int InputsNum
        {
            get { return Inputs; }
            set { Inputs = value; }
        }
        public static int OutputsNum
        {
            get { return Outputs; }
            set { Outputs = value; }
        }

        public static int HiddenNodes = 0; 
        public static int averageStatingGenes = 0;
        public static int GenomeStatingSize = 150;

        public static double DisjointCoefficient = 2.0;          //c1
        public static double ExcessCoefficient = 2.0;            //c2
        public static double WeightDifferenceCoefficient = 1.0;  //c3  
        public static double CompatibilityThresholdStart = 6.0;   //6.0
        public static double CompatibilityModifier = 0.3;

        public static double MutationPower = 2.5;
        public static double SurvivalThreshold = 0.2;  //0.2
        public static int DropoffAge = 15;

        public static double PerturbChance = 0.9;
        public static double CrossoverChance = 0.75;

        public static double MutateConnectionsChance = 0.25;       //  0.25  
        public static double LinkMutationChance = 2;               //  2  
        public static double NodeMutationChance = 0.5;             //  0.5  
        public static double BiasMutationChance = 0.4;             //  0.4  
        public static double DisableMutationChance = 0.4;          //  0.4  
        public static double EnableMutationChance = 0.2;           //  0.2  

        public static double StepSize = 0.1;                       //0.1

        public static int MaxSpeicesNum = 20;

        public static int OldAgeThreshold = 50;
        public static double OldAgePenalty = 0.7;
        public static int YouthThreshold = 10;
        public static double YouthBonus = 1.3;

        public static bool NormalizeCompatibilityDistance = true;
        //------------------------
        private static double Number;
        private static int InnovationNumber;
        private static int speciesSerialNumber;
        public static double CompatibilityThreshold
        {
            get { return Number; }
            set { Number = value; }
        }
        public static int Innovation
        {
            get { return InnovationNumber; }
            set { InnovationNumber = value; }
        }
        public static int speciesSerialNum
        {
            get { return speciesSerialNumber; }
            set { speciesSerialNumber = value; }
        }

        //------------------------
        // HyperNEAT
        public static int initialDepth = 4;
        public static int maxDepth = 8;
        public static double varianceThreshold = 0.03;
        public static double bandThreshold = 0.3;
        public static int iterationLevel = 1;
        public static double divisionThreshold = 0.03;
    }
}
