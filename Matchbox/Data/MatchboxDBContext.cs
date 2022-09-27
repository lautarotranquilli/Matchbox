using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Matchbox.Models;

namespace Matchbox.Data
{
    public class MatchboxDBContext : DbContext
    {
        public MatchboxDBContext (DbContextOptions<MatchboxDBContext> options)
            : base(options)
        {
        }

        public DbSet<Matchbox.Models.Cliente> Cliente { get; set; }

        public DbSet<Matchbox.Models.Usuario> Usuario { get; set; }

        public DbSet<Matchbox.Models.Empresa> Empresa { get; set; }
    }
}
