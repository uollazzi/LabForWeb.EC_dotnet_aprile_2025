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


}
