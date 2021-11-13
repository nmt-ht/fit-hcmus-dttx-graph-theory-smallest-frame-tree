using Models;
using System;
using System.IO;
using System.Linq;

namespace Helpers
{
    public static class Helper
    {
        private static string PathString { get; set; } = string.Empty;
        public static AdjacencyMatrix InitAdjacencyMatrix(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);

                int n = int.Parse(lines[0]);
                int[,] arr = new int[n, n];

                for (int i = 0; i < n; i++)
                {
                    string[] row = lines[i + 1].Split(" ");

                    for (int j = 0; j < n; j++)
                    {
                        arr[i, j] = int.Parse(row[j]);
                    }
                }

                return new AdjacencyMatrix(n, arr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message.ToString());
                Console.ReadLine();
            }

            return null;
        }

        public static void PrintMatrixToScreen(AdjacencyMatrix adjacencyMatrix)
        {
            Console.WriteLine(adjacencyMatrix.N);

            for (int i = 0; i < adjacencyMatrix.N; i++)
            {
                for (int j = 0; j < adjacencyMatrix.N; j++)
                {
                    Console.Write(adjacencyMatrix.Array[i, j]);
                    Console.Write(" ");
                }

                Console.WriteLine("");
            }
        }

        public static void PrintAlgorithm(Edge[] T)
        {
            Console.WriteLine("Tap canh cua cay khung:");

            var totalWeight = 0;
            foreach (var eT in T)
            {
                if (eT != null)
                {
                    totalWeight += eT.Weight;
                    Console.WriteLine("{0} - {1}: {2}", eT.V, eT.W, eT.Weight);
                }
            }

            Console.WriteLine($"Trong so cua cay khung: {totalWeight}");
        }

        public static Edge[] InitEdgeList(AdjacencyMatrix graph)
        {
            Edge[] edges = new Edge[graph.N * (graph.N - 1)];
            Edge edge;
            int nEdgeCount = 0;

            for (int i = 0; i < graph.N; i++)
            {
                for (int j = 0; j < graph.N; j++)
                {
                    if (graph.Array[i, j] > 0 || graph.Array[i, j] < 0)
                    {
                        edge = new Edge();
                        edge.V = i;
                        edge.W = j;
                        edge.Weight = graph.Array[i, j];
                        edges[nEdgeCount] = edge;
                        nEdgeCount++;
                    }
                }
            }

            edges = edges.Where(e => e != null).ToArray<Edge>();
            return edges;
        }

        public static void Sort(Edge[] edges)
        {
            Array.Sort(edges, delegate (Edge a, Edge b)
            {
                return a.Weight.CompareTo(b.Weight);
            });
        }

        public static int Find(Subset[] subsets, int i)
        {
            if (subsets[i].Parent != i)
                subsets[i].Parent = Find(subsets, subsets[i].Parent);

            return subsets[i].Parent;
        }

        public static void Union(Subset[] subsets, int x, int y)
        {
            int xroot = Find(subsets, x);
            int yroot = Find(subsets, y);

            if (subsets[xroot].Rank < subsets[yroot].Rank)
                subsets[xroot].Parent = yroot;
            else if (subsets[xroot].Rank > subsets[yroot].Rank)
                subsets[yroot].Parent = xroot;
            else
            {
                subsets[yroot].Parent = xroot;
                ++subsets[xroot].Rank;
            }
        }

        public static void PrintAllSolutions(int source, int[] distances, int[] parents)
        {
            int nVertices = distances.Length;
            int maxValue = int.MaxValue;
            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                Console.Write($"Duong di ngan nhat tu {source} den {vertexIndex}: \t");
                if (maxValue == distances[vertexIndex])
                {
                    Console.Write("Khong co");
                    continue;
                }
                else { Console.Write($"{distances[vertexIndex]} \t"); }

                PrintPath(vertexIndex, parents);
                if (!string.IsNullOrEmpty(PathString))
                {
                    Console.Write(PathString.Substring(0, PathString.Length - 4));
                }
                else { Console.Write("Khong co duong di."); }
                Console.WriteLine();
            }
        }

        private static void PrintPath(int currentVertex, int[] parents)
        {
            PathString = string.Empty;
            int NO_PARENT = -1;
            if (currentVertex == NO_PARENT)
                return;

            PrintPath(parents[currentVertex], parents);
            PathString += currentVertex + " -> ";
        }
    }
}
