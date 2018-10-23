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
        private bool isFirst;

        public Dot(int x, int y,int shape)
        {
            i = x;
            j = y;
            this.shape = shape;
            isFirst = false;
        }
        public Dot(int x, int y, int shape,bool first)
        {
            i = x;
            j = y;
            this.shape = shape;
            isFirst = first;
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
        public bool verifyFirst()
        {
            return isFirst;
        }
    }
}
