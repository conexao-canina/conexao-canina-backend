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
			
			services.AddScoped<INotificacaoService, NotificacaoService>();
			
			return services;
		}
	}
}
