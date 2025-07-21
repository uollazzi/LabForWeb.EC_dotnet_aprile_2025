using LabForWeb.MVC.Data;
using LabForWeb.MVC.Extensions;
using LabForWeb.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LabForWeb.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var prodotti = await _context.Prodotti.Select(p => p.ToProdottoModel()).ToListAsync();

        var model = new ProdottiGalleryPartialModel
        {
            Titolo = "Tutti i prodotti",
            Prodotti = prodotti
        };

        return View(model);
    }

    // GET /Home/Privacy
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
