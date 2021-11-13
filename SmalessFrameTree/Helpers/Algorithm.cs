using Models;
using System;

namespace Helpers
{
    public class Algorithm
    {
        private Edge[] T;
        private int nT;
        private bool[] marked;
        int NO_PARENT = -1;
        int UNLIMITED_VALUE = int.MaxValue;

        public void Prim(AdjacencyMatrix graph, int source)
        {
            T = new Edge[graph.N - 1];
            nT = 0;
            marked = new bool[graph.N];
            for (int i = 0; i < graph.N; i++)
                marked[i] = false;

            marked[source] = true;

            while (nT < graph.N - 1)
            {
                Edge edgeMin = new Edge();
                int nMinWeight = -1;
                int isVistedEndVertice = -1;

                for (int w = 0; w < graph.N; w++)
                {
                    if (!marked[w])
                    {
                        for (int v = 0; v < graph.N; v++)
                        {
                            if (marked[v] && graph.Array[v, w] > 0)
                            {
                                if (nMinWeight < 0 || graph.Array[v, w] < nMinWeight)
                                {
                                    edgeMin.V = v;
                                    edgeMin.W = w;
                                    edgeMin.Weight = graph.Array[v, w];
                                    isVistedEndVertice = w;
                                    nMinWeight = graph.Array[v, w];
                                }
                            }
                        }
                    }
                }

                marked[isVistedEndVertice] = true;
                T[nT++] = edgeMin;
            }

            Helper.PrintAlgorithm(T);
        }

        public void Kruskal(AdjacencyMatrix graph)
        {
            Edge[] edges = Helper.InitEdgeList(graph);
            Helper.Sort(edges);
            int verticesCount = graph.N;
            T = new Edge[verticesCount];
            int i = 0;
            int e = 0;

            Subset[] subsets = new Subset[verticesCount];
            for (int v = 0; v < verticesCount; ++v)
            {
                subsets[v].Parent = v;
                subsets[v].Rank = 0;
            }

            while (e < verticesCount - 1)
            {
                Edge nextEdge = edges[i++];
                int x = Helper.Find(subsets, nextEdge.V);
                int y = Helper.Find(subsets, nextEdge.W);

                if (x != y)
                {
                    T[e++] = nextEdge;
                    Helper.Union(subsets, x, y);
                }
            }

            Helper.PrintAlgorithm(T);
        }

        public void Dijkstra(AdjacencyMatrix adjacencyMatrix, int source)
        {
            int nVertices = adjacencyMatrix.N;
            int[] shortestDistances = new int[nVertices];
            bool[] processed = new bool[nVertices];

            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                shortestDistances[vertexIndex] = UNLIMITED_VALUE;
                processed[vertexIndex] = false;
            }

            shortestDistances[source] = 0;
            int[] parents = new int[nVertices];
            parents[source] = NO_PARENT;

            for (int i = 1; i < nVertices; i++)
            {
                int nearestVertex = -1;
                int shortestDistance = UNLIMITED_VALUE;
                for (int vIndex = 0; vIndex < nVertices; vIndex++)
                {
                    if (!processed[vIndex] && shortestDistances[vIndex] <= shortestDistance)
                    {
                        nearestVertex = vIndex;
                        shortestDistance = shortestDistances[vIndex];
                    }
                }

                processed[nearestVertex] = true;

                for (int v = 0; v < nVertices; v++)
                {
                    int edgeDistance = adjacencyMatrix.Array[nearestVertex, v];

                    if (edgeDistance > 0 && ((shortestDistance + edgeDistance) < shortestDistances[v]))
                    {
                        parents[v] = nearestVertex;
                        shortestDistances[v] = shortestDistance + edgeDistance;
                    }
                }
            }

            Helper.PrintAllSolutions(source, shortestDistances, parents);
        }

        public void BellmanFord(AdjacencyMatrix graph, int source)
        {
            int nVertices = graph.N;
            Edge[] edges = Helper.InitEdgeList(graph);
            int edgesCount = edges.Length;
            int[] shortestDistances = new int[nVertices];

            for (int i = 0; i < nVertices; i++)
                shortestDistances[i] = UNLIMITED_VALUE;

            shortestDistances[source] = 0;
            int[] parents = new int[nVertices];
            parents[source] = NO_PARENT;

            for (int i = 1; i <= nVertices - 1; ++i)
            {
                for (int j = 0; j < edgesCount; ++j)
                {
                    int u = edges[j].V;
                    int v = edges[j].W;
                    int weight = edges[j].Weight;

                    if (shortestDistances[u] != UNLIMITED_VALUE && shortestDistances[u] + weight < shortestDistances[v])
                    {
                        shortestDistances[v] = shortestDistances[u] + weight;
                        parents[v] = u;
                    }
                }
            }

            for (int i = 0; i < edgesCount; ++i)
            {
                int u = edges[i].V;
                int v = edges[i].W;
                int weight = edges[i].Weight;

                if (shortestDistances[u] != UNLIMITED_VALUE && shortestDistances[u] + weight < shortestDistances[v])
                {
                    Console.WriteLine("Do thi co mach am.");
                    return;
                }
            }

            Helper.PrintAllSolutions(source, shortestDistances, parents);
        }
    }
}
