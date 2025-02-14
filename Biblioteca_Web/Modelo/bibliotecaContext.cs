using Microsoft.EntityFrameworkCore;

namespace Biblioteca_Web.Modelo
{
    public class bibliotecaContext : DbContext
    {
       

        
            public bibliotecaContext(DbContextOptions<bibliotecaContext> options) : base(options)
            {



            }
            public DbSet<Autor> Autor { get; set; }
            public DbSet<Libro> libro { get; set; }

        
    }
}
