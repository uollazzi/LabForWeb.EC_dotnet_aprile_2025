using LabForWeb.MVC.Data;
using LabForWeb.MVC.Extensions;
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



        //List<ProdottoModel> prodotti = [
        //    new ProdottoModel{
        //        Id = 1,
        //        Nome = "Ciabatte col pelo"
        //    },
        //    new ProdottoModel{
        //        Id = 2,
        //        Nome = "Bicicletta"
        //    }
        //];

        var prodotti = _context.Prodotti.Select(p => p.ToProdottoModel()).ToList();

        return View(prodotti);
    }
}
