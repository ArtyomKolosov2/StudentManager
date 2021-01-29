using StudentManager_Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Specifications.Base.Interfaces
{
    public interface ISpecification<T> where T : class, IHasId
    {
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        List<Expression<Func<T, object>>> OrderByExpressions { get; }
        List<Expression<Func<T, object>>> OrderByDescendingExpressions { get; }
        List<Expression<Func<T, bool>>> WhereExpressions { get; }
        bool IsNoTracking { get; set; }
        int Take { get; set; }
        int Skip { get; set; }
    }
}
