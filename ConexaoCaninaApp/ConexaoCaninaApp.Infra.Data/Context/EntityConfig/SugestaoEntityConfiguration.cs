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
	internal class SugestaoEntityConfiguration : IEntityTypeConfiguration<Sugestao>
	{
		public void Configure(EntityTypeBuilder<Sugestao> builder)
		{
			builder.HasKey(x => x.SugestaoId);

			builder.Property(x => x.SugestaoId)
				.ValueGeneratedNever();

			builder.Property(x => x.Descricao)
				.IsRequired()
				.HasMaxLength(500);

			builder.Property(x => x.DataEnvio)
				.IsRequired();	
		}
	}
}
