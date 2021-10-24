using Models;
using System;

namespace Helpers
{
    public class Algorithm
    {
        private Edge[] T;
        private int nT;
        private bool[] marked;

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
                    if(!marked[w])
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
            int verticesCount = graph.N;
            Edge[] result = new Edge[verticesCount];
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
                    result[e++] = nextEdge;
                    Helper.Union(subsets, x, y);
                }
            }

            Helper.PrintAlgorithm(result);
        }
    }
}
