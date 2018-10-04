using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            x1 = 25;
            x2 = 550;
            y1 = 75;
            y2 = 75;
            SetBounds(0, 0, screen.Bounds.Width, screen.Bounds.Height);
            graphics = CreateGraphics();
            normalPen = new Pen(Color.Gray, 1);
            backBrush = new SolidBrush(Color.White);
            Paint += S_Paint;
        }

        private void S_Paint(object sender, PaintEventArgs e)
        {
            graphics.DrawRectangle(normalPen, new Rectangle(x1, y1, 500, 500));
            graphics.DrawRectangle(normalPen, new Rectangle(x2, y2, 500, 500));
            graphics.FillRectangle(backBrush, new Rectangle(x1+1, y1+1, 499, 499));
            graphics.FillRectangle(backBrush, new Rectangle(x2+1, y2+1, 499, 499));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        

        private void CleanGoalPanel()
        {
            graphics.DrawRectangle(normalPen, new Rectangle(x1, y1, 500, 500));
            graphics.DrawRectangle(normalPen, new Rectangle(x2, y2, 500, 500));
            graphics.FillRectangle(backBrush, new Rectangle(x1 + 1, y1 + 1, 499, 499));
            graphics.FillRectangle(backBrush, new Rectangle(x2 + 1, y2 + 1, 499, 499));
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            CleanGoalPanel();
            order = Int32.Parse(comboBox1.SelectedItem.ToString());
            sudoku = new Sudoku(order);
            int counterX = x1;
            int counterY = y1;
            int boxSize = 500 / order;
            int numberPosX = boxSize / 3;
            int numberPosY = boxSize / 3;

            for (int i = 0; i <= order; i++)
            {
                graphics.DrawLine(normalPen, counterX, y1, counterX, y1 + 500);
                counterX += boxSize;
            }
            for (int j = 0; j <= order; j++)
            {
                graphics.DrawLine(normalPen, x1, counterY, x1 + 500, counterY);
                counterY += boxSize;
            }
            List<Figure> figures = sudoku.getFiguresList();
            graphics.DrawString(figures.Count + "", new Font("Arial", 20), new SolidBrush(Color.Black), 600, 100);
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
            foreach (Figure figure in figures)
            {
                List<Dot> dots = figure.getDots();
                foreach (Dot dot in dots)
                {
                    int i = dot.getI() * boxSize;
                    int j = dot.getJ() * boxSize;
                    int shape = dot.getShape();
                    Pen bordersPen = new Pen(Color.Black, 3);
                    int x = x1 + i + 1;
                    int y = y1 + j + 1;
                    switch (shape)
                    {
                        case 1:
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            graphics.DrawLine(bordersPen, x, y, x, y + boxSize);//Horizontal Up
                            graphics.DrawLine(bordersPen, x, y + boxSize, x + boxSize, y + boxSize);//Vertical Right
                            break;
                        case 2:
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            graphics.DrawLine(bordersPen, x + boxSize, y, x + boxSize, y+ boxSize);//Horizontal Down
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
                        case 7:
                            graphics.DrawLine(bordersPen, x, y + boxSize, x + boxSize, y + boxSize);//Vertical Right
                            graphics.DrawLine(bordersPen, x + boxSize, y, x + boxSize, y + boxSize);//Horizontal Down
                            break;
                        case 8:
                            graphics.DrawLine(bordersPen, x + boxSize, y, x + boxSize, y + boxSize);//Horizontal Down
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            break;
                        case 9:
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            graphics.DrawLine(bordersPen, x, y, x, y + boxSize);//Horizontal Up
                            break;
                        case 10:
                            graphics.DrawLine(bordersPen, x, y, x, y + boxSize);//Horizontal Up
                            graphics.DrawLine(bordersPen, x, y + boxSize, x + boxSize, y + boxSize);//Vertical Right
                            break;
                        case 11:
                            graphics.DrawLine(bordersPen, x, y, x, y + boxSize);//Horizontal Up
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            //----
                            //----
                            break;
                        case 12:
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            graphics.DrawLine(bordersPen, x + boxSize, y, x + boxSize, y + boxSize);//Horizontal Down
                            //---
                            //---
                            break;
                        case 13:
                            graphics.DrawLine(bordersPen, x, y, x, y + boxSize);//Horizontal Up
                            graphics.DrawLine(bordersPen, x, y + boxSize, x + boxSize, y + boxSize);//Vertical Right
                            //-----
                            //-----
                            break;
                        case 14:
                            graphics.DrawLine(bordersPen, x, y + boxSize, x + boxSize, y + boxSize);//Vertical Right
                            graphics.DrawLine(bordersPen, x + boxSize, y, x + boxSize, y + boxSize);//Horizontal Down
                            //----
                            //----
                            break;
                        case 15:
                            graphics.DrawLine(bordersPen, x + boxSize, y, x + boxSize, y + boxSize);//Horizontal Down
                            //----
                            //----
                            //----
                            //----
                            break;
                        case 16:
                            graphics.DrawLine(bordersPen, x, y + boxSize, x + boxSize, y + boxSize);//Vertical Right
                            //----
                            //----
                            //----
                            //----
                            break;
                        case 17:
                            graphics.DrawLine(bordersPen, x, y, x, y + boxSize);//Horizontal Up
                            //----
                            //----
                            //----
                            //----
                            break;
                        case 18:
                            graphics.DrawLine(bordersPen, x, y, x + boxSize, y);//Vertical Left
                            //----
                            //----
                            //----
                            //----
                            break;
                    }
                }
            }


            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    graphics.DrawString(sudoku.getPositionNumber(i, j) + "", new Font("Arial", 10), new SolidBrush(Color.Black), x1 + i * boxSize + numberPosX, y1 + j * boxSize + numberPosY);
                }
            }
        }
    }
}
