using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku matrixS = new Sudoku(5);
            matrixS.printSudoku();
            Console.Write(" \n");
            matrixS.GenerateBooleanMatriz();
            matrixS.printboolean();
            matrixS.saveSudoku();
            Console.ReadKey();
            
        }
    }
}
