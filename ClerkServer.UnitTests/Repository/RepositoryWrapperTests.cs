using System.Linq;
using ClerkServer.Repository;
using Xunit;

namespace ClerkServer.UnitTests.Repository {

	public class RepositoryWrapperTests : IClassFixture<RepositoryWrapperFixture> {

		private readonly RepositoryWrapperFixture _fixture;
		
		public RepositoryWrapperTests(RepositoryWrapperFixture fixture) => _fixture = fixture;

		[Fact]
		public void FindAll_HasListWithUsers_ReturnsList() {
			// Arrange
			using var context = _fixture.CreateContext();
			var repository = new RepositoryWrapper(context);

			// Act
			var item = repository.User.FindAll().ToList();

			// Assert
			Assert.True(item.Any());
		}

	}

}