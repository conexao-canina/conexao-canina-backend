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
        public DbSet<Proprietario> Proprietarios { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Foto> Fotos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configurando relacionamento um-para-muitos entre Proprietário e Cão
            modelBuilder.Entity<Proprietario>() 
                .HasMany(p => p.Caes)
                .WithOne(c => c.Proprietario)
                .HasForeignKey(c => c.ProprietarioId)
                .OnDelete(DeleteBehavior.Cascade); // quando proprietario for excluído, todos os seus cães também serão

            // configurando relacionamento um-para-um entre Proprietário e Localização
            modelBuilder.Entity<Proprietario>()
                .HasOne(p => p.Localizacao)
                .WithOne()
                .HasForeignKey<Proprietario>(p => p.LocalizacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            // configurando de indice unico no campo "Email" do usuario
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Cao>()
                .HasMany(c => c.Fotos)
                .WithOne(f => f.Cao)
				.HasForeignKey(f => f.CaoId);


		}
	}
}
