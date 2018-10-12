using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace KillerSudoku
{
    class Sudoku
    {
        private int[,] matrix;
        private int[,] resultMatrix;
        private FigureFactory figureFactory;
        private bool[,] booleanMatrix;
        private List<Figure> figureList;
        private int order;
        private int size;
        private int rowsComplete;
        private List<int> vector;
        private List<string> vectorString;
        private bool repeat;
        private bool matrixComplete;
        private StreamReader objReader;
        private StreamReader objReader2;
        private Thread qmainThread;
        private s myForm;
        //private string sLine;
        // private ArrayList arrText;
        public Sudoku(s form, int size)
        {
            myForm = form;
            order = size;
            this.size = size;
            matrix = new int[size, size];
            resultMatrix = new int[size, size];
            rowsComplete = 0;
            vector = new List<int>();
            vectorString = new List<string>();
            repeat = false;
            matrixComplete = false;
            qmainThread = new Thread(generateMatrix);
            qmainThread.Start();
            qmainThread.Join();
            //generateMatrix();
            GenerateBooleanMatrix();
            figureFactory = new FigureFactory(size, matrix, booleanMatrix);
            figureList = figureFactory.getFigures();
            FillNullMatrix();
            FillRandomNumbers();
        }
        public Sudoku(string firstFile, string secondFile)
        {
            objReader = new StreamReader(firstFile);
            objReader2= new StreamReader(secondFile);                 
        }
        public void generateMatrix()
        {
            myForm.updateGeneratingLabel("Generating...", "0%");
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
                    }
                }
                rowsComplete++;
                myForm.CleanRightPanel();
                myForm.updateGeneratingLabel("Generating...", getCompletedPercentage().ToString() + "%");
                Thread.Sleep(100);
            }
            myForm.CleanRightPanel();
        }
        public bool getMatrixComplete()
        {
            return matrixComplete;
        }
        public int getCompletedPercentage()
        {
            return (rowsComplete*100)/order;
        }
        public void SaveSudoku()
        {
            String line = "";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\josue\Desktop\WriteLines.txt"))
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
        public void LoadSodoku()
        {
            return;
        }
        public void FillNullMatrix()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    resultMatrix[i, j] = 0;
                }
            }
        }
        
        public void FillRandomNumbers()
        {
            Random rnd = new Random();
            for (int i=0; i < GetSize(); i++)
            {
                for (int j =0; j< GetSize(); j++)
                {
                    int randomNumber = rnd.Next(1, size);
                    if (randomNumber == 1) 
                    {
                        resultMatrix[i, j] = matrix[i, j];
                    }
                }
                                         
            }
            
        }
        private int getNumber(int x, int y)
        {
            Random rnd = new Random();
            int number = vector[rnd.Next(vector.Count)];
            bool exists = false;
            bool done = false;
            while (done == false)
            {
                for (int i = 0; i < x; i++) //Check above it
                {
                    if (matrix[i, y] == number)
                    {
                        exists = true;
                    }
                }
                for (int j = 0; j < y; j++)//check at the left
                {
                    if (matrix[x, j] == number)
                    {
                        exists = true;
                    }
                }
                if (exists == true)
                {
                    vector.Remove(number);
                    exists = false;
                    if(vector.Count == 0)
                    {
                        repeat = true;
                        done = true;
                        fillVector();
                    }
                    else
                    {
                        number = vector[rnd.Next(vector.Count)];
                    }
                }
                else
                {
                    fillVector();
                    done = true;
                }
            }
            return number;
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
        public int getResultPositionNumber(int x, int y)
        {
            return resultMatrix[x, y];
        }
    }
}
