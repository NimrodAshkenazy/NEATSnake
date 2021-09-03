using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_2._5
{
    class Tail
    {
        int? Tailx;
        int? Taily;
        public int? GetTailx()
        {
            return Tailx;
        }
        public void SetTailx(int? i)
        {
            Tailx = i;
        }
        public int? GetTaily()
        {
            return Taily;
        }
        public void SetTaily(int? i)
        {
            Taily = i;
        }
    }
}
