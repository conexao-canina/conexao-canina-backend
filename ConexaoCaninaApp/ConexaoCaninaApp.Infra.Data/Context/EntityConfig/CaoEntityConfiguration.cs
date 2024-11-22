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
	public class CaoEntityConfiguration : IEntityTypeConfiguration<Cao>
	{
		public void Configure(EntityTypeBuilder<Cao> builder)
		{
			builder.HasKey(x => x.CaoId);


			builder.Property(x => x.CaoId)
				.ValueGeneratedNever();

			builder.Property(x => x.Nome)
				.IsRequired()
				.HasMaxLength(30);

			builder.Property(x => x.Descricao)
				.HasMaxLength(500);

			builder.Property(x => x.Idade)
				.IsRequired()
				.HasDefaultValue(0);

			builder.Property(x => x.Raca)
				.IsRequired();

			builder.Property(x => x.Tamanho)
				.IsRequired();

			builder.Property(x => x.Genero)
				.IsRequired();

			builder.Property(x => x.Cidade)
				.IsRequired();

			builder.Property(x => x.Estado)
				.IsRequired();

			builder.Property(x => x.CaracteristicasUnicas)
				.HasMaxLength(500);

			builder.Property(x => x.Status)
				.IsRequired()
				.HasDefaultValue(StatusCao.Pendente);

			builder.HasMany(x => x.Fotos)
				.WithOne()
				.HasForeignKey("CaoId")
				.HasConstraintName("FK_Foto_Cao")
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(x => x.HistoricosDeSaude)
				.WithOne()
				.HasForeignKey("CaoId")
				.HasConstraintName("FK_HistoricosDeSaude_Cao")
				.OnDelete(DeleteBehavior.Restrict);


		}
	}
}
