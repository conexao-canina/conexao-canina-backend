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
	public abstract class Repository<TEntity, Tkey> : IRepository<TEntity, Tkey> where TEntity : class
	{
		protected readonly ApplicationDbContext _dataContext;
		protected readonly DbSet<TEntity> _entity;

		public Repository(ApplicationDbContext context)
		{
			_dataContext = context ?? throw new ArgumentNullException(nameof(context));
			_entity = _dataContext.Set<TEntity>();			
		}

		public virtual void Add(TEntity entity)
		{
			_entity.Add(entity);
		}

		public virtual void Delete(TEntity entity)
		{
			_entity.Remove(entity);
		}

		public virtual List<TEntity> GetAll()
		{
			return _entity.ToList();
		}

		public virtual TEntity GetById(Tkey id)
		{
			return _entity.Find(id);
		}

		public virtual void Update(TEntity entity)
		{
			_entity.Update(entity);
		}

		public int SaveChanges()
		{
			return _dataContext.SaveChanges();
		}
	}
}
