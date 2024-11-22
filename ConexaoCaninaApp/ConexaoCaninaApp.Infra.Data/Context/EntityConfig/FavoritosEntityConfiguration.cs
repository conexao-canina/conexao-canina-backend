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
	internal class FavoritosEntityConfiguration : IEntityTypeConfiguration<Favorito>
	{
		public void Configure(EntityTypeBuilder<Favorito> builder)
		{
			builder.HasKey(x => x.FavoritoId);

			builder.Property(x => x.FavoritoId)
				.ValueGeneratedNever();

			builder.Property(x => x.Data)
				.IsRequired();

			builder.Ignore(x => x.Cao)
				.HasOne(_ => _.Cao)
				.WithMany()
				.HasForeignKey("_caoId")
				.HasConstraintName("FK_Favoritos_Cao");

			builder.Property<Guid>("_caoId")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("CaoId")
				.IsRequired();
		}
	}
}
