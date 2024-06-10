using ConvexPolygonGenerator.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ConvexPolygonGenerator
{
    internal static class PythonRunner
    {
        public static List<List<Point>> Generate(string pythonPath, string pythonScriptPath, string polygonOutput,int numberOfVertices,int numberOfPolygons)
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

            var polygons = new List<List<Point>>();

            try
            {
                string json = File.ReadAllText(polygonOutput);
                // Deserializuj zawartość pliku JSON jako tablicę obiektów Point
                List<List<List<double>>> polygonPointsList = JsonConvert.DeserializeObject<List<List<List<double>>>>(json);

                // Zmapuj listę list double na listę obiektów Point
                polygons = polygonPointsList
                  .Select(polygonPoints => polygonPoints
                      .Select(pointList => new Point(pointList[0], pointList[1]))
                      .ToList())
                  .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd przy odczycie pliku: {ex.Message}");
            }
            return polygons;
        }
    }
}
