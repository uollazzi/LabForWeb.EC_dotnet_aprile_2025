using LabForWeb.MVC.Data.Models;
using LabForWeb.MVC.Models;

namespace LabForWeb.MVC.Extensions;

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
}
