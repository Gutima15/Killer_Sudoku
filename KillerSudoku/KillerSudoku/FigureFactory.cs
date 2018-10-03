using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KillerSudoku
{
    class FigureFactory
    {
        private List<Figure> vector;
        private List<int> numbers;
        private List<Dot> dots;
        private string operation;
        private int result;
        private int order;
        private bool[,] boolMatrix;
        private int[,] matrix;
        private int id;

        public FigureFactory(int order, int[,] matrix, bool[,] boolMatrix)
        {
            this.order = order;
            this.matrix = matrix;
            this.boolMatrix = boolMatrix;
            vector = new List<Figure>();
            dots = new List<Dot>();
            numbers = new List<int>();
            operation = "";
            result = 0;
            id = 1;
            FillNumbers();
        }
        private string getOperation()
        {
            Random rnd = new Random();
            int op = rnd.Next(1, 3);
            if(op == 1)
            {
                return "+";
            }
            if(op == 2)
            {
                return "*";
            }
            else
            {
                return "%";
            } 
        }
        private int getResult(string op,int n1,int n2,int n3,int n4)
        {
            if (op == "+")
            {
                return n1 + n2 + n3 + n4;
            }
            if (op == "*")
            {
                return n1 * n2 * n3 * n4;
            }
            else
            {
                return n1 % n2 % n3 % n4;
            }
        }
        public List<Figure> getFigures()
        {
            Random rnd = new Random();
            bool done = false;
            int n = 0;
            for(int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    if (boolMatrix[i, j] == false)
                    {
                        done = false;
                        FillNumbers();
                        while (done == false)
                        {
                            if (numbers.Count == 0)
                            {
                                done = true;
                            }
                            else
                            {
                                int index = rnd.Next(numbers.Count);
                                n = numbers[index];

                                switch (n)
                                {
                                    case 1://-----------FIGURE 1------------- COMPLETE     Blue
                                        if ((i + 1) < order && (i + 2) < order && (i + 3) < order)
                                        {
                                            if (boolMatrix[i + 1, j] == false && boolMatrix[i + 2, j] == false && boolMatrix[i + 3, j] == false)
                                            {
                                                createFigure(i, j, i + 1, j, i + 2, j, i + 3, j, Color.Blue);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 2://-----------FIGURE 2-------------COMPLETE      Blue
                                        if ((j + 1) < order && (j + 2) < order && (j + 3) < order)
                                        {
                                            if (boolMatrix[i, j + 1] == false && boolMatrix[i, j + 2] == false && boolMatrix[i, j + 3] == false)
                                            {
                                                createFigure(i, j, i, j + 1, i, j + 2, i, j + 3, Color.Blue);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 3://-----------FIGURE 3-------------COMPLETE      Violet
                                        if ((j + 1) < order && (j - 1) >= 0 && (i + 1) < order)
                                        {
                                            if (boolMatrix[i, j + 1] == false && boolMatrix[i + 1, j] == false && boolMatrix[i + 1, j - 1] == false)
                                            {
                                                createFigure(i, j, i, j + 1, i + 1, j, i + 1, j - 1, Color.Violet);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 4://-----------FIGURE 4-------------COMPLETE       Violet
                                        if ((i + 1) < order && (i + 2) < order && (j + 1) < order)
                                        {
                                            if (boolMatrix[i + 1, j] == false && boolMatrix[i + 1, j + 1] == false && boolMatrix[i + 2, j + 1] == false)
                                            {
                                                createFigure(i, j, i + 1, j, i + 1, j + 1, i + 2, j + 1, Color.Violet);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 5://-----------FIGURE 5-------------COMPLETE       Red
                                        if ((j + 1) < order && (j + 2) < order && (i + 1) < order)
                                        {
                                            if (boolMatrix[i, j + 1] == false && boolMatrix[i + 1, j + 1] == false && boolMatrix[i + 1, j + 2] == false)
                                            {
                                                createFigure(i, j, i, j + 1, i + 1, j + 1, i + 1, j + 2, Color.Red);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 6://-----------FIGURE 6-------------COMPLETE        Red
                                        if ((i + 1) < order && (i + 2) < order && (j - 1) >= 0)
                                        {
                                            if (boolMatrix[i + 1, j] == false && boolMatrix[i + 1, j - 1] == false && boolMatrix[i + 2, j - 1] == false)
                                            {
                                                createFigure(i, j, i + 1, j, i + 1, j - 1, i + 2, j - 1, Color.Red);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 7://-----------FIGURE 7-------------COMPLETE        Orange
                                        if ((i + 1) < order && (j + 1) < order)
                                        {
                                            if (boolMatrix[i, j + 1] == false && boolMatrix[i + 1, j] == false && boolMatrix[i + 1, j + 1] == false)
                                            {
                                                createFigure(i, j, i, j + 1, i + 1, j, i + 1, j + 1, Color.Orange);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 8://-----------FIGURE 8-------------COMPLETE        Yellow
                                        if ((i + 1) < order && (i + 2) < order && (j + 1) < order)
                                        {
                                            if (boolMatrix[i + 1, j] == false && boolMatrix[i + 2, j] == false && boolMatrix[i + 2, j + 1] == false)
                                            {
                                                createFigure(i, j, i + 1, j, i + 2, j, i + 2, j + 1, Color.Yellow);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 9://-----------FIGURE 9-------------COMPLETE        Yellow
                                        if ((j + 1) < order && (j + 2) < order && (i + 1) < order)
                                        {
                                            if (boolMatrix[i, j + 1] == false && boolMatrix[i, j + 2] == false && boolMatrix[i + 1, j] == false)
                                            {
                                                createFigure(i, j, i, j + 1, i, j + 2, i + 1, j, Color.Yellow);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 10://-----------FIGURE 10-------------COMPLETE       Yellow
                                        if ((j + 1) < order && (i + 1) < order && (i + 2) < order)
                                        {
                                            if (boolMatrix[i, j + 1] == false && boolMatrix[i + 1, j + 1] == false && boolMatrix[i + 2, j + 1] == false)
                                            {
                                                createFigure(i, j, i, j + 1, i + 1, j + 1, i + 2, j + 1, Color.Yellow);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 11://-----------FIGURE 11-------------COMPLETE        Yellow
                                        if ((i + 1) < order && (j - 1) >= 0 && (j - 2) >= 0)
                                        {
                                            if (boolMatrix[i + 1, j] == false && boolMatrix[i + 1, j - 1] == false && boolMatrix[i + 1, j - 2] == false)
                                            {
                                                createFigure(i, j, i + 1, j, i + 1, j - 1, i + 1, j - 2, Color.Yellow);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 12://-----------FIGURE 12-------------COMPLETE        Green
                                        if ((j - 1) >= 0 && (i + 1) < order && (i + 2) < order)
                                        {
                                            if (boolMatrix[i + 1, j] == false && boolMatrix[i + 2, j] == false && boolMatrix[i + 2, j - 1] == false)
                                            {
                                                createFigure(i, j, i + 1, j, i + 2, j, i + 2, j - 1, Color.Green);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 13://-----------FIGURE 13-------------COMPLETE        Green
                                        if ((i + 1) < order && (j + 1) < order && (j + 2) < order)
                                        {
                                            if (boolMatrix[i + 1, j] == false && boolMatrix[i + 1, j + 1] == false && boolMatrix[i + 1, j + 2] == false)
                                            {
                                                createFigure(i, j, i + 1, j, i + 1, j + 1, i + 1, j + 2, Color.Green);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 14://-----------FIGURE 14-------------COMPLETE        Green
                                        if ((j + 1) < order && (i + 1) < order && (i + 2) < order)
                                        {
                                            if (boolMatrix[i, j + 1] == false && boolMatrix[i + 1, j] == false && boolMatrix[i + 2, j] == false)
                                            {
                                                createFigure(i, j, i, j + 1, i + 1, j, i + 2, j, Color.Green);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 15://-----------FIGURE 15-------------COMPLETE        Green
                                        if ((j + 1) < order && (j + 2) < order && (i + 1) < order)
                                        {
                                            if (boolMatrix[i, j + 1] == false && boolMatrix[i, j + 2] == false && boolMatrix[i + 1, j + 2] == false)
                                            {
                                                createFigure(i, j, i, j + 1, i, j + 2, i + 1, j + 2, Color.Green);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 16://-----------FIGURE 16-------------COMPLETE        Cyan
                                        if ((j + 1) < order && (j - 1) >= 0 && (i + 1) < order)
                                        {
                                            if (boolMatrix[i + 1, j - 1] == false && boolMatrix[i + 1, j] == false && boolMatrix[i + 1, j + 1] == false)
                                            {
                                                createFigure(i, j, i + 1, j - 1, i + 1, j, i + 1, j + 1, Color.Cyan);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 17://-----------FIGURE 17-------------COMPLETE        Cyan
                                        if ((j + 1) < order && (i + 1) < order && (i + 2) < order)
                                        {
                                            if (boolMatrix[i + 1, j] == false && boolMatrix[i + 2, j] == false && boolMatrix[i + 1, j + 1] == false)
                                            {
                                                createFigure(i, j, i + 1, j, i + 1, j + 1, i + 2, j, Color.Cyan);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 18://-----------FIGURE 18-------------COMPLETE        Cyan
                                        if ((j + 1) < order && (j + 2) < order && (i + 1) < order)
                                        {
                                            if (boolMatrix[i, j + 1] == false && boolMatrix[i, j + 2] == false && boolMatrix[i + 1, j + 1] == false)
                                            {
                                                createFigure(i, j, i, j + 1, i + 1, j + 1, i, j + 2, Color.Cyan);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                    case 19://-----------FIGURE 19-------------COMPLETE        Cyan
                                        if ((j + 1) < order && (j + 2) < order && (j + 3) < order)
                                        {
                                            if (boolMatrix[i, j + 1] == false && boolMatrix[i, j + 2] == false && boolMatrix[i, j + 3] == false)
                                            {
                                                createFigure(i, j, i + 1, j - 1, i + 1, j, i + 2, j, Color.Cyan);
                                                done = true; // FINISH
                                            }
                                            else
                                            {
                                                numbers.Remove(n);
                                            }
                                        }
                                        else
                                        {
                                            numbers.Remove(n);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return vector;
        }
        private void createFigure(int i1,int j1, int i2, int j2, int i3, int j3, int i4, int j4,Color color)
        {
            boolMatrix[i1, j1] = true;
            boolMatrix[i2, j2] = true;
            boolMatrix[i3, j3] = true;
            boolMatrix[i4, j4] = true;
            dots.Add(new Dot(i1, j1));
            dots.Add(new Dot(i2, j2));
            dots.Add(new Dot(i3, j3));
            dots.Add(new Dot(i4, j4));
            operation = getOperation();
            result = getResult(operation, matrix[i1, j1], matrix[i2, j2], matrix[i3, j3], matrix[i4, j4]);
            vector.Add(new Figure(1, id, color, operation, result, dots));
            operation = "";
            result = 0;
            id++;
            dots = new List<Dot>();
            FillNumbers();
        }
        private void FillNumbers()
        {
            numbers.Clear();
            for (int i = 1; i < 19; i++)
            {
                numbers.Add(i);
            }
        }
    }
}
