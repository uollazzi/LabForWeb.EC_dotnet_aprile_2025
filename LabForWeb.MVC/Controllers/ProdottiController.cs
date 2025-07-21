using LabForWeb.MVC.Data;
using LabForWeb.MVC.Extensions;
using LabForWeb.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IActionResult> GetFakeData()
    {
        var client = new HttpClient();

        var response = await client.GetFromJsonAsync<DummyJsonProductsResponse>("https://dummyjson.com/products");

        foreach (var p in response.Products)
        {
            _context.Prodotti.Add(new Data.Models.Prodotto
            {
                Attivo = true,
                Descrizione = p.Description,
                DescrizioneBreve = p.Description,
                Giacenza = (short)p.Stock,
                ImageUrl = p.Thumbnail,
                Nome = p.Title,
                Prezzo = (decimal)p.Price,
                Visibile = true
            });
        }

        _context.SaveChanges();

        return View(await _context.Prodotti.Select(s => s.ToProdottoModel()).ToListAsync());
    }
}
