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
        private int shape;

        public Dot(int x, int y,int shape)
        {
            i = x;
            j = y;
            this.shape = shape;
        }
        public int getI()
        {
            return i;
        }
        public int getJ()
        {
            return j;
        }
        public int getShape()
        {
            return shape;
        }
    }
}
