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

		public async Task EnviarNotificacaoParaAdministrador(Cao cao, string observacao)
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

		public async Task EnviarNotificacaoDeExclusaoParaUsuario(string emailUsuario, string nomeDoCao, string mensagem)
		{
			var assunto = "Exclusão de perfil concluida";
			var mensagemParaUsuario = $"o perfil do cão {nomeDoCao} foi excluído com sucesso! " +
				$"Caso precise de mais informações ou suporte, entre em contato conosco";

			await EnviarEmailAsync(emailUsuario, assunto, mensagem);
		}

		public async Task EnviarNotificacaoSolicitacaoCruzamento(string emailUsuario, string nomeDoCao, string mensagem)
		{
			var assunto = "Nova Solicitação de Cruzamento";
			var mensagemParaUsuario = $"Você recebeu uma solicitação de cruzamento para o cão {nomeDoCao}. {mensagem}";

			await EnviarEmailAsync(emailUsuario, assunto, mensagemParaUsuario);
		}

		public async Task EnviarNotificacaoDeLike(string emailUsuario, string nomeDoCao)
		{
			var assunto = "Seu cão recebeu um novo Like!";
			var mensagem = $"O perfil do seu cão {nomeDoCao} acabou de receber um Like!";

			await EnviarEmailAsync(emailUsuario, assunto, mensagem);
		}
		public async Task EnviarNotificacaoDeUnlike(string emailUsuario, string nomeDoCao)
		{
			var assunto = "Seu cão teve um Like removido!";
			var mensagem = $"O perfil do seu cão {nomeDoCao} teve um Like removido.";

			await EnviarEmailAsync(emailUsuario, assunto, mensagem);
		}

		public async Task EnviarNotificacaoSolicitacaoRejeitada(string emailUsuario, string nomeDoCao, string requisitosNaoAtendidos)
		{
			var assunto = "Solicitação de Cruzamento Rejeitada";
			var mensagem = $"A solicitação de cruzamento para o cão {nomeDoCao} foi rejeitada. Os seguintes requisitos não foram atendidos: {requisitosNaoAtendidos}.";

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
