using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private Thread solver;
        private Thread counter;
        private int minutes;
        private int seconds;
        private bool solving;

        public s()
        {
            InitializeComponent();
            Screen screen = Screen.PrimaryScreen;
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
            solving = false;
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
            graphics.DrawString(word, new Font("Arial", 16), new SolidBrush(Color.Yellow), 1210, 230);
            graphics.DrawString(percentage, new Font("Arial", 16), new SolidBrush(Color.Yellow), 1250, 260);
        }
        
        public void updateTime(int minutes, int seconds)
        {
            CleanRightPanel();
            String mins = minutes + "";
            String secs = seconds + "";
            if (minutes < 10) { mins = "0" + mins; }
            if (seconds < 10) { secs = "0" + secs; }
            graphics2.DrawString("Solving...", new Font("Arial", 16), new SolidBrush(Color.Yellow), 1210, 230);
            graphics2.DrawString("Time: " + mins + ":" + secs, new Font("Arial", 16), new SolidBrush(Color.Yellow), 1210, 260);
        }
        public void updateComparations(Int64 n)
        {
            graphics3.FillRectangle(new SolidBrush(Color.Black), new Rectangle(1205, 310, 150, 30));
            graphics3.DrawString(n+"", new Font("Arial", 16), new SolidBrush(Color.Yellow), 1210, 320);
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
            //graphics.DrawString(figures.Count + "", new Font("Arial", 20), new SolidBrush(Color.Black), 600, 100);
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
            int boxSize = 600 / order;
            graphics.FillRectangle(new SolidBrush(Color.White), x2 + 3 + i * boxSize, 3 + j * boxSize, boxSize - 3, boxSize - 3);
            if (number != 0)
            {
                graphics.DrawString(number + "", new Font("Arial", (int)(boxSize / 2)), new SolidBrush(Color.Black), x2 + i * boxSize, j * boxSize + (int)(boxSize / 3));
            }

        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (solving)
            {
                solving = false;
                stopThreads();
            }
            minutes = 0;
            seconds = 0;
            CleanPanel_1();
            CleanPanel_2();
            graphics2.FillRectangle(new SolidBrush(Color.Black), new Rectangle(1205, 220, 150, 120));
            if (comboBox1.SelectedIndex > -1)
            {
                order = Int32.Parse(comboBox1.SelectedItem.ToString());
                sudoku = new Sudoku(this, order);
                sudoku2 = new EmptySudoku(this, order, sudoku.GetMatrix(), sudoku.getFiguresList());
                drawSquareLines(x1, y1);
                drawSquareLines(x2, y2);
                drawColorBoxes();
                drawFiguresEdges(0, 0);
                drawFiguresEdges(600, 0);
                drawMatrixNumbers();
                drawOperations(Color.Black);
                drawResultInitialNumbers(Color.Blue);
            }
            else
            {
                MessageBox.Show("You must select a size",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
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
                {   //Also, if the file exists to prevent the not existance of the file.                    
                    bool existingFile = System.IO.File.Exists(fileName);
                    if (existingFile == false)
                    {
                        MessageBox.Show("FileName not found", "FileName Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        openFileDialog1.Dispose();
                    }
                    CleanPanel_1();
                    CleanPanel_2();
                    graphics2.FillRectangle(new SolidBrush(Color.Black), new Rectangle(1205, 220, 150, 120));
                    fileSize(fileName);
                    sudoku = new Sudoku(this, order);
                    sudoku.LoadSudoku(fileName);
                    sudoku.LoadFigurates(fileName.Insert(fileName.IndexOf('.'), "_Figurates"));
                    sudoku2 = new EmptySudoku(this, order, sudoku.GetMatrix(), sudoku.getFiguresList());
                    drawSquareLines(x1, y1);
                    drawSquareLines(x2, y2);
                    drawColorBoxes();
                    drawFiguresEdges(0, 0);
                    drawFiguresEdges(600, 0);
                    drawMatrixNumbers();
                    drawOperations(Color.Black);
                    drawResultInitialNumbers(Color.Blue);
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
            System.IO.StreamReader fileR = new System.IO.StreamReader(path);
            order = fileR.ReadLine().Length / 2;
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
            graphics3.FillRectangle(new SolidBrush(Color.Black), new Rectangle(1205, 280, 150, 30));
            graphics3.DrawString("Comparations", new Font("Arial", 16), new SolidBrush(Color.Yellow), 1210, 290);
            solver = new Thread(sudoku2.solveSudoku);
            counter = new Thread(timer);
            sudoku2.setNoCompleted();
            minutes = 0;
            seconds = 0;
            solving = true;
            solver.Start();
            counter.Start();
        }
        private void timer()
        {
            while (true)
            {
                updateTime(minutes, seconds);
                seconds++;
                if (seconds == 60)
                {
                    seconds = 0;
                    minutes++;
                }
                Thread.Sleep(1000);
            }
        }
        public void stopThreads()
        {
            counter.Suspend();
            solver.Suspend();
            seconds = 0;
            minutes = 0;
        }
    }
}
