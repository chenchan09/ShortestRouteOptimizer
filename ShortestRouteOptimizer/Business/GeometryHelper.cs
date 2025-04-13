namespace ShortestRouteOptimizer.Business
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public static class GeometryHelper
    {
        public static (Point A, Point B) ShortenTowardCenter(double x1, double y1, double x2, double y2, double distance)
        {
            double dx = x1 - x2;
            double dy = y1 - y2;
            double length = Math.Sqrt(dx * dx + dy * dy);
            //distance = (length *2 ) - distance;
            if (distance * 2 > length)
                throw new ArgumentException("Distance too large — points would overlap or flip.");

            double ux = dx / length;
            double uy = dy / length;

            var newA = new Point
            {
                X = x1 + ux * distance,
                Y = y1 + uy * distance
            };

            var newB = new Point
            {
                X = x2 - ux * distance,
                Y = y2 - uy * distance
            };

            return (newA, newB);
        }
    }
    public class Vertex
    {
        public int X { get; set; }
        public int Y { get; set; }

    }
 
    public static class VertexHelper
    {
        public static List<Vertex> GenerateNonOverlappingVertices(int count, int width, int height, int minDistance)
        {
            Random rand = new Random();
            List<Vertex> vertices = new List<Vertex>();

            while (vertices.Count < count)
            {
                int x = rand.Next(0, width);
                int y = rand.Next(0, height);
                var newVertex = new Vertex { X = x, Y = y };

                bool isTooClose = vertices.Any(v =>
                    Math.Sqrt(Math.Pow(v.X - x, 2) + Math.Pow(v.Y - y, 2)) < minDistance
                );

                if (!isTooClose)
                {
                    vertices.Add(newVertex);
                }
            }

            return vertices;
        }
    }    
}
