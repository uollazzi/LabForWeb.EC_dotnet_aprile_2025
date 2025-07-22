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
        if (_context.Prodotti.Any())
            return Ok(new { message = "Tutto già riempito" });

        var client = new HttpClient();

        var responseCategories = await client.GetFromJsonAsync<IEnumerable<DummyCategory>>("https://dummyjson.com/products/categories");

        foreach (var c in responseCategories)
        {
            _context.Categorie.Add(new Data.Models.Categoria { 
                Nome = c.Name,
                Slug = c.Slug,
            });
        }        

        await _context.SaveChangesAsync();

        var responseProducts = await client.GetFromJsonAsync<DummyJsonProductsResponse>("https://dummyjson.com/products");

        foreach (var p in responseProducts.Products)
        {
            var prodotto = new Data.Models.Prodotto
            {
                Attivo = true,
                Descrizione = p.Description,
                DescrizioneBreve = p.Description,
                Giacenza = (short)p.Stock,
                ImageUrl = p.Thumbnail,
                Nome = p.Title,
                Prezzo = (decimal)p.Price,
                Visibile = true,
            };

            // cerco la categoria che ha come Slug p.Category
            var cat = _context.Categorie.Single(c => c.Slug == p.Category);

            // la aggiungo alle categoria
            prodotto.Categorie.Add(cat);

            _context.Prodotti.Add(prodotto);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return Ok(await _context.Prodotti.Select(s => s.ToProdottoModel()).ToListAsync());
    }
}
