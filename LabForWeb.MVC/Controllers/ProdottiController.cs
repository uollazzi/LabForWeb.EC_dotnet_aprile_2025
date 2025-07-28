using LabForWeb.MVC.Data;
using LabForWeb.MVC.Extensions;
using LabForWeb.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;

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

        var prodotti = _context.Prodotti
            .OrderByDescending(x => x.Id)
            .Select(p => p.ToProdottoModel()).ToList();

        return View(prodotti);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categorie = await _context.Categorie.Select(c => c.ToCategoriaModel()).ToListAsync();
        var listItems = new SelectList(categorie, "Id", "Nome").ToList();

        ViewData["categorie"] = listItems;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProdottoDTO prodotto)
    {
        if (ModelState.IsValid)
        {
            // salvataggio su DB
            try
            {
                var nuovoProdotto = new Data.Models.Prodotto
                {
                    Nome = prodotto.Nome,
                    Descrizione = prodotto.Descrizione,
                    DescrizioneBreve = prodotto.DescrizioneBreve,
                    Giacenza = prodotto.Giacenza,
                    Prezzo = prodotto.Prezzo,
                    Attivo = prodotto.Attivo,
                    Visibile = prodotto.Visibile,
                };

                // salvo il file su disco
                if (prodotto.Immagine != null && prodotto.Immagine.Length > 0)
                {
                    var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    Directory.CreateDirectory(uploadsDir);

                    var fileName = Path.GetFileName(prodotto.Immagine.FileName); // frigo.jpg
                    var filePath = Path.Combine(uploadsDir, fileName);  // C:\Progetti\Tutorials\LAB4T\dotNET\dotnet_aprile_2025\LabForWeb.EC\LabForWeb.MVC\wwwroot\uploads\frigo.jpg                    
                    
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await prodotto.Immagine.CopyToAsync(stream);         

                    nuovoProdotto.ImageUrl = $"/uploads/{fileName}";
                }                

                // recupero il record categoria attraverso prodotto.CategoriaID
                var cat = await _context.Categorie.SingleAsync(c => c.Id == prodotto.CategoriaID);
                nuovoProdotto.Categorie.Add(cat);

                _context.Prodotti.Add(nuovoProdotto);

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(prodotto);
            }            
        }

        return View(prodotto);

    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var prodotto = await _context.Prodotti.FindAsync(id);
        if (prodotto == null) return NotFound();

        return View(prodotto.ToProdottoModel());
    }

    [HttpPost, ActionName("Delete")]    
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var prodotto = await _context.Prodotti.FindAsync(id);
        if (prodotto == null) return NotFound();

        try
        {
            if (!string.IsNullOrEmpty(prodotto.ImageUrl) && prodotto.ImageUrl.StartsWith("/uploads"))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", prodotto.ImageUrl[1..]);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Prodotti.Remove(prodotto);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);            
        }

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> GetFakeData()
    {
        if (_context.Prodotti.Any())
            return Ok(new { message = "Tutto già riempito" });

        var client = new HttpClient();

        var responseCategories = await client.GetFromJsonAsync<IEnumerable<DummyCategory>>("https://dummyjson.com/products/categories");

        foreach (var c in responseCategories)
        {
            _context.Categorie.Add(new Data.Models.Categoria
            {
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
