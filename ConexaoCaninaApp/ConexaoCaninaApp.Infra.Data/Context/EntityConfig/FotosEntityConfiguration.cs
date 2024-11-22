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
	public class FotosEntityConfiguration : IEntityTypeConfiguration<Foto>
	{
		public void Configure(EntityTypeBuilder<Foto> builder)
		{
			builder.HasKey(x => x.FotoId);

			builder.Property(x => x.FotoId)
				.ValueGeneratedNever();

			builder.Property(x => x.CaminhoArquivo)
				.IsRequired();

			builder.Property(x => x.Descricao)
				.HasMaxLength(500);
		}
	}
}
