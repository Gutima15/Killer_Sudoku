using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sudoku matrixS = new Sudoku(5);
            //matrixS.printSudoku();
            //Console.Write(" \n");
            //matrixS.GenerateBooleanMatriz();
            //matrixS.printboolean();
            //matrixS.saveSudoku();
            //Console.WriteLine(isSubmultiple(288, 4));
            int[] array1 = new int[10];
            for(int i = 0; i<10; i++){
                array1[i] = 2;
            }
            int[] array2 = array1;
            array1[7] = 1;
            for (int j = 0; j < 10; j++)
            {
                Console.WriteLine(array1[j]+"  "+ array2[j]);
            }
            Console.ReadKey();
            
        }
        public static Boolean isSubmultiple(double result, double n)
        {
            int res1 = (int)(result / n);
            double res2 = result / n;
            Console.WriteLine(res1);
            Console.WriteLine(res2);
            if ((res2 - res1) == 0)
            {
                return true;
            }
            return false;
        }
    }
}
