using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            // Check if the object exists
            if (!_context.CelestialObjects.Any(c => c.Id == id))
            {
                return NotFound();
            }
            return Ok(_context.CelestialObjects.Where(c => c.Id == id).FirstOrDefault());
        }
        [HttpGet("{name: string}")]
        public IActionResult GetByName(string name)
        {
            // Check if the object exists
            if (!_context.CelestialObjects.Any(c => c.Name == name))
            {
                return NotFound();
            }
            return Ok(_context.CelestialObjects.Where(c => c.Name == name).FirstOrDefault());
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.CelestialObjects);
        }
             
    }
}
