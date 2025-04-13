using ShortestRouteOptimizer.Data;

namespace ShortestRouteOptimizer.Interface
{
    public interface IAlgorithm
    {
        ShortestPathData ShortestPath(string fromNodeName, string toNodeName, List<Node> graphNode);
        List<ShortestPathData> FindRoute(List<ShortestPathData> list, ShortestPathData data, string fromNodeName, string toNodeName, List<Node> graphNode);
        List<Node> GetNodes();
    }
}
