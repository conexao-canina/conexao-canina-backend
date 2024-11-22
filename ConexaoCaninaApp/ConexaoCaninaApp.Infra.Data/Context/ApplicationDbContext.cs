using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConexaoCaninaApp.Domain.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace ConexaoCaninaApp.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public ApplicationDbContext() { }

        public DbSet<Cao> Caes { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
		public DbSet<Usuario> Proprietarios { get; set; }
		public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<HistoricoDeSaude> HistoricosDeSaude { get; set; }      
        public DbSet<Sugestao> Sugestoes { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		}
	}
}
