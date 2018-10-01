using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Figure
    {
        private Sudoku matrix;
        private List<int> figureList; 

        public Figure(Sudoku matrix)
        {
            this.matrix = matrix;
            figureList = new List<int>();
            //matrix.randomFill();
            //matrix.print(0);
            
        }

        public Sudoku GetMatrix()
        {
            return matrix;
        }
        /* Function that generates the figure l, randomly in the 4 ways of rotation.
         * parameters: int i and int j to indicate the position over the matrix to start the figure,
         * The figure is added to the list of figures.
         */
        public void generate_L(int i, int j)
        {
            Random random = new Random();
            int rand = random.Next(0, 4);
            if (rand == 0)
            {

            }
            else if(rand == 1)
            {

            }
            else if(rand == 2)
            {

            }
            else
            {
               
            }
        }

        public void generate_Inverted_L(int i, int j)
        {
            Random random = new Random();
            int rand = random.Next(0, 4);
            if (rand == 0)
            {

            }
            else if (rand == 1)
            {

            }
            else if (rand == 2)
            {

            }
            else
            {

            }
        }

        public void generate_s(int i, int j)
        {
            Random random = new Random();
            int rand = random.Next(0, 4);
            if (rand == 0)
            {

            }
            else if (rand == 1)
            {

            }
            else if (rand == 2)
            {

            }
            else
            {

            }
        }

        public void generate_Inverted_s(int i, int j)
        {
            Random random = new Random();
            int rand = random.Next(0, 4);
            if (rand == 0)
            {

            }
            else if (rand == 1)
            {

            }
            else if (rand == 2)
            {

            }
            else
            {

            }
        }
        public void generate_t(int i, int j)
        {
            Random random = new Random();
            int rand = random.Next(0, 4);
            if (rand == 0)
            {

            }
            else if (rand == 1)
            {

            }
            else if (rand == 2)
            {

            }
            else
            {

            }
        }

        public void generate_l(int i, int j)
        {
            Random random = new Random();
            int rand = random.Next(0, 4);
            if (rand == 0)
            {

            }
            else if (rand == 1)
            {

            }
            else if (rand == 2)
            {

            }
            else
            {

            }
        }

        public void generate_square(int i, int j)
        {
            Random random = new Random();
            int rand = random.Next(0, 4);
            if (rand == 0)
            {

            }
            else if (rand == 1)
            {

            }
            else if (rand == 2)
            {

            }
            else
            {

            }
        }
    }
}
