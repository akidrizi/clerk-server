using System;
using System.Data.Common;
using ClerkServer.Entities;
using ClerkServer.UnitTests.Constants;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace ClerkServer.UnitTests.Repository.Domain {

	public class UserRepositoryFixture : IDisposable {

		private static bool _databaseInitialized;

		public UserRepositoryFixture() {
			Connection = new MySqlConnection(@"Server=localhost;User Id=root;Password=;Database=clerk_test;");

			Seed();

			Connection.Open();
		}

		private DbConnection Connection { get; }

		public RepositoryContext CreateContext(DbTransaction transaction = null) {
			var context = new RepositoryContext(new DbContextOptionsBuilder<RepositoryContext>().UseMySql(Connection).Options);

			if (transaction != null) {
				context.Database.UseTransaction(transaction);
			}

			return context;
		}

		private void Seed() {
			if (_databaseInitialized) return;
			using (var context = CreateContext()) {
				context.Database.EnsureDeleted();
				context.Database.EnsureCreated();

				var user = SeedData.GetCreatedUser();

				context.Add(user);
				context.SaveChanges();
			}

			_databaseInitialized = true;
		}

		public void Dispose() {
			Connection.Dispose();
		}

	}

}