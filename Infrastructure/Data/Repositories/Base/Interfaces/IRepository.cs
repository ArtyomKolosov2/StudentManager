using Infrastructure.Data.Specifications.Base.Interfaces;
using StudentManager_Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories.Base.Interfaces
{
    public interface IRepository<T> where T : class, IHasId
    {
        public Task<IReadOnlyList<T>> GetAll();
        public Task<IReadOnlyList<T>> GetAll(ISpecification<T> specification);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetById(int id);
        Task<int> Count(Expression<Func<T, bool>> predicate);
    }
}
