using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudoku
{
    class Dot
    {
        private int i;
        private int j;

        public Dot(int x, int y)
        {
            i = x;
            j = y;
        }
        public int getI()
        {
            return i;
        }
        public int getJ()
        {
            return j;
        }
    }
}
