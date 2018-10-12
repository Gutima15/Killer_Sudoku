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
        private Graphics graphics;
        private Pen normalPen;
        private SolidBrush backBrush;

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
            normalPen = new Pen(Color.Gray, 1);
            backBrush = new SolidBrush(Color.White);
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
            graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(1205,220,150,100));
        }
        public void updateGeneratingLabel(String word, String percentage)
        {
            graphics.DrawString(word, new Font("Arial", 16), new SolidBrush(Color.Yellow), 1210, 230);
            graphics.DrawString(percentage, new Font("Arial", 16), new SolidBrush(Color.Yellow), 1250, 260);
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
        private void drawFiguresEdges(int startX,int startY)
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
                    graphics.DrawString(sudoku.getPositionNumber(i, j) + "", new Font("Arial", 35 - order), new SolidBrush(Color.Black), x1 + 2 + i * boxSize, y1 + boxSize - (41 - order) + j * boxSize);
                }
            }
        }

        private void drawOperations(int xStart, Color color)
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
                        int i = (dot.getI() * boxSize) + xStart;
                        int j = (dot.getJ() * boxSize);
                        graphics.DrawString(figure.getOperation() +" ", new Font("Arial", 25-order), new SolidBrush(color), 2 + i , 2 + j +(25 - order));
                        graphics.DrawString(figure.getResult() + " ", new Font("Arial", 25 - order), new SolidBrush(color), 2 + i, 2 + j);
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
                    if(sudoku.getResultPositionNumber(i, j) != 0)
                    {
                        graphics.DrawString(sudoku.getResultPositionNumber(i, j) + "", new Font("Arial", 25 - order), new SolidBrush(color), x1 + 2 + i * boxSize + 600, y1 + boxSize - (31 - order) + j * boxSize);
                    }
                    
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            
            CleanPanel_1();
            CleanPanel_2();
            if (comboBox1.SelectedIndex > -1)
            {
                order = Int32.Parse(comboBox1.SelectedItem.ToString());
                sudoku = new Sudoku(this, order);
                drawSquareLines(x1, y1);
                drawSquareLines(x2, y2);
                drawColorBoxes();
                drawFiguresEdges(0, 0);
                drawFiguresEdges(600, 0);
                drawMatrixNumbers();
                drawOperations(0, Color.Black);
                drawOperations(600, Color.Black);
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
            //sudoku.SaveSudoku();
            
            //updateGeneratingLabel("Generating...", "0%");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image  
            // assigned to Button2.  
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Txt File|*.txt";
            saveFileDialog1.Title = "Save Sudoku";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.  
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the  
                // File type selected in the dialog box.  
                // NOTE that the FilterIndex property is one-based.  
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        //this.button2.Image.Save(fs,
                           //System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        //this.button2.Image.Save(fs,
                           //System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        //this.button2.Image.Save(fs,
                           //System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }
    }
}
