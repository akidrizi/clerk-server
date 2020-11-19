using System.Linq;
using System.Threading.Tasks;
using ClerkServer.Entities.Models;
using ClerkServer.Repository;
using ClerkServer.UnitTests.Constants;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClerkServer.UnitTests.Repository {

	public class RepositoryWrapperTests : IClassFixture<RepositoryWrapperFixture> {

		private readonly RepositoryWrapperFixture _fixture;

		public RepositoryWrapperTests(RepositoryWrapperFixture fixture) => _fixture = fixture;

		[Fact]
		public async Task FindAll_WithPreCreatedUsers_HasAtLeastOneUser() {
			// Arrange
			await using var context = _fixture.CreateContext();
			var repository = new RepositoryWrapper(context);
			
			// Act
			var item = await repository.User.FindAll().ToListAsync();

			// Assert
			Assert.True(item.Any());
		}

		[Fact]
		public async Task FindByCondition_ByUserEmail_ReturnsUser() {
			// Arrange
			await using var context = _fixture.CreateContext();
			var repository = new RepositoryWrapper(context);

			var createdEmail = SeedData.GetCreatedUser().Email;
			
			// Act
			var user = await repository.User.FindByCondition(u => u.Email == createdEmail).FirstAsync();

			// Assert
			Assert.True(user.Email.Equals(createdEmail));
			Assert.IsType<User>(user);
		}

		[Fact]
		public async Task Create_UserEntity_SavedSuccessfully() {
			// Arrange
			await using var context = _fixture.CreateContext();
			var repository = new RepositoryWrapper(context);
			
			var user = SeedData.GetPendingUser();

			// Act
			repository.User.Create(user);
			var created = await repository.SaveAsync() > 0;

			// Assert
			Assert.True(created);
		}

		[Fact]
		public async Task Update_UserPicture_UpdatedSuccessfully() {
			// Arrange
			await using var context = _fixture.CreateContext();
			var repository = new RepositoryWrapper(context);

			var createdEmail = SeedData.GetCreatedUser().Email;
			var user = await repository.User.FindByCondition(u => u.Email == createdEmail).FirstAsync();
			user.Picture = "new-avatar.png";

			// Act
			repository.User.Update(user);
			var updated = await repository.SaveAsync() > 0;

			// Assert
			Assert.True(updated);
		}

		[Fact]
		public async Task Delete_UserEntity_DeletedSuccessfully() {
			// Arrange
			await using var context = _fixture.CreateContext();
			var repository = new RepositoryWrapper(context);
			
			var user = SeedData.GetPendingUser();
			repository.User.Create(user);
			await repository.SaveAsync();

			// Act
			repository.User.Delete(user);
			var deleted = await repository.SaveAsync() > 0;

			// Assert
			Assert.True(deleted);
		}

	}

}