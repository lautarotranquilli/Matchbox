using Microsoft.EntityFrameworkCore;

namespace Matchbox.Data
{
    public class MatchboxDBContext : DbContext
    {
        public MatchboxDBContext(DbContextOptions<MatchboxDBContext> options)
            : base(options)
        {
        }

        public DbSet<Matchbox.Models.Cliente> Cliente { get; set; }

        public DbSet<Matchbox.Models.Usuario> Usuario { get; set; }

        public DbSet<Matchbox.Models.Empresa> Empresa { get; set; }

        public DbSet<Matchbox.Models.Rubro> Rubro { get; set; }

        public DbSet<Matchbox.Models.Servicio> Servicio { get; set; }
    }
}