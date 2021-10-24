using Helpers;
using System;

namespace SmalessFrameTree
{
    class Program
    {
        private const string ADJACENCY_MATRIX_INPUT_FILE = @".\Source\input.txt";
        static void Main(string[] args)
        {
            var matrix = Helper.InitAdjacencyMatrix(ADJACENCY_MATRIX_INPUT_FILE);
            Helper.PrintMatrixToScreen(matrix);
            Console.WriteLine();

            Console.WriteLine("*** PRIM'S Algorithm ***");
            Console.WriteLine("Please input start vertice: ");
            var startVertice = int.Parse(Console.ReadLine().ToString());
            Algorithm algorithm = new Algorithm();
            algorithm.Prim(matrix, startVertice);
            
            Console.WriteLine();
            Console.WriteLine("*** KRUSKAL'S Algorithm ***");
            algorithm.Kruskal(matrix);
            Console.ReadLine();
        }
    }
}
