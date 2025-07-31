using LabForWeb.EC.API.Models;
using LabForWeb.EC.DAL.Models;

namespace LabForWeb.EC.API.Extensions;

public static class ModelsExtensions
{
    public static CategoriaModel ToCategoriaModel(this Categoria item)
    {
        return new CategoriaModel
        {
            Id = item.Id,
            Nome = item.Nome
        };
    }

    public static ProdottoModel ToProdottoModel(this Prodotto item)
    {
        return new ProdottoModel
        {
            Id = item.Id,
            Nome = item.Nome,
            Descrizione = item.Descrizione,
            DescrizioneBreve = item.DescrizioneBreve,
            Attivo = item.Attivo,
            Giacenza = item.Giacenza,
            Prezzo = item.Prezzo,
            Visibile = item.Visibile,
            ImageUrl = item.ImageUrl
        };
    }
}
