using LabForWeb.MVC.Data;
using LabForWeb.MVC.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabForWeb.MVC.Controllers
{
    public class CarrelloController : Controller
    {
        private readonly ApplicationDbContext _dc;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CarrelloController(ApplicationDbContext dc, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _dc = dc;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(int prodottoId)
        {
            // scrittura DB

            // 1. recupero l'utente loggato
            var user = await _userManager.GetUserAsync(User);

            // 2. cerco se esiste già un carrello NON chiuso dell'utente loggato
            // Include => carica in autonomia tutti i carrelliDettagli in join col carrello
            var carrelloAttivo = await _dc.Carrelli.Include(c => c.Dettagli).SingleOrDefaultAsync(c => c.Utente == user && !c.DataChiusura.HasValue);

            // 3. se non lo trovo ne creo uno nuovo
            if (carrelloAttivo == null)
            {
                carrelloAttivo = new Carrello();
                carrelloAttivo.Utente = user;
                carrelloAttivo.DataCreazione = DateTime.Now;
                _dc.Carrelli.Add(carrelloAttivo);
            }

            // 4. recupero il prodotto
            var prodotto = await _dc.Prodotti.SingleAsync(p => p.Id == prodottoId);


            // 5. cerco se esiste già tra i dettagli del carrello, il prodotto che voglio inserire
            var dettaglio = carrelloAttivo.Dettagli.SingleOrDefault(cd => cd.Prodotto == prodotto);

            // 6. se non esiste lo creo
            if (dettaglio == null)
            {
                dettaglio = new CarrelloDettaglio { Prodotto = prodotto, Quantita = 1 };
                carrelloAttivo.Dettagli.Add(dettaglio);
            }
            else
            {
                // 7. se esiste il dettaglio incremento di 1 la quantità
                dettaglio.Quantita++;
            }

            try
            {
                _dc.SaveChanges();
            }
            catch (Exception ex)
            {

            }


            return RedirectToAction("Index", "Home");
        }
    }
}
