using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAT
{
    class Quadtree
    {     
        private double x;
        private double y;
        private double Width;
        private int Level;
   
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
        public double GetWidth()
        {
            return Width;
        }
        public void SetWidth(double x)
        {
            Width = x;
        }
        public int GetLevel()
        {
            return Level;
        }
        public void SetLevel(int n)
        {
            Level = n;
        }
        
        private double value = 0;
        public double GetValue()
        {
            return value;
        }
        public void SetValue(double x)
        {
            value = x;
        }

        private double Weight = 0;
        public double GetWeight()
        {
            return Weight;
        }
        public void SetWeight(double X)
        {
            Weight = X;
        }

        List<Quadtree> quadtrees = new List<Quadtree>();
        public List<Quadtree> GetQuadtrees()
        {
            return quadtrees;
        }
        public void SetQuadtree(Quadtree tree)
        {
            quadtrees.Add(tree);
        }

       

      
    }
}
