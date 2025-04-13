namespace ShortestRouteOptimizer.Data
{
    public class ShortestPathData
    {
        public ShortestPathData()
        {
            NodeNames = new List<string>();
            Distance = 0;
        }
        public List<string> NodeNames { get; set; }
        public int Distance { get; set; }
        public string ErrorMessage { get; set; }
    }
}
