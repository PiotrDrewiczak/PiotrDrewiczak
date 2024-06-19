using ConvexPolygonGenerator.Models;


namespace ConvexPolygonGenerator
{
    public class PolygonGenerator
    {
        private string _pythonPath;
        private string _pythonScriptPath;
        private string _polygonOutput;
        private int _numberOfVertices;
        private int _numberOfPolygons;

        public PolygonGenerator(string pythonPath,string pythonScriptPath,string polygonOutput,int numberOfVertices,int numberOfPolygons)
        {
           this._pythonPath = pythonPath;
           this._pythonScriptPath = pythonScriptPath;
           this._polygonOutput = polygonOutput;
           this._numberOfVertices = numberOfVertices;
           this._numberOfPolygons = numberOfPolygons;
        }

        public PolygonAndRectangleResult GenerateConvexPolygon()
        {
            var polygonsList = PythonRunner.Generate(_pythonPath, _pythonScriptPath, _polygonOutput,_numberOfPolygons,_numberOfVertices);

            SavePolygonsToCSV(polygonsList.Polygons, "polygons.csv");
            return polygonsList;
        }

        // Zapisywanie wierzchołków wielokątów do pliku CSV

        public void SavePolygonsToCSV(List<List<Point>> polygons, string filePath)
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



    

 
        
    