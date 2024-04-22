using ConvexPolygonGenerator.Models;

namespace ConvexPolygonGenerator
{
    public class PolygonGenerator
    {
        private Random random;
        public PolygonGenerator()
        { 
            random = new Random();
        }
        public List<List<Point>> GenerateConvexPolygon(int numberOfPolygons,int numberOfVertices)
        {
            var polygon = new List<List<Point>>();
            for (int j = 0; j < numberOfPolygons; j++)
            {
                double centerX = random.Next(300)+ 100;
                double centerY = random.Next(100) + 100;
                double radius = random.Next(100) + 100;

                var vertices = new List<Point>();

                // Generowanie nieregularnie rozłożonych wierzchołków na okręgu
                for (int i = 0; i < numberOfVertices; i++)
                {
                    double angle = 2 * Math.PI * random.NextDouble(); // Losowy kąt
                    double distance = Math.Sqrt(random.NextDouble()) * radius; // Losowa odległość
                    double x = centerX + Math.Cos(angle) * distance; // Współrzędna x
                    double y = centerY + Math.Sin(angle) * distance; // Współrzędna y
                    vertices.Add(new Point(x, y));
                }
                // Sortowanie wierzchołków względem kąta
                vertices.Sort((a, b) =>
                {
                    double angleA = Math.Atan2(a.Y - centerY, a.X - centerX);
                    double angleB = Math.Atan2(b.Y - centerY, b.X - centerX);
                    return angleA.CompareTo(angleB);
                });

                polygon.Add(vertices);
            }
            SavePolygonsToCSV(polygon, "polygons.csv");
            return polygon;
        }

        // Zapisywanie wierzchołków wielokątów do pliku CSV

        public static void SavePolygonsToCSV(List<List<Point>> polygons, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var polygon in polygons)
                {
                    foreach (var point in polygon)
                    {
                        writer.WriteLine($"{point.X},{point.Y}");
                    }
                    // Oddzielenie kolejnych wielokątów pustym wierszem
                    writer.WriteLine();
                }
            }
        }
    }
    }



    

 
        
    