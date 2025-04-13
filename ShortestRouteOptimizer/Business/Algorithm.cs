using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShortestRouteOptimizer.Data;
using ShortestRouteOptimizer.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortestRouteOptimizer.Business
{
    public class Algorithm : IAlgorithm
    {
        public ShortestPathData ShortestPath(string fromNodeName, string toNodeName, List<Node> graphNode)
        {
            List<ShortestPathData> shortestPathDataList = new List<ShortestPathData>();
            bool isfromNodeExist = NodeExists(fromNodeName, graphNode);
            bool istoNodeExist = NodeExists(toNodeName, graphNode);
            if (isfromNodeExist && istoNodeExist)
            {
                shortestPathDataList = FindRoute(shortestPathDataList, new ShortestPathData(), fromNodeName, toNodeName, graphNode);
            }
            else
            {
                string link = !isfromNodeExist && !istoNodeExist == true ? " and " : !isfromNodeExist ? fromNodeName : toNodeName;
                return (new ShortestPathData
                {
                    NodeNames = new List<string>(),
                    Distance = 0,
                    ErrorMessage = $"Node {link} does not exist in the graph."
                });
            }

            var shortestDistance = shortestPathDataList.Where(a => a.Distance == shortestPathDataList.Min(x => x.Distance)).ToList();
            var NodeLengths = shortestDistance.Select(a => a.NodeNames.Count()).ToList();
            var shortestNodeLength = shortestDistance.Where(a => a.NodeNames.Count() == NodeLengths.Min()).First();
            return shortestNodeLength;
        }

        public List<ShortestPathData> FindRoute(List<ShortestPathData> list, ShortestPathData data, string fromNodeName, string toNodeName, List<Node> graphNode)
        {
            //Console.WriteLine("Check");
            var resultList = list ?? new List<ShortestPathData>();

            // Clone the current data to avoid mutating the shared data
            var newPath = new ShortestPathData
            {
                NodeNames = new List<string>(data.NodeNames),
                Distance = data.Distance
            };

            newPath.NodeNames.Add(fromNodeName);

            // Find next possible nodes
            var nodes = graphNode.Where(a => (!newPath.NodeNames.Contains(a.to) && a.from == fromNodeName) //non bidirectional
            || (!newPath.NodeNames.Contains(a.from) && a.to == fromNodeName && a.bidirectinal == true //bidirectional
            )).ToList();

            if (nodes != null && nodes.Count > 0 && fromNodeName != toNodeName)
            {
                foreach (var node in nodes)
                {
                    string nextNode = fromNodeName != node.to ? node.to : node.from;

                    // Clone again for each data
                    var clonedPath = new ShortestPathData
                    {
                        NodeNames = new List<string>(newPath.NodeNames),
                        Distance = newPath.Distance + node.distance
                    };

                    FindRoute(resultList, clonedPath, nextNode, toNodeName, graphNode);
                }
            }
            else if (fromNodeName == toNodeName)
            {
                resultList.Add(newPath);
            }

            return resultList;
        }

        public List<Node> GetNodes()
        {
            var graphNode = new List<Node>
            {
                new Node { from = "A", to = "B", distance = 4, bidirectinal = true },
                new Node { from = "A", to = "C", distance = 6, bidirectinal = true },
                new Node { from = "E", to = "B", distance = 2, bidirectinal = false },
                new Node { from = "D", to = "C", distance = 8, bidirectinal = true },
                new Node { from = "D", to = "G", distance = 1, bidirectinal = true },
                new Node { from = "E", to = "D", distance = 4, bidirectinal = true },
                new Node { from = "E", to = "F", distance = 3, bidirectinal = true },
                new Node { from = "E", to = "I", distance = 8, bidirectinal = true },
                new Node { from = "F", to = "B", distance = 2, bidirectinal = true },
                new Node { from = "F", to = "H", distance = 6, bidirectinal = true },
                new Node { from = "G", to = "E", distance = 4, bidirectinal = true },
                new Node { from = "G", to = "I", distance = 5, bidirectinal = true },
                new Node { from = "H", to = "G", distance = 5, bidirectinal = true },
            };
            return graphNode;
        }
        public bool NodeExists(string nodeName, List<Node> graphNode)
        {
            return graphNode.Any(node => node.from == nodeName || node.to == nodeName);
        }
    }
}
