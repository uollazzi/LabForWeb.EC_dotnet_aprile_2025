using Microsoft.AspNetCore.Mvc;

namespace LabForWeb.MVC.Controllers
{
    public class CarrelloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(int prodottoId)
        {
            // scrittura DB



            return RedirectToAction("Index", "Home");
        }
    }
}
