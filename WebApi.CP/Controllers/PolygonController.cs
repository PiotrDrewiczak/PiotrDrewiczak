using ConvexPolygonGenerator;
using ConvexPolygonGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.CP.Models;

namespace WebApi.CP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolygonController : ControllerBase
    {
        private IOptions<AppSettingsModel> settings;

        public PolygonController(IOptions<AppSettingsModel> settings)
        {
            this.settings = settings;
        }

        [HttpGet(Name = "GetPolygons")]
        public List<List<Point>> GetPolygons()
        {
            PolygonGenerator generator = new PolygonGenerator(
                settings.Value.PythonPath,
                settings.Value.PythonScriptPath,
                settings.Value.PolygonOutput,
                settings.Value.NumberOfVertices,
                settings.Value.NumberOfPolygons
                );

            
            var list = generator.GenerateConvexPolygon();

            return list;
        }
    }
}
