using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClerkServer.Domain;
using ClerkServer.Entities.Models;
using ClerkServer.Repository;
using ClerkServer.Repository.Domain;
using ClerkServer.UnitTests.Constants;
using Xunit;

namespace ClerkServer.UnitTests.Repository.Domain {

	public class UserRepositoryTests : IClassFixture<UserRepositoryFixture> {

		private readonly UserRepositoryFixture _fixture;

		public UserRepositoryTests(UserRepositoryFixture fixture) => _fixture = fixture;

		[Fact]
		public async Task GetAllUsersAsync_WithPreCreated_ReturnsListOfUsers() {
			// Arrange
			await using var context = _fixture.CreateContext();
			var repository = new UserRepository(context);

			// Act
			var list = await repository.GetAllUsersAsync();

			// Assert
			Assert.True(list.Any());
			Assert.IsType<List<User>>(list);
		}

		[Fact]
		public async Task GetUserByIdAsync_FromQueriedUserList_ReturnsUser() {
			// Arrange
			await using var context = _fixture.CreateContext();
			var repository = new UserRepository(context);
			var list = await repository.GetAllUsersAsync();

			// Act
			var user = await repository.GetUserByIdAsync(list.First().Id);

			// Assert
			Assert.IsType<User>(user);
		}

		[Fact]
		public async Task CreateUser_WithUserDto_SavedSuccessfully() {
			// Arrange
			await using var context = _fixture.CreateContext();
			var repository = new UserRepository(context);
			var repoWrapper = new RepositoryWrapper(context);

			var user = new UserDto {
				Email = "test@test.test",
				Name = "Test Testerson",
				PhoneNumber = "123456789",
				Picture = "some-picture.png"
			};

			// Act
			repository.CreateUser(user);
			var created = await repoWrapper.SaveAsync() > 0;

			// Assert
			Assert.True(created);
		}

		[Fact]
		public async Task BulkInsertUniqueUsersAsync_CreateListWithVariousEmails_SavesOnlyFilteredOutUser() {
			// Arrange
			await using var context = _fixture.CreateContext();
			var repository = new UserRepository(context);
			var repoWrapper = new RepositoryWrapper(context);
			var list = new List<User> { SeedData.GetCreatedUser(), SeedData.GetPendingUser() };

			// Act
			await repository.BulkInsertUniqueUsersAsync(list);
			var created = await repoWrapper.SaveAsync();

			// Assert
			Assert.True(list.Count > created);
		}

		[Fact]
		public async Task FindByEmailAsync_ExistingUser_DeletedSuccessfully() {
			// Arrange
			await using var context = _fixture.CreateContext();
			var repository = new UserRepository(context);
			var s = SeedData.GetCreatedUser().Email;

			// Act
			var user = await repository.FindByEmailAsync(s);

			// Assert
			Assert.IsType<User>(user);
		}

	}

}