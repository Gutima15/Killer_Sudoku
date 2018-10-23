using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

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
        private s myForm;
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
            generateMatrix();
            GenerateBooleanMatrix();
            figureFactory = new FigureFactory(size, matrix, booleanMatrix);
            figureList = figureFactory.getFigures();
            FillNullMatrix();
            FillRandomNumbers();
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
        public void SaveSudoku(string path)
        {
            String line = "";
            System.IO.StreamWriter file = new System.IO.StreamWriter(path);
            using (file)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        line += matrix[j, i] + ",";
                    }
                    //string finalLine= line.Remove(line.LastIndexOf(','));                    
                    file.WriteLine(line);
                    line = "";
                }
                //file.Close();
            }
            file.Close();
            SaveFigurates(path);
        }
        /* 
           I "return"  in this second file:  
           x"," being the [x,#] of each dot in the list of figurates.
           y"," being the [#,i] of each dot in the list of figurates.        
           shape"," being the number of the determinated shape
           first","being just 0 or 1 to indicate if the number in the dot will be the first or not.
         */
        public void SaveFigurates(string path)
        {
            System.IO.StreamWriter Ffile = new System.IO.StreamWriter(path.Insert(path.IndexOf('.'), "_Figurates"));
            String line = "";
            using (Ffile)
            {
                List<Figure> list = getFiguresList();
                for (int c = 0; c < list.Count; c++)
                {
                    Figure figure = list.ElementAt(c);
                    int x;
                    int y;
                    int shape;
                    int first;
                    foreach (Dot item in figure.getDots())
                    {
                        x = item.getI();
                        y = item.getJ();
                        shape = item.getShape();
                        if (item.verifyFirst() == true)
                        {
                            first = 0;
                        }
                        else
                        {
                            first = 1;
                        }
                        line += x + "," + y + "," + shape + "," + first + ",";
                        Ffile.WriteLine(line);
                        line = "";
                    }
                    line += figure.getT() + "," + figure.getColor() + "," + figure.getOperation() + "," + figure.getResult();
                    Ffile.WriteLine(line);
                    line = "";
                }
            }
            Ffile.Close();
            hideFile(path.Insert(path.IndexOf('.'), "_Figurates"));
            //In this case, We write first before hidden de file            
        }
        public void setPositionNumber(int x, int y, int num)
        {
            matrix[x, y] = num;
        }
        public void hideFile(string path)
        {
            if ((File.GetAttributes(path) & FileAttributes.Hidden) != FileAttributes.Hidden)
            {
                //If the file is not hidden, then it chance to it.
                File.SetAttributes(path, System.IO.FileAttributes.Hidden);
            }
        }
        public void LoadSudoku(string path)//Al cargar el sudoku, reescribo lo que hay en la matriz, debo hacer lo mismo con la lista de figuras
        {
            matrix = new int[size, size];
            StreamReader fileR = new StreamReader(path);
            string[] array;// = new string[GetSize()*2];
            string line = fileR.ReadToEnd(); //Todo el file
            array = line.Split(','); //["1","3","2","5","4",...]
            int x = 0;
            int c = 0;
            for(int k = 0; k < array.Length; k++)
            {
                int i = 0;
                int? value = Int32.TryParse(array[k], out i) ? i : (int?)null;
                if (value.HasValue)
                {
                    setPositionNumber(c, x, (int)value);
                    c++;
                    if (c == order)
                    {
                        c = 0;
                        x++;
                    }
                }
            }
        }
        public void LoadFigurates(string path)
        {
            figureList = new List<Figure>();
            GenerateBooleanMatrix();
            StreamReader fileR = new StreamReader(path);
            List<Dot> dots = new List<Dot>();
            while (!fileR.EndOfStream)
            {
                string textLine = fileR.ReadLine();//0,0,3,0,   
                string[] array2 = textLine.Split(',');
                if (textLine.EndsWith(","))
                {
                    int x = Convert.ToInt32(array2[0]);
                    int y = Convert.ToInt32(array2[1]);
                    int shape = Convert.ToInt32(array2[2]);
                    bool first = false;
                    if (Convert.ToInt32(array2[3]) == 0)
                    {
                        first = true;
                    }
                    Dot nDot = new Dot(x, y, shape, first);
                    dots.Add(nDot);
                }
                else //1,Color [LawnGreen],*,8
                {
                    string[] array = textLine.Split(',');
                    int type = Convert.ToInt32(array[0]);
                    string[] secArray = array[1].Split(']'); //["Color [LawnGreen",  ","   , "*" , "8" ]
                    string color = secArray[0].Substring(secArray[0].IndexOf('[') + 1);
                    string operation = array[2];
                    int result = Convert.ToInt32(array[3]);
                    if (result != 0)
                    {
                        Figure figure = new Figure(order,type, Color.FromName(color), operation, result, dots);
                        figureList.Add(figure);
                    }
                    else
                    {
                        Figure figure = new Figure(type, Color.FromName(color), dots); //Joker builder
                        figureList.Add(figure);
                    }
                    dots = new List<Dot>();
                }
            }
        }
    }
}
