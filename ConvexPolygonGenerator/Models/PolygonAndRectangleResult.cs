namespace ConvexPolygonGenerator.Models
{
    public class PolygonAndRectangleResult
    {
        public List<List<Point>> Polygons { get; set; }
        public List<Rectangle> Rectangles { get; set; }

        public PolygonAndRectangleResult()
        {
            Polygons = new List<List<Point>>();
            Rectangles = new List<Rectangle>();
        }
    }
}
