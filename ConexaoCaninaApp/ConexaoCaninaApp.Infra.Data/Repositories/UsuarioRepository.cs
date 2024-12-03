using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Context;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Infra.Data.Repositories
{
	public class UsuarioRepository : Repository<Usuario, Guid>, IUsuarioRepository
	{
		public UsuarioRepository(ApplicationDbContext context) : base(context) { }

        public Usuario GetByEmail(string email)
        {
            return _entity.FirstOrDefault(e => e.Email == email);
        }
    }
}
