using System.Collections.Generic;
using System.Threading.Tasks;
using ClerkServer.Domain;
using ClerkServer.Entities.Models;

namespace ClerkServer.Contracts.Domain {

	public interface IUserRepository : IRepositoryBase<User> {

		List<User> GetAllUsers();
		Task<List<User>> GetAllUsersAsync();

		User GetUserById(long id);
		Task<User> GetUserByIdAsync(long id);

		User CreateUser(UserDto dto);

		User FindByEmail(string email);
		Task<User> FindByEmailAsync(string email);

	}

}