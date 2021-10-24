namespace Models
{
    public class AdjacencyMatrix
    {
        public AdjacencyMatrix(int n, int[,] amArray)
        {
            N = n;
            Array = amArray;
        }

        public int N { get; set; }
        public int[,] Array { get; set; }
    }

    public struct Subset
    {
        public int Parent;
        public int Rank;
    }
}
