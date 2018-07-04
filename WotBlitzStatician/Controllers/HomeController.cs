using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WotBlitzStatician.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Spa()
        {
          return File("~/index.html", "text/html");
        }
    }
}
