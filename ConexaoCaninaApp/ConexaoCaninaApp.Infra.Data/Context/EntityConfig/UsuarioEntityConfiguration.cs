using ConexaoCaninaApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Infra.Data.Context.EntityConfig
{
	internal class UsuarioEntityConfiguration : IEntityTypeConfiguration<Usuario>
	{ // 12
		public void Configure(EntityTypeBuilder<Usuario> builder)
		{
			builder.HasKey(x => x.UsuarioId);

			builder.Property(x => x.UsuarioId)
				.ValueGeneratedNever();

			builder.Property(x => x.Nome)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.Email)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.Telefone)
				.IsRequired()
				.HasMaxLength(20);

			builder.Property(x => x.Senha)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.IsAdmin)
				.IsRequired();

			builder.HasMany(x => x.Caes)
				.WithOne()
				.HasForeignKey("UsuarioId")
				.HasConstraintName("FK_Cao_Usuario");

			builder.HasMany(x => x.Favoritos)
				.WithOne()
				.HasForeignKey("UsuarioId")
				.HasConstraintName("FK_Like_Usuario");

			builder.HasMany(x => x.Sugestoes)
				.WithOne()
				.HasForeignKey("UsuarioId")
				.HasConstraintName("FK_Sugestao_Usuario");


		}
	}
}
