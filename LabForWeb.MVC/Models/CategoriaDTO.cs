using System.ComponentModel.DataAnnotations;

namespace LabForWeb.MVC.Models;

// DTO => Data Transfer Object
public class CategoriaDTO
{
    [Required(ErrorMessage ="Il campo {0} è obbligatorio")]
    public string? Nome{ get; set; }
}
