using Core.Entities;
using Core.Interfaces;
using Infrastracture.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Repositories
{
    public class UnitOfWork(StoreContext context) : IUnitOfWork
    {
        private readonly Hashtable _repositories = new();
        public async Task<bool> Complete()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(context);
                _repositories.Add(type, repository);
            }
            if (_repositories[type] is IGenericRepository<TEntity> repo)
                return repo;
            throw new InvalidCastException($"The repository for type {type} is not of the expected type.");

        }
    }
}
