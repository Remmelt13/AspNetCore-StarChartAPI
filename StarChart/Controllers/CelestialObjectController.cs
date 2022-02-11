using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

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

            var celestial = _context.CelestialObjects.Where(c => c.Id == id).FirstOrDefault();
            celestial.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == id).ToList();

            return Ok(celestial);

        }
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            // Check if the object exists
            if (!_context.CelestialObjects.Any(c => c.Name == name))
            {
                return NotFound();
            }

            var celestials = _context.CelestialObjects.Where(c => c.Name == name).ToList();
            foreach (var celestial in celestials)
            {
                celestial.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == celestial.Id).ToList();
            }

            return Ok(celestials);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestials = _context.CelestialObjects.ToList();
            foreach(var celestial in celestials)
            {
                celestial.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == celestial.Id).ToList();
            }
            return Ok(celestials);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] CelestialObject celestial)
        {
            _context.CelestialObjects.Add(celestial);
            _context.SaveChanges();
            return CreatedAtRoute("GetById" ,new { id = celestial.Id}, celestial);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CelestialObject celestialObject)
        {
            var celestial = _context.CelestialObjects.Find(id);
            if (celestial == null)
            {
                return NotFound();
            }
            celestial.Name = celestialObject.Name;
            celestial.OrbitedObjectId = celestialObject.OrbitedObjectId;
            celestial.OrbitalPeriod = celestialObject.OrbitalPeriod;

            _context.CelestialObjects.Update(celestial);
            _context.SaveChanges();



            return NoContent();
        }

        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id, string name)
        {
            var celestial = _context.CelestialObjects.Find(id);
            if (celestial == null)
            {
                return NotFound();
            }
            celestial.Name = name;

            _context.CelestialObjects.Update(celestial);
            _context.SaveChanges();



            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_context.CelestialObjects.Any(c => c.Id == id))
            {
                return NotFound();
            }

            _context.CelestialObjects.RemoveRange(_context.CelestialObjects.Where(c => c.Id == id));
            _context.SaveChanges();


            return NoContent();

        }


    }
}
