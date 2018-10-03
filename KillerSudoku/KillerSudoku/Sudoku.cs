using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudoku
{
    class Sudoku
    {
        private int[,] matrix;
        private FigureFactory figureFactory;
        private bool[,] booleanMatrix;
        private List<Figure> figureList;
        private int order;
        private int size;
        private List<int> vector;
        private List<string> vectorString;
        private bool repeat;
        private int failCounter;

        public Sudoku(int size)
        {
            order = size;
            this.size = size;
            matrix = new int[size, size];
            failCounter = 0;
            vector = new List<int>();
            vectorString = new List<string>();
            repeat = false;
            generateMatrix();
            GenerateBooleanMatrix();
            figureFactory = new FigureFactory(size, matrix, booleanMatrix);
            figureList = figureFactory.getFigures();
            
        }

        public void generateMatrix()
        {
            fillVector();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = getNumber(i, j);
                    if (repeat == true)
                    {
                        repeat = false;
                        j = 0;
                        fillVector();
                        failCounter = 0;
                    }
                }
                fillVector();
            }
        }
        public int GetSize()
        {
            return size;
        }

        private void fillVector()
        {
            vector.Clear();
            for (int i = 1; i < order + 1; i++)
            {
                vector.Add(i);
            }
        }
        
        private int getNumber(int x, int y)
        {
            Random rnd = new Random();
            int index = rnd.Next(vector.Count);
            int number = vector[index];
            bool exists = false;
            bool done = false;

            while (done == false)
            {
                for (int i = 0; i < x; i++)
                {
                    if (matrix[i, y] == number)
                    {
                        exists = true;
                    }
                }
                for (int j = 0; j < y; j++)
                {
                    if (matrix[x, j] == number)
                    {
                        exists = true;
                    }
                }
                if (exists == true)
                {
                    index = rnd.Next(vector.Count);
                    number = vector[index];
                    exists = false;
                    failCounter++;
                    if (failCounter == 25)
                    {
                        repeat = true;
                        failCounter = 0;
                        done = true;
                    }
                }
                else
                {
                    vector.Remove(number);
                    failCounter = 0;
                    done = true;
                }
            }
            return number;
        }
        
        public void saveSudoku()
        {
            String line = "";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Jorge Eduardo\Desktop\WriteLines.txt"))
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        line += matrix[i, j] + ",";
                    }
                    file.WriteLine(line);
                    line = "";
                }
            }
        }

        public void GenerateBooleanMatrix()
        {
            booleanMatrix = new Boolean[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    booleanMatrix[i, j] = false;
                }
            }
        }
        
        public int[,] GetMatrix()
        {
            return matrix;
        }
        public List<Figure> getFiguresList()
        {
            return figureList;
        }
        public int getPositionNumber(int x,int y)
        {
            return matrix[x, y];
        }
    }
}
