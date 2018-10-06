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
        private string operation;  //(1,+),(2,*),(3,%)
        private int result;
        private List<Dot> dotList;
        private bool isJoker;

        public Figure(int type,Color color,string operation,int result,List<Dot> dotList)
        {
            this.type = type;
            this.color = color;
            this.operation = operation;
            this.dotList = dotList;
            this.result = result;
            isJoker = false;
        }
        public Figure(int type, Color color, List<Dot> dotList)
        {
            this.type = type;
            this.color = color;
            this.dotList = dotList;
            isJoker = true;
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
    }
}
