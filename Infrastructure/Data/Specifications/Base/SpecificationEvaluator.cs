using Infrastructure.Data.Specifications.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using StudentManager_Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Specifications.Base
{
	internal static class SpecificationEvaluator
	{
		internal static IQueryable<T> ApplySpecification<T>(IQueryable<T> baseQuery, ISpecification<T> specification) where T : class, IHasId
		{
			baseQuery = specification.Includes.Aggregate
				(baseQuery, (current, include) => current.Include(include));

			baseQuery = specification.IncludeStrings.Aggregate(baseQuery, (current, include) => current.Include(include));

			baseQuery = specification.OrderByExpressions.Aggregate(baseQuery,
				(current, expression) => current.OrderBy(expression));

			baseQuery = specification.OrderByDescendingExpressions.Aggregate(baseQuery,
				(current, expression) => current.OrderByDescending(expression));

			baseQuery = specification.WhereExpressions.Aggregate(baseQuery,
				(current, expression) => current.Where(expression));

			if (specification.Skip > 0)
			{
				baseQuery = baseQuery.Skip(specification.Skip);
			}

			if (specification.Take > 0)
			{
				baseQuery = baseQuery.Take(specification.Take);
			}

			if (specification.IsNoTracking)
			{
				baseQuery.AsNoTracking();
			}

			return baseQuery;
		}
	}
}
