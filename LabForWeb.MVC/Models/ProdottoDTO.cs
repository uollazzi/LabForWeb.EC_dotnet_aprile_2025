using System.ComponentModel.DataAnnotations;

namespace LabForWeb.MVC.Models;

public class ProdottoDTO
{    
    [Required(ErrorMessage ="{0} obbligatorio")]
    [MaxLength(400)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "{0} obbligatorio")]
    [MaxLength(2000)]
    public string? DescrizioneBreve { get; set; }

    [Required(ErrorMessage = "{0} obbligatorio")]
    public string? Descrizione { get; set; }

    [Required(ErrorMessage = "{0} obbligatorio")]
    public short Giacenza { get; set; } = 0;

    [Required(ErrorMessage = "{0} obbligatorio")]
    public decimal Prezzo { get; set; }

    [Required]
    public bool Visibile { get; set; } = true;

    [Required]
    public bool Attivo { get; set; } = true;    

    public string? ImageUrl { get; set; }

    public IFormFile? Immagine { get; set; }

    [Required]
    public int CategoriaID { get; set; }
}
