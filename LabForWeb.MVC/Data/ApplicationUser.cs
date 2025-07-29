using LabForWeb.MVC.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabForWeb.MVC.Data;

public class ApplicationUser: IdentityUser
{
    [PersonalData]
    [Required]
    [MaxLength(120)]
    public string? Nome { get; set; }

    [PersonalData]
    [Required]
    [MaxLength(120)]
    public string? Cognome { get; set; }

    [PersonalData]
    [Required]
    [Column(TypeName = "char(16)")]
    public string? CodiceFiscale { get; set; }

    [PersonalData]
    [Required]
    public bool NotificheWA { get; set; }

    public virtual ICollection<Indirizzo> Indirizzi { get; set; } = [];
}
