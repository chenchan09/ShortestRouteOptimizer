using ShortestRouteOptimizer.Business;
using ShortestRouteOptimizer.Data;

namespace UnitTesting
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var algorithm = new Algorithm();
            var graphNode = algorithm.GetNodes();


            string fromNodeName = "A";
            string toNodeName = "D";
            // Act
            var result = algorithm.ShortestPath(fromNodeName, toNodeName, graphNode);
            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal([ "A", "B", "F", "E", "D" ], result.NodeNames);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var algorithm = new Algorithm();
            var graphNode = algorithm.GetNodes();


            string fromNodeName = "B";
            string toNodeName = "E";
            // Act
            var result = algorithm.ShortestPath(fromNodeName, toNodeName, graphNode);
            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(["B", "F", "E"], result.NodeNames);
        }
    }
}
