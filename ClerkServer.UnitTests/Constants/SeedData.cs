using ClerkServer.Entities.Models;

namespace ClerkServer.UnitTests.Constants {

	public static class SeedData {

		public static User GetCreatedUser() {
			return new User {
				Email = "test-user@clerk.dev",
				Name = "Test User",
				PhoneNumber = "123456789",
				Picture = "//some.cdn.com/some-path/avatar.png"
			};
		}

		public static User GetPendingUser() {
			return new User {
				Email = "test-user-pending@clerk.dev",
				Name = "Test User Pending",
				PhoneNumber = "123456789",
				Picture = "//some.cdn.com/some-path/avatar.png"
			};
		}

	}

}