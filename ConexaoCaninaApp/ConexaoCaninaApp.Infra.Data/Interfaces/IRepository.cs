using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Infra.Data.Interfaces
{
	public interface IRepository<TEntity, TKey> where TEntity : class
	{
		List<TEntity> GetAll();
		TEntity GetById(TKey id);
		void Add(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity id);
		int SaveChanges();

	}
}
