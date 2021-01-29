using Infrastructure.Data.Specifications.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories.Base.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> specification);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
