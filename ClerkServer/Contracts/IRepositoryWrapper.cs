using System.Threading.Tasks;
using ClerkServer.Contracts.Domain;

namespace ClerkServer.Contracts {

	public interface IRepositoryWrapper {

		IUserRepository User { get; }
		Task<int> SaveAsync();

	}

}