using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsyncWithSpec(ISpecification<T>spec);
        Task<T?> GetEntityWithSpec(ISpecification<T>spec);

        Task<IReadOnlyList<TResult>> GetAllAsyncWithSpec<TResult>(ISpecification<T,TResult> spec);
        Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T,TResult> spec);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> SaveAll();
        bool IsExists(int id);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
