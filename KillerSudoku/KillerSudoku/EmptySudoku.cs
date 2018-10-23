using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KillerSudoku
{
    class EmptySudoku
    {
        private List<Figure> figureList;
        private int[,] fullMatrix;
        private bool[,] booleanMatrix;
        private int[,] partialMatrix;
        private int[,] resultMatrix;
        private bool isCompleted;
        private int order;
        List<int> vector;
        private s myForm;
        private Int64 comparations;
        public EmptySudoku(s form, int order, int[,] newMatrix, List<Figure> figures)
        {
            myForm = form;
            fullMatrix = newMatrix;
            figureList = figures;
            this.order = order;
            vector = new List<int>();
            booleanMatrix = new bool[order, order];
            partialMatrix = new int[order, order];
            resultMatrix = new int[order, order];
            fillMatrices();
            fillPartialMatrix();
            isCompleted = false;
            comparations = 0;
        }
        private void fillMatrices()
        {
            for(int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    booleanMatrix[i, j] = false;
                    partialMatrix[i, j] = 0;
                    resultMatrix[i, j] = 0;
                }
            }
        }
        private void fillPartialMatrix()
        {
            fillVector();
            Random random = new Random();
            int i = 0;
            int times = 1;
            if (order > 7) { times++; }
            if (order > 11) { times++; }
            if (order > 14) { times++; }
            for (int k = 0; k < times; k++)
            {
                for (int j = 0; j < order; j++)
                {
                    i = vector[random.Next(vector.Count)];
                    if(booleanMatrix[i, j] == false)
                    {
                        partialMatrix[i, j] = fullMatrix[i, j];
                        resultMatrix[i, j] = fullMatrix[i, j];
                        booleanMatrix[i, j] = true;
                        vector.Remove(i);
                    }
                }
                fillVector();
            }
        }
        public int getPositionNumber(int i,int j)
        {
            return partialMatrix[i, j];
        }
        private void fillVector()
        {
            vector.Clear();
            for (int i = 0; i < order; i++)
            {
                vector.Add(i);
            }
        }
        private Figure getFigure(int i, int j)
        {
            foreach (Figure figure in figureList)
            {
                List<Dot> dots = figure.getDots();
                foreach (Dot dot in dots)
                {
                    if((dot.getI() == i) && (dot.getJ() == j))
                    {
                        return figure;
                    }
                }
            }
            return null;
        }
        public void solveSudoku()
        {
            int i = 0;
            int j = 0;
            List<int> options;
            if (isFull() == true)
            {
                isCompleted = true;
                myForm.stopThreads();
                comparations = 0;
                return;
            }
            else
            {
                for(int x = 0; x < order; x++)
                {
                    for (int y = 0; y < order; y++)
                    {
                        if(partialMatrix[x,y] == 0)
                        {
                            i = x;
                            j = y;
                            break;
                        }
                    }
                }
                options = getOptions(i, j);
                for (int k = 0; k < options.Count; k++)
                {
                    if (!isCompleted)
                    {
                        partialMatrix[i, j] = options[k];
                        resultMatrix[i, j] = options[k];
                        myForm.updateNumber(options[k], i, j);
                        comparations++;
                        myForm.updateComparations(comparations);
                        solveSudoku();
                    }
                }
                if (!isCompleted)
                {
                    partialMatrix[i, j] = 0;
                    myForm.updateNumber(0, i, j);
                }
            }
        }
        private List<int> getOptions(int i,int j)
        {
            List<int> options = new List<int>();
            for(int n = 1; n < order + 1; n++)
            {
                if(checkNumberRepetition(i,j,n) == true && checkFigureFit(i,j,n) == true)
                {
                    options.Add(n);
                }
            }
            return options;
        }
        public void setNoCompleted()
        {
            isCompleted = false;
        }
        private bool checkNumberRepetition(int i, int j, int n)
        {
            for(int k = 0; k < order; k++)
            {
                if(partialMatrix[k,j] == n)
                {
                    return false;
                }
            }
            for (int m = 0; m < order; m++)
            {
                if (partialMatrix[i, m] == n)
                {
                    return false;
                }
            }
            return true;
        }
        private bool checkFigureFit(int i, int j, int n)
        {
            Figure figure = getFigure(i, j);
            List<Dot> dots = figure.getDots();
            if(figure.isJoker() == false)
            {
                int n1 = partialMatrix[dots[0].getI(), dots[0].getJ()];
                int n2 = partialMatrix[dots[1].getI(), dots[1].getJ()];
                int n3 = partialMatrix[dots[2].getI(), dots[2].getJ()];
                int n4 = partialMatrix[dots[3].getI(), dots[3].getJ()];
                if (dots[0].getI() == i && dots[0].getJ() == j){ n1 = n;}
                if (dots[1].getI() == i && dots[1].getJ() == j) { n2 = n; }
                if (dots[2].getI() == i && dots[2].getJ() == j) { n3 = n; }
                if (dots[3].getI() == i && dots[3].getJ() == j) { n4 = n; }
                if (figure.getOperation() == "*")
                {
                    if (figure.isSubmultiple(n) == false)
                    {
                       return false;
                    }
                    else
                    {
                        if (n1 != 0 && n2 != 0 && n3 != 0 && n4 != 0)
                        {
                            if ((n1 * n2 * n3 * n4) != figure.getResult())
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (n1 == 0) { n1++; }
                            if (n2 == 0) { n2++; }
                            if (n3 == 0) { n3++; }
                            if (n4 == 0) { n4++; }
                            if (n1 * n2 * n3 * n4 > figure.getResult())
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    if (n1 != 0 && n2 != 0 && n3 != 0 && n4 != 0)
                    {
                        if ((n1 + n2 + n3 + n4) != figure.getResult())
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (n1 + n2 + n3 + n4 > figure.getResult())
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return true;
        }
        private bool isFull()
        {
            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    if(partialMatrix[i,j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void SaveSudoku(string path)
        {
            String line = "";
            System.IO.StreamWriter file = new System.IO.StreamWriter(path);
            using (file)
            {
                for (int i = 0; i < order; i++)
                {
                    for (int j = 0; j < order; j++)
                    {
                        line += resultMatrix[j, i] + ",";
                    }
                    string finalLine = line.Remove(line.LastIndexOf(','));
                    file.WriteLine(finalLine);
                    line = "";
                }
                //file.Close();
            }
            file.Close();
            // P with this line when you want to overwrite the file.
            /* 
               I "return"  in this second file:  
               x"," being the [x,#] of each dot in the list of figurates.
               y"," being the [#,i] of each dot in the list of figurates.        
               shape"," being the number of the determinated shape
               first","being just 0 or 1 to indicate if the number in the dot will be the first or not.
             */
            System.IO.StreamWriter Ffile = new System.IO.StreamWriter(path.Insert(path.IndexOf('.'), "_Figurates"));
            using (Ffile)
            {
                List<Figure> list = figureList;
                //Hide the file of figutares and do not allow the user to select it.
                //PD: In this case we hide the file before writting in it.
                // if ((File.GetAttributes(path.Insert(path.IndexOf('.') , "_Figurates")) & FileAttributes.Hidden) != FileAttributes.Hidden)
                // {
                //If the file is not hidden, then it chance to it.
                //    File.SetAttributes(path.Insert(path.IndexOf('.') , "_Figurates"), System.IO.FileAttributes.Hidden);                    
                //}
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
                        line += x + "," + y + "," + shape + "," + first;
                        Ffile.WriteLine(line);
                        line = "";
                    }
                }
                //Ffile.Close();
            }
            Ffile.Close();
            //In this case, We write first before hidden de file
            if ((File.GetAttributes(path.Insert(path.IndexOf('.'), "_Figurates")) & FileAttributes.Hidden) != FileAttributes.Hidden)
            {
                //If the file is not hidden, then it chance to it.
                File.SetAttributes(path.Insert(path.IndexOf('.'), "_Figurates"), System.IO.FileAttributes.Hidden);
            }
        }
    }
}
