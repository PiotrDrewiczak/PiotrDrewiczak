using ConvexPolygonGenerator.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ConvexPolygonGenerator
{
    internal static class PythonRunner
    {
        public static PolygonAndRectangleResult Generate(string pythonPath, string pythonScriptPath, string polygonOutput,int numberOfVertices,int numberOfPolygons)
        {

            // Uruchom skrypt Pythonowy
            var start = new ProcessStartInfo()
            {
                FileName = pythonPath,
                Arguments = $"{pythonScriptPath} {numberOfPolygons} {numberOfVertices} \"{polygonOutput}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
        
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.WriteLine(result);
                }

                string error = process.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("ERROR:");
                    Console.WriteLine(error);
                }

                process.WaitForExit();
            }

            var polyResult = new PolygonAndRectangleResult();

            try
            {
                string json = File.ReadAllText(polygonOutput);
                dynamic data = JsonConvert.DeserializeObject<dynamic>(json);

                // Deserializuj wielokąty
                List<List<List<double>>> polygonPointsList = data.polygons.ToObject<List<List<List<double>>>>();
                polyResult.Polygons = polygonPointsList
                    .Select(polygonPoints => polygonPoints
                        .Select(pointList => new Point(pointList[0], pointList[1]))
                        .ToList())
                    .ToList();

                // Deserializuj prostokąty
                List<List<int>> rectangleList = data.rectangles.ToObject<List<List<int>>>();
                polyResult.Rectangles = rectangleList
                    .Select(rect => new Rectangle(rect[0], rect[1], rect[2], rect[3]))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd przy odczycie pliku: {ex.Message}");
            }
            return polyResult;
        }
    }
}
