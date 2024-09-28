using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Application.Services;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using ConexaoCaninaApp.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Infra.IoC
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddProjectServices(this IServiceCollection services)
		{
			services.AddScoped<ICaoService, CaoService>();
			services.AddScoped<ICaoRepository, CaoRepository>();
			services.AddScoped<INotificacaoService, NotificacaoService>();
			services.AddScoped<IFotoService, FotoService>();
			services.AddScoped<IArmazenamentoService, ArmazenamentoLocalService>();
			services.AddScoped<IFotoRepository, FotoRepository>();

			return services;
		}
	}
}
