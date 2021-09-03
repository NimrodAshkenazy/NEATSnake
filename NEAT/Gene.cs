using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAT
{
    public class Gene
    {
        private int into;
        private int outo;
        private double weight = 0;
        private bool enabled = true;
        private int innovation = 0;    
        private bool Recurrent=false;
        public int GetInto()
        {
            return into;
        }
        public void SetInto(int n)
        {
            into = n;
        }
        public int GetOuto()
        {
            return outo;
        }
        public void SetOuto(int n)
        {
            outo = n;
        }
        public double GetWeight()
        {
            return weight;
        }
        public void SetWeight(double n)
        {
            weight = n;
        }
        public bool IsEnabled()
        {
            return enabled;
        }
        public void SetEnabled(bool n)
        {
            enabled = n;
        }
        public int GetInnovation()
        {
            return innovation;
        }
        public void SetInnovation(int n)
        {
            innovation = n;
        }
        public bool IsRecurrent()
        {
            return Recurrent;
        }
        public void SetRecurrent(bool n)
        {
            Recurrent=n;
        }

        //HyperNEAT
        private double x1;
        private double y1;
        private double x2;
        private double y2;
        public double GetX1()
        {
            return x1;
        }
        public void SetX1(double n)
        {
            x1 = n;
        }
        public double GetY1()
        {
            return y1;
        }
        public void SetY1(double n)
        {
            y1 = n;
        }
        public double GetX2()
        {
            return x2;
        }
        public void SetX2(double n)
        {
            x2 = n;
        }
        public double GetY2()
        {
            return y2;
        }
        public void SetY2(double n)
        {
            y2 = n;
        }

    }
}
