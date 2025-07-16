using LabForWeb.MVC.Data;
using LabForWeb.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabForWeb.MVC.Controllers;

public class ProdottiController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProdottiController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        //// dictionary CodiceFiscale => Nome
        //var users = new Dictionary<string, string>();
        //users.Add("XYZ", "Matteo");
        //users.Add("BLL", "Bullo");

        //var matteo = users["XYZ"];
        //users["XYZ"] = "Franco";

        ViewData["Messaggio"] = "Ciao sono io!";

        
        // POCO class
        List<ProdottoModel> prodotti = [
            new ProdottoModel{
                Id = 1,
                Nome = "Ciabatte col pelo"
            },
            new ProdottoModel{
                Id = 2,
                Nome = "Bicicletta"
            }
        ];

        return View(prodotti);
    }
}
