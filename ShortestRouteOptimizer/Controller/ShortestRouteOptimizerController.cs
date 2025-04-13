using Microsoft.AspNetCore.Mvc;
using ShortestRouteOptimizer.Interface;

namespace ShortestRouteOptimizer
{
    public class ShortestRouteOptimizerController : Controller
    {
        private readonly IAlgorithm _algorithm;
        public ShortestRouteOptimizerController(IAlgorithm algorithm)
        {
            _algorithm = algorithm;
            // Constructor logic can be added here if needed
        }
        public IActionResult Index(string from, string to)
        {
            var graphNode = _algorithm.GetNodes();
            var result = _algorithm.ShortestPath(from, to, graphNode);
            ViewBag.Nodes = graphNode;
            ViewBag.from = from == null ? "" : from.Trim();
            ViewBag.to = to == null ? "" : to.Trim();
            return View(result);
        }
    }
}
