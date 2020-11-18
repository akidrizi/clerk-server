using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ClerkServer.Contracts;
using ClerkServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClerkServer.Repository {

	public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class {

		protected RepositoryContext RepositoryContext { get; set; }

		public RepositoryBase(RepositoryContext repositoryContext) {
			RepositoryContext = repositoryContext;
		}

		public IQueryable<T> FindAll() {
			return RepositoryContext.Set<T>().AsNoTracking();
		}

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) {
			return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
		}

		public void Create(T entity) {
			RepositoryContext.Set<T>().Add(entity);
		}

		public void CreateRange(IEnumerable<T> entities) {
			RepositoryContext.Set<T>().AddRange(entities);
		}

		public async Task CreateRangeAsync(IEnumerable<T> entities) {
			await RepositoryContext.Set<T>().AddRangeAsync(entities);
		}
		
		public void Update(T entity) {
			RepositoryContext.Set<T>().Update(entity);
		}

		public void UpdateRange(IEnumerable<T> entities) {
			RepositoryContext.Set<T>().UpdateRange(entities);
		}

		public void Delete(T entity) {
			RepositoryContext.Set<T>().Remove(entity);
		}

	}

}