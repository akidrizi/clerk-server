using System.Threading.Tasks;
using ClerkServer.Contracts;
using ClerkServer.Contracts.Domain;
using ClerkServer.Entities;
using ClerkServer.Repository.Domain;

namespace ClerkServer.Repository {

	public class RepositoryWrapper : IRepositoryWrapper {

		private readonly RepositoryContext _context;
		private IUserRepository _user;

		public RepositoryWrapper(RepositoryContext context) {
			_context = context;
		}

		public IUserRepository User {
			get {
				if (_user == null) {
					_user = new UserRepository(_context);
				}

				return _user;
			}
		}

		public async Task SaveAsync() {
			await _context.SaveChangesAsync();
		}

	}

}