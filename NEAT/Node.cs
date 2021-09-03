using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAT
{
    public class Node
    {
        private double value = 0;
        private int SerialNumber = 0;
        private string type = "";                 // Input Output Hidden Bias
        private string Function = "";
        public double GetValue()
        {
            return value;
        }
        public void SetValue(double x)
        {
            value = x;
        }
        public int GetSerialNumber()
        {
            return SerialNumber;
        }
        public void SetSerialNumber(int x)
        {
            SerialNumber = x;
        }
        public string GetType()
        {
            return type;
        }
        public void SetType(string s)
        {
            type = s;
        }
        public string GetFunction()
        {
            return Function;
        }
        public void SetFunction(string s)
        {
            Function = s;
        }

        //HyperNEAT
        private double x;
        private double y;
        public double GetX()
        {
            return x;
        }
        public void SetX(double n)
        {
            x = n;
        }
        public double GetY()
        {
            return y;
        }
        public void SetY(double n)
        {
            y = n;
        }
    }
}
