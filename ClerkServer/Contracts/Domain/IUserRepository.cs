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

		/*
		 * Inserts list of users in the database.
		 * Will exclude emails that already exist.
		 */
		void BulkInsertUsers(List<User> users);
		
		/*
		 * Inserts list of users in the database.
		 * Will exclude emails that already exist.
		 */
		Task BulkInsertUsersAsync(List<User> users);
		
		User FindByEmail(string email);
		Task<User> FindByEmailAsync(string email);

	}

}