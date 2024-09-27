using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Services
{
	public class NotificacaoService : INotificacaoService
	{
		private readonly IConfiguration _configuration;

		public NotificacaoService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task EnviarNotificacaoParaAdministrador(Cao cao)
		{
			var emailAdministrador = _configuration["EmailSettings:AdminEmail"];
			var assunto = "Novo perfil de cachorro pendente";
			var mensagem = $"O cão {cao.Nome} está aguardando aprovação.";

			await EnviarEmailAsync(emailAdministrador, assunto, mensagem);
		}

		public async Task EnviarNotificacaoParaUsuario(Cao cao)
		{
			var emailUsuario = cao.Proprietario.Email;
			var assunto = "O Perfil do seu cachorro foi aprovado!";
			var mensagem = $"O perfil do seu cão {cao.Nome} foi aprovado" +
				$" e está agora visível na plataforma.";

			await EnviarEmailAsync(emailUsuario, assunto, mensagem);
		}

		private async Task EnviarEmailAsync(string email, string assunto, string mensagem)
		{
			var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
			{
				Port = int.Parse(_configuration["EmailSettings:Port"]),
				Credentials = new NetworkCredential(
					_configuration["EmailSettings:Username"],
					_configuration["EmailSettings:Password"]),
				EnableSsl = true,
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress(_configuration["EmailSettings:From"]),
				Subject = assunto,
				Body = mensagem,
				IsBodyHtml = true,
			};
			mailMessage.To.Add(email);

			await smtpClient.SendMailAsync(mailMessage);
		}
	}
}
