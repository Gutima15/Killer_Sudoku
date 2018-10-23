using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudoku
{
    class Figure
    {
        private int type;
        private Color color;
        private string operation;
        private int result;
        private List<Dot> dotList;
        private List<int> submultiples;
        private bool joker;
        private int order;

        public Figure(int order,int type,Color color,string operation,int result,List<Dot> dotList)
        {
            this.order = order;
            this.type = type;
            this.color = color;
            this.operation = operation;
            this.dotList = dotList;
            this.result = result;
            submultiples = new List<int>();
            fillSubmultiplesList();
            joker = false;
        }
        public Figure(int type, Color color, List<Dot> dotList)
        {
            this.type = type;
            this.color = color;
            this.dotList = dotList;
            submultiples = null;
            joker = true;
            operation = "";
            result = 0;
        }
        public Color getColor()
        {
            return color;
        }
        public List<Dot> getDots()
        {
            return dotList;
        }
        public int getResult()
        {
            return result;
        }
        public string getOperation()
        {
            return operation;
        }
        public bool isJoker()
        {
            return joker;
        }
        public void fillSubmultiplesList()
        {
            if (operation == "*")
            {
                double res = (double)result;
                int res1 = 0;
                double res2 = 0;
                for (double u = 1; u < order + 1; u++)
                {
                    res1 = (int)(result / u);
                    res2 = res / u;
                    if ((res2 - res1) == 0)
                    {
                        submultiples.Add((int)u);
                    }
                }
            }
        }
        public Boolean isSubmultiple(int n)
        {
            for(int i = 0; i < submultiples.Count; i++)
            {
                if(submultiples[i] == n)
                {
                    return true;
                }
            }
            return false;
        }
        public int getT()
        {
            return type;
        }
    }
}
