using Microsoft.AspNetCore.Mvc;

namespace StarChart.Controllers
{
    public class CelestialObjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
