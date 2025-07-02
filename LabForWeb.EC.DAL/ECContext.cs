using LabForWeb.EC.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LabForWeb.EC.DAL;

public class ECContext : DbContext
{
    public ECContext() { }

    public ECContext(DbContextOptions<ECContext> options)
        : base(options)
    {

    }

    public DbSet<Utente> Utenti => Set<Utente>();
    public DbSet<Prodotto> Prodotti => Set<Prodotto>();
    public DbSet<Ordine> Ordini => Set<Ordine>();
    public DbSet<OrdineDettaglio> OrdineDettagli => Set<OrdineDettaglio>();
    public DbSet<Categoria> Categorie => Set<Categoria>();
    public DbSet<Indirizzo> Indirizzi => Set<Indirizzo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Ordine>(entity =>
        //{            
        //    entity.HasIndex(e => e.Stato);
        //    entity.HasIndex(e => new { e.Numero, e.Anno }).IsUnique();
        //    entity.HasIndex(e => e.Data);
        //});
        modelBuilder.Entity<Utente>()
            .HasData(
                new Utente
                {
                    Id = 1,
                    Nome = "Admin",
                    Cognome = "Admin",
                    Email = "admin@admin.com",
                    CodiceFiscale = "",
                    Telefono = "",
                    NotificheWA = true
                }
            );

        modelBuilder.Entity<Categoria>()
            .HasData(
                new Categoria
                {
                    Id = 1,
                    Nome = "Articoli sportivi"
                },
                new Categoria
                {
                    Id = 2,
                    Nome = "Elettrodomestici"
                },                
                new Categoria
                {
                    Id = 3,
                    Nome = "Abbigliamento per la coppia"
                }
            );

    }
}
