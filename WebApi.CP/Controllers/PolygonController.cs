using ConvexPolygonGenerator;
using ConvexPolygonGenerator.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.CP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolygonController : ControllerBase
    {
        
        [HttpGet(Name = "GetPolygons")]
        public List<List<Point>> GetPolygons()
        {
            PolygonGenerator generator = new PolygonGenerator();
            var list = generator.GenerateConvexPolygon(10, 5);

            return list;
        }
    }
}
