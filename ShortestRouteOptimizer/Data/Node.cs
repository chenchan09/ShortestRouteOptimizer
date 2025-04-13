namespace ShortestRouteOptimizer.Data
{
    public class Node
    {
        public string from {get; set; }
        public string to { get; set; }
        public int distance { get; set; }
        public bool bidirectinal { get; set; }
    }
}
