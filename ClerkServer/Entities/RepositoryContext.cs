using ClerkServer.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ClerkServer.Entities {

	public class RepositoryContext : DbContext {

		public RepositoryContext(DbContextOptions options)
			: base(options) { }

		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<User>(user => {
				user.HasKey(e => e.Id);
				user.HasIndex(e => e.Email).IsUnique();
			});
		}

	}

}