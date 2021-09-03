using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_2._5
{
    class Snake
    {
        int xPos;
        int yPos;
        int lastMove;  //0 left 1 right 2 up 3 down
        List<Tail> tails = new List<Tail>();
        public int GetxPos()
        {
            return xPos;
        }
        public void SetxPos(int i)
        {
            xPos = i;
        }
        public int GetyPos()
        {
            return yPos;
        }
        public void SetyPos(int i)
        {
            yPos = i;
        }
        public int GetlastMove()
        {
            return lastMove;
        }
        public void SetlastMove(int i)
        {
            lastMove = i;
        }
        public List<Tail> GetTails()
        {
            return tails;
        }
        public void AddaTail(Tail tail)
        {
            tails.Add(tail);
        }
        public void CleanTails()
        {
            tails.Clear();
        }
    }
}
