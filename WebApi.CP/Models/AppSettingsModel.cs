namespace WebApi.CP.Models
{
    public class AppSettingsModel
    {
        public const string ApplicationSettings = "ApplicationSettings";

        public string PythonPath { get; set; }
        public string PythonScriptPath { get; set; }
        public string PolygonOutput { get; set; }
        public int NumberOfPolygons { get; set; }
        public int NumberOfVertices { get; set; }
    }
}
