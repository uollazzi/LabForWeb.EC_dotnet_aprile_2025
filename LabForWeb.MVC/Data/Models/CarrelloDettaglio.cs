using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LabForWeb.MVC.Data.Models;

public class CarrelloDettaglio
{
    public long Id { get; set; }

    [Required]    
    public int Quantita { get; set; } = 1;

    [Required]
    public virtual Carrello? Carrello { get; set; }

    [Required]
    public virtual Prodotto? Prodotto { get; set; }
}
