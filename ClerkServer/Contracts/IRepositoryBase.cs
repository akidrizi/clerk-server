using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClerkServer.Contracts {

	public interface IRepositoryBase<T> {

		IQueryable<T> FindAll();
		IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
		void Create(T entity);
		void CreateRange(IEnumerable<T> entities);
		Task CreateRangeAsync(IEnumerable<T> entities);
		void Update(T entity);
		void UpdateRange(IEnumerable<T> entities);
		void Delete(T entity);

	}

}