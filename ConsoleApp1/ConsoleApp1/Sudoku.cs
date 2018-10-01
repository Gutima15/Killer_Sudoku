using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{

    class Sudoku
    {
        private int[,] matrix;
        private int[,] booleanMatrix;
        private int order;
        private int size;
        private List<int> vector;
        private List<String> vectorString;
        private Boolean repeat;
        private int failCounter;
        public Sudoku(int size)
        {
            matrix = new int[size,size];
            failCounter = 0;
            vector = new List<int>();
            vectorString = new List<String>();
            repeat = false;
            for (int i = 1; i < size + 1; i++)
            {
                vector.Add(i);
            }
            order = size;
            this.size = size;
            String line = "";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = getNumber(i,j);
                    line = line + matrix[i, j] + " ";
                    if(repeat == true)
                    {
                        repeat = false;
                        j = 0;
                        fillVector();
                        failCounter = 0;
                    }
                }
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("--------Fila " + i + " completa---------");
                Console.WriteLine("----------------------------------------");
                //vectorString.Add(line);
                //line = "";
                fillVector();
            }
            /*
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Jorge Eduardo\Desktop\WriteLines2.txt"))
            {
                foreach (String newLine in vectorString)
                {
                    file.WriteLine(newLine);
                }
            }
            */
        }
        public int GetSize() {
            return size;
        }

        private void fillVector()
        {
            vector.Clear();
            for (int i = 1; i < order+1; i++)
            {
                vector.Add(i);
            }
        }
        /* Evalutes if the number can be use depending of the condition and locate the choosen number in the matrix
         * */
        private int getNumber(int x, int y)
        {
            Random rnd = new Random();
            int index = rnd.Next(vector.Count);
            //Console.WriteLine("Index: " + index);
            int number = vector[index];
            //Console.WriteLine("Number: " + number);
            Boolean exists = false;
            Boolean done = false;
            
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
                    //Console.WriteLine("No sirvió");
                    index = rnd.Next(vector.Count);
                    number = vector[index];
                    //Console.WriteLine("Index: " + index);
                    //Console.WriteLine("Number: " + number);
                    exists = false;
                    failCounter++;
                    if(failCounter == 25)
                    {
                        repeat = true;
                        failCounter = 0;
                        Console.WriteLine("Se debe repetir la fila");
                        done = true;
                    }
                }
                else
                {
                    Console.WriteLine("Si sirvió, se elimina: "+number);
                    vector.Remove(number);
                    failCounter = 0;
                    done = true;
                }   
            }
            return number;
        }

        public void printSudoku()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] > 9)
                    {
                        Console.Write(matrix[i, j] + "  ");
                    }
                    else
                    {
                        Console.Write(matrix[i, j] + "   ");
                    }
                }
                Console.WriteLine(" ");
            }
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

        public void GenerateBooleanMatriz()
        {
            booleanMatrix = new int[GetSize(), GetSize()];
            for(int i=0; i < GetSize(); i++)
            {
                for(int j=0; j < GetSize(); j++)
                {
                    booleanMatrix[i, j] = 0;
                }
            }
        }

        public void printboolean()
        {
            for (int i = 0; i < GetSize(); i++)
            {
                for (int j = 0; j < GetSize(); j++)
                {
                    Console.Write(booleanMatrix[i, j] + "   ");
                }
                Console.WriteLine(" ");
            }
        }
    }
}
