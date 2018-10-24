using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillerSudoku
{
    public partial class s : Form
    {
        private int x1;
        private int x2;
        private int y1;
        private int y2;
        private int order;
        private Sudoku sudoku;
        private EmptySudoku sudoku2;
        private Graphics graphics;
        private Graphics graphics2;
        private Graphics graphics3;
        private Pen normalPen;
        private SolidBrush backBrush;

        private Thread counter;
        private Thread[] array;
        private int threads;
        private int minutes;
        private int seconds;

        private bool solving;
        private bool generated;
        private bool generating;
        private bool solved;

        public s()
        {
            InitializeComponent();
            Screen screen = Screen.PrimaryScreen;
            array = new Thread[1];
            x1 = 0;
            x2 = 600;
            y1 = 0;
            y2 = 0;
            SetBounds(0, 0, screen.Bounds.Width, screen.Bounds.Height);
            graphics = CreateGraphics();
            graphics2 = CreateGraphics();
            graphics3 = CreateGraphics();
            normalPen = new Pen(Color.Gray, 1);
            backBrush = new SolidBrush(Color.White);
            minutes = 0;
            seconds = 0;
            threads = 1;
            solving = false;
            generated = false;
            solved = false;
        }

        private void CleanPanel_1()
        {
            graphics.DrawRectangle(normalPen, new Rectangle(x1, y1, 600, 600));
            graphics.FillRectangle(backBrush, new Rectangle(x1 + 1, y1 + 1, 599, 599));
        }
        private void CleanPanel_2()
        {
            graphics.DrawRectangle(normalPen, new Rectangle(x2, y2, 600, 600));
            graphics.FillRectangle(backBrush, new Rectangle(x2 + 1, y2 + 1, 599, 599));
        }
        public void CleanRightPanel()
        {
            graphics2.FillRectangle(new SolidBrush(Color.Black), new Rectangle(1205, 220, 150, 60));
        }
        public void updateGeneratingLabel(String word, String percentage)
        {
            Graphics graphs = CreateGraphics();
            graphs.DrawString(word, new Font("Arial", 16), new SolidBrush(Color.Yellow), 1210, 230);
            graphs.DrawString(percentage, new Font("Arial", 16), new SolidBrush(Color.Yellow), 1250, 260);
        }
        public void updateTime(int minutes, int seconds)
        {
            Graphics graphs = CreateGraphics();
            CleanRightPanel();
            String mins = minutes + "";
            String secs = seconds + "";
            if (minutes < 10) { mins = "0" + mins; }
            if (seconds < 10) { secs = "0" + secs; }
            graphs.DrawString("Solving...", new Font("Arial", 16), new SolidBrush(Color.Yellow), 1210, 230);
            graphs.DrawString("Time: " + mins + ":" + secs, new Font("Arial", 16), new SolidBrush(Color.Yellow), 1210, 260);
        }
        public void updateComparations(Int64 n)
        {
            Graphics graphs = CreateGraphics();
            graphs.FillRectangle(new SolidBrush(Color.Black), new Rectangle(1205, 310, 150, 30));
            graphs.DrawString(n+"", new Font("Arial", 16), new SolidBrush(Color.Yellow), 1210, 320);
        }
        private void drawSquareLines(int startX, int startY)
        {
            int counterX = startX;
            int counterY = startY;
            int boxSize = 600 / order;

            for (int i = 0; i <= order; i++)
            {
                graphics.DrawLine(normalPen, counterX, startY, counterX, startY + 600);
                counterX += boxSize;
            }
            for (int j = 0; j <= order; j++)
            {
                graphics.DrawLine(normalPen, startX, counterY, startX + 600, counterY);
                counterY += boxSize;
            }
        }
        private void drawColorBoxes()
        {
            List<Figure> figures = sudoku.getFiguresList();
            int boxSize = 600 / order;
            foreach (Figure figure in figures)
            {
                List<Dot> dots = figure.getDots();
                SolidBrush drawBrush = new SolidBrush(figure.getColor());
                foreach (Dot dot in dots)
                {
                    int i = dot.getI() * boxSize;
                    int j = dot.getJ() * boxSize;
                    graphics.FillRectangle(drawBrush, x1 + i + 1, y1 + j + 1, boxSize - 1, boxSize - 1);
                }
            }
        }
        private void drawFiguresEdges(int startX, int startY)
        {
            List<Figure> figures = sudoku.getFiguresList();
            int boxSize = 600 / order;
            foreach (Figure figure in figures)
            {
                List<Dot> dots = figure.getDots();
                foreach (Dot dot in dots)
                {
                    int i = dot.getI() * boxSize;
                    int j = dot.getJ() * boxSize;
                    int shape = dot.getShape();
                    Pen bordersPen = new Pen(Color.Black, 3);
                    int x = startX + x1 + i + 1;
                    int y = startY + y1 + j + 1;
                    switch (shape)
                    {
                        case 1:
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            graphics.DrawLine(bordersPen, x, y, x, y + boxSize);//Horizontal Up
                            graphics.DrawLine(bordersPen, x, y + boxSize, x + boxSize, y + boxSize);//Vertical Right
                            break;
                        case 2:
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            graphics.DrawLine(bordersPen, x + boxSize, y, x + boxSize, y + boxSize);//Horizontal Down
                            graphics.DrawLine(bordersPen, x, y + boxSize, x + boxSize, y + boxSize);//Vertical Right
                            break;
                        case 3:
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            graphics.DrawLine(bordersPen, x, y, x, y + boxSize);//Horizontal Up
                            graphics.DrawLine(bordersPen, x + boxSize, y, x + boxSize, y + boxSize);//Horizontal Down
                            break;
                        case 4:
                            graphics.DrawLine(bordersPen, x, y + boxSize, x + boxSize, y + boxSize);//Vertical Right
                            graphics.DrawLine(bordersPen, x, y, x, y + boxSize);//Horizontal Up
                            graphics.DrawLine(bordersPen, x + boxSize, y, x + boxSize, y + boxSize);//Horizontal Down
                            break;
                        case 5:
                            graphics.DrawLine(bordersPen, x, y, x, y + boxSize);//Horizontal Up
                            graphics.DrawLine(bordersPen, x + boxSize, y, x + boxSize, y + boxSize);//Horizontal Down
                            break;
                        case 6:
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            graphics.DrawLine(bordersPen, x, y + boxSize, x + boxSize, y + boxSize);//Vertical Right
                            break;
                        case 11:
                            graphics.DrawLine(bordersPen, x, y, x, y + boxSize);//Horizontal Up
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            break;
                        case 12:
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            graphics.DrawLine(bordersPen, x + boxSize, y, x + boxSize, y + boxSize);//Horizontal Down
                            break;
                        case 13:
                            graphics.DrawLine(bordersPen, x, y, x, y + boxSize);//Horizontal Up
                            graphics.DrawLine(bordersPen, x, y + boxSize, x + boxSize, y + boxSize);//Vertical Right
                            break;
                        case 14:
                            graphics.DrawLine(bordersPen, x, y + boxSize, x + boxSize, y + boxSize);//Vertical Right
                            graphics.DrawLine(bordersPen, x + boxSize, y, x + boxSize, y + boxSize);//Horizontal Down
                            break;
                        case 15:
                            graphics.DrawLine(bordersPen, x + boxSize, y, x + boxSize, y + boxSize);//Horizontal Down
                            break;
                        case 16:
                            graphics.DrawLine(bordersPen, x, y + boxSize, x + boxSize, y + boxSize);//Vertical Right
                            break;
                        case 17:
                            graphics.DrawLine(bordersPen, x, y, x, y + boxSize);//Horizontal Up
                            break;
                        case 18:
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            break;
                        case 19:
                            graphics.DrawLine(bordersPen, x, y, x, y + boxSize);//Horizontal Up
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            graphics.DrawLine(bordersPen, x, y + boxSize, x + boxSize, y + boxSize);//Vertical Right
                            graphics.DrawLine(bordersPen, x + boxSize, y, x + boxSize, y + boxSize);//Horizontal Down
                            break;
                    }
                }
            }
        }
        private void drawMatrixNumbers()
        {
            int boxSize = 600 / order;
            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    graphics.DrawString(sudoku.getPositionNumber(i, j) + "", new Font("Arial", (int)(boxSize / 2)), new SolidBrush(Color.Black), x1 + i * boxSize, y1 + j * boxSize + (int)(boxSize / 3));
                }
            }
        }
        private void drawOperations(Color color)
        {
            List<Figure> figures = sudoku.getFiguresList();
            int boxSize = 600 / order;
            foreach (Figure figure in figures)
            {
                List<Dot> dots = figure.getDots();
                foreach (Dot dot in dots)
                {
                    if (dot.verifyFirst() == true)
                    {
                        int i = (dot.getI() * boxSize);
                        int j = (dot.getJ() * boxSize);
                        graphics.DrawString(figure.getResult() + "" + figure.getOperation(), new Font("Arial", 25 - order), new SolidBrush(color), 2 + i, 2 + j);
                    }
                }
            }
        }
        private void drawResultInitialNumbers(Color color)
        {
            int boxSize = 600 / order;
            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    int number = sudoku2.getPositionNumber(i, j);
                    if (number != 0)
                    {
                        graphics.DrawString(number + "", new Font("Arial", (int)(boxSize / 2)), new SolidBrush(color), x2 + i * boxSize, j * boxSize + (int)(boxSize / 3));
                    }

                }
            }
        }
        public void updateNumber(int number, int i, int j)
        {
            Graphics graphs = CreateGraphics();
            int boxSize = 600 / order;
            graphs.FillRectangle(new SolidBrush(Color.White), x2 + 3 + i * boxSize, 3 + j * boxSize, boxSize - 3, boxSize - 3);
            if (number != 0)
            {
                graphs.DrawString(number + "", new Font("Arial", (int)(boxSize / 2)), new SolidBrush(Color.Black), x2 + i * boxSize, j * boxSize + (int)(boxSize / 3));
            }

        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (solving)
            {
                solving = false;
                stopThreads();
            }
            generated = false;
            minutes = 0;
            seconds = 0;
            CleanPanel_1();
            CleanPanel_2();
            resetArray();
            graphics2.FillRectangle(new SolidBrush(Color.Black), new Rectangle(1205, 220, 150, 120));
            if (comboBox1.SelectedIndex > -1)
            {
                order = Int32.Parse(comboBox1.SelectedItem.ToString());
                sudoku = new Sudoku(this, order);
                sudoku2 = new EmptySudoku(this, order, sudoku.GetMatrix(), sudoku.getFiguresList(),sudoku);
                drawSquareLines(x1, y1);
                drawSquareLines(x2, y2);
                drawColorBoxes();
                drawFiguresEdges(0, 0);
                drawFiguresEdges(600, 0);
                drawMatrixNumbers();
                drawOperations(Color.Black);
                drawResultInitialNumbers(Color.Blue);
                generated = true;
            }
            else
            {
                generated = false;
                MessageBox.Show("You must select a size","Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
            }
        }
        public void showSudokuSolvedMessage()
        {
            MessageBox.Show("Sudoku solved successfully!!",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Txt Files(.txt)|*.txt";
            openFileDialog1.Title = "Load Sudoku";
            //First we have to be sure the user select the button save in the saveDialog
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {   //then check if the name the user enters is not empty
                string fileName = openFileDialog1.FileName;
                if (fileName != "")
                {
                    generated = false;
                    bool existingFile = System.IO.File.Exists(fileName);
                    if (existingFile == false)
                    {
                        MessageBox.Show("FileName not found", "FileName Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        openFileDialog1.Dispose();
                    }
                    CleanPanel_1();
                    CleanPanel_2();
                    resetArray();
                    graphics2.FillRectangle(new SolidBrush(Color.Black), new Rectangle(1205, 220, 150, 120));
                    fileSize(fileName);
                    sudoku = new Sudoku(this, order,true);
                    sudoku.LoadSudoku(fileName);
                    sudoku.LoadFigurates(fileName.Insert(fileName.IndexOf('.'), "_Figurates"));
                    sudoku.LoadPartial(fileName.Insert(fileName.IndexOf('.'), "_PartialMatrix"));
                    sudoku2 = new EmptySudoku(this, order, sudoku.GetMatrix(), sudoku.getFiguresList(),sudoku,true);
                    sudoku2.setPartialMatrix(sudoku.getPartialMatrix());
                    drawSquareLines(x1, y1);
                    drawSquareLines(x2, y2);
                    drawColorBoxes();
                    drawFiguresEdges(0, 0);
                    drawFiguresEdges(600, 0);
                    drawMatrixNumbers();
                    drawOperations(Color.Black);
                    drawResultInitialNumbers(Color.Blue);
                    generated = true;
                }
            }
            else
            {
                MessageBox.Show("Unable File name", "FileName Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                openFileDialog1.Dispose();
            }
        }
        private void fileSize(string path)
        {
            //System.IO.StreamReader fileR = new System.IO.StreamReader(path);
            StreamReader fileR = new StreamReader(path);
            int c = 0;
            while (!fileR.EndOfStream)
            {
                string textLine = fileR.ReadLine();//0,0,3,0,   
                c++;
            }
            order = c;
                //order = fileR.ReadLine().Length / 2;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog 
            // assigned to Button save (BtnSave)
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Txt Files(.txt)|.txt";
            saveFileDialog1.Title = "Save Sudoku";
            //saveFileDialog1.ShowDialog();
            //First we have to be sure the user select the button save in the saveDialog
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {   //then check if the name the user enters is not empty
                string fileName = saveFileDialog1.FileName;
                string fileName2 = saveFileDialog1.FileName.Insert(saveFileDialog1.FileName.IndexOf('.'), "_Figurates");
                if (fileName != "")
                {
                    bool existingFile = System.IO.File.Exists(fileName);
                    bool existingFile2 = System.IO.File.Exists(fileName2);
                    if (existingFile == true && existingFile2 == true)//pregunto si quiere sobreescribir, windows lo hace
                    {
                        System.IO.File.Delete(fileName);
                        System.IO.File.Delete(fileName2);
                        sudoku.SaveSudoku(fileName);
                        saveFileDialog1.Dispose();
                    }
                    sudoku.SaveSudoku(fileName);
                    saveFileDialog1.Dispose();
                }
                else
                {
                    MessageBox.Show("Unable File name", "FileName Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                saveFileDialog1.Dispose();
            }
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            if (isNumber(textBoxThreads.Text) == true)
            {
                solving = true;
                generated = false;
                graphics3.FillRectangle(new SolidBrush(Color.Black), new Rectangle(1205, 280, 150, 30));
                graphics3.DrawString("Comparations", new Font("Arial", 16), new SolidBrush(Color.Yellow), 1210, 290);
                sudoku2.setNoCompleted();
                counter = new Thread(timer);
                counter.Start();
                minutes = 0;
                seconds = 0;
                int n = Int32.Parse(textBoxThreads.Text);
                startThreads(n);
            }
            else
            {
                MessageBox.Show("You must select a valid number of threads", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }
        public void resetArray()
        {
            if (array != null)
            { 
                for (int k = 0; k < array.Length; k++)
                {
                    if (array[k] != null)
                    {
                        array[k].Abort();
                        array[k] = null;
                    }
                }
            }
        }
        public void setSolved()
        {
            solved = true;
            solving = false;
        }
        private bool isNumber(String n)
        {
            if (n != null)
            {
                int r;
                if(Int32.TryParse(n,out r))
                {
                    if (r>0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void timer()
        {
            while (true)
            {
                if (solving == true)
                {
                    updateTime(minutes, seconds);
                    seconds++;
                    if (seconds == 60)
                    {
                        seconds = 0;
                        minutes++;
                    }
                }
                Thread.Sleep(1000);
            }
        }
        public void stopThreads()
        {
            counter.Abort();
            for (int i = 0; i < array.Length; i++)
            {
                array[i].Abort();
            }
            seconds = 0;
            minutes = 0;
        }
        private void startThreads(int n)
        {
            array = new Thread[n];
            threads = n;
            for (int i = 0; i < n; i++)
            {
                array[i] = new Thread(sudoku2.solveSudoku);
                array[i].Start();
            }
        }
        public void updateThreadsAmount()
        {
            Graphics graphs = CreateGraphics();
            graphs.FillRectangle(new SolidBrush(Color.Black), new Rectangle(1205, 600, 150, 50));
            graphs.DrawString(threads+"", new Font("Arial", 24), new SolidBrush(Color.Yellow), 1220, 610);
        }
        private void s_Load(object sender, EventArgs e)
        {

        }

        private void textBoxThreads_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddThread_Click(object sender, EventArgs e)
        {
            updateThreadsAmount();
        }
    }
}
