using Infrastructure.Data.Specifications.Base.Interfaces;
using StudentManager_Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Specifications.Base
{
    public class Specification<T> : ISpecification<T> where T : class, IHasId
    {
        protected Specification() { }
        public List<Expression<Func<T, object>>> Includes { get; init; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; init; } = new List<string>();
        public List<Expression<Func<T, object>>> OrderByExpressions { get; init; } = new List<Expression<Func<T, object>>>();
        public List<Expression<Func<T, object>>> OrderByDescendingExpressions { get; init; } = new List<Expression<Func<T, object>>>();
        public List<Expression<Func<T, bool>>> WhereExpressions { get; init; } = new List<Expression<Func<T, bool>>>();
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsNoTracking { get; set; } = false;

        public ISpecification<T> AddPagination(int take = 0, int skip = 0)
        {
            Take = take;
            Skip = skip;

            return this;
        }

        public ISpecification<T> WithoutTracking()
        {
            IsNoTracking = true;
            return this;
        }

        public ISpecification<T> WithTracking()
        {
            IsNoTracking = false;
            return this;
        }

        protected void AddOrdering(Expression<Func<T, object>> expression)
        {
            OrderByExpressions.Add(expression);
        }

        protected void AddDescendingOrdering(Expression<Func<T, object>> expression)
        {
            OrderByDescendingExpressions.Add(expression);
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddWhere(Expression<Func<T, bool>> selectExpression)
        {
            WhereExpressions.Add(selectExpression);
        }

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}
