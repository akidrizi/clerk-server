using System.Collections.Generic;
using System.Threading.Tasks;
using ClerkServer.DTO;
using ClerkServer.Entities.Models;

namespace ClerkServer.Contracts.Domain {

	public interface IUserRepository : IRepositoryBase<User> {

		/*
		 * Get the list of all users ordered by latest registration.
		 */
		List<User> GetAllUsers();

		/*
		 * Get the list of all users ordered by latest registration.
		 */
		Task<List<User>> GetAllUsersAsync();

		/*
		 * Get a paginated response of users.
		 */
		PagedList<User> GetUsers(UserParameters userParameters);

		/*
		 * Get a paginated response of users.
		 */
		Task<PagedList<User>> GetUsersAsync(UserParameters userParameters);

		/*
		 * Get single user by ID
		 */
		User GetUserById(long id);

		/*
		 * Get single user by ID
		 */
		Task<User> GetUserByIdAsync(long id);

		/*
		 * Create new User from a data transfer object.
		 */
		User CreateUser(UserDto dto);

		/*
		 * Inserts list of users in the database.
		 * Will exclude emails that already exist.
		 */
		void BulkInsertUniqueUsers(List<User> users);

		/*
		 * Inserts list of users in the database.
		 * Will exclude emails that already exist.
		 */
		Task BulkInsertUniqueUsersAsync(List<User> users);

		/*
		 * Get list of users matching the provided email.
		 */
		List<User> FindUsersByEmail(string email);

		/*
		 * Get list of users matching the provided email.
		 */
		Task<List<User>> FindUsersByEmailAsync(string email);

	}

}