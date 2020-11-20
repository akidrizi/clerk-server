using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClerkServer.Entities.Models;
using ClerkServer.RandomUserAPI;

namespace ClerkServer.Extensions {

	public static class RandomUserServiceExtensions {
		
		/*
		 * Convert response from Random User API to User entity without duplicate email filtering.
		 */
		public static async Task<List<User>> ToUsersAsync(this RandomUserService randomUserService, int results) {
			var catalog = await randomUserService.CollectRandomUsersAsync(results);
			if (catalog == null)
				return new List<User>();

			return catalog.Results
				.Select(x => new User {
					Email = x.Email.ToLower(),
					Name = $"{x.Name?.First} {x.Name?.Last}",
					PhoneNumber = x.Phone,
					Picture = x.Picture?.Thumbnail,
					RegistrationDate = x.Registered?.Date ?? DateTime.UtcNow
				}).ToList();
		}

		/*
		 * Convert response from Random User API to User entity with duplicate email filtering.
		 *
		 * Filters out duplicates with the oldest registration date. 
		 */
		public static async Task<List<User>> ToUniqueUsersAsync(this RandomUserService randomUserService, int results) {
			var catalog = await randomUserService.CollectRandomUsersAsync(results);
			if (catalog == null)
				return new List<User>();

			return catalog.Results.OrderBy(u => u.Registered)
				.GroupBy(x => x.Email).Select(y => y.FirstOrDefault()).ToList()
				.Select(x => new User {
					Email = x.Email.ToLower(),
					Name = $"{x.Name?.First} {x.Name?.Last}",
					PhoneNumber = x.Phone,
					Picture = x.Picture?.Thumbnail,
					RegistrationDate = x.Registered?.Date ?? DateTime.UtcNow
				}).ToList();
		}

	}

}