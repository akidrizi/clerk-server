using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClerkServer.Contracts.Domain;
using ClerkServer.Domain;
using ClerkServer.Entities;
using ClerkServer.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ClerkServer.Repository.Domain {

	public class UserRepository : RepositoryBase<User>, IUserRepository {

		public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

		public List<User> GetAllUsers() {
			return FindAll().ToList();
		}

		public async Task<List<User>> GetAllUsersAsync() {
			return await FindAll().ToListAsync();
		}

		public User GetUserById(long id) {
			return FindByCondition(u => u.Id == id).FirstOrDefault();
		}

		public async Task<User> GetUserByIdAsync(long id) {
			return await FindByCondition(u => u.Id == id).FirstOrDefaultAsync();
		}

		public User CreateUser(UserDto dto) {
			var user = new User {
				Email = dto.Email,
				Name = dto.Name,
				PhoneNumber = dto.PhoneNumber,
				Picture = dto.Picture,
			};
			Create(user);

			return user;
		}

		public void BulkInsertUniqueUsers(List<User> users) {
			var incomingEmails = users.Select(u => u.Email).ToList();
			var dbEmails = FindByCondition(u => incomingEmails
					.Any(e => e == u.Email))
				.Select(u => u.Email).ToList();

			var existing = users.Where(u => dbEmails.Any(e => e == u.Email)).ToList();
			var newEntries = users.Except(existing).ToList();

			CreateRange(newEntries);
		}

		public async Task BulkInsertUniqueUsersAsync(List<User> users) {
			var incomingEmails = users.Select(u => u.Email).ToList();
			var dbEmails = await FindByCondition(u => incomingEmails
					.Any(e => e == u.Email))
				.Select(u => u.Email).ToListAsync();

			var existing = users.Where(u => dbEmails.Any(e => e == u.Email)).ToList();
			var newEntries = users.Except(existing).ToList();

			await CreateRangeAsync(newEntries);
		}

		public User FindByEmail(string email) {
			return FindByCondition(u => u.Email.Equals(email.ToLower())).FirstOrDefault();
		}

		public async Task<User> FindByEmailAsync(string email) {
			return await FindByCondition(u => u.Email.Equals(email.ToLower())).FirstOrDefaultAsync();
		}

	}

}