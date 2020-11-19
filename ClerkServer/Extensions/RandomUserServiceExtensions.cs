using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClerkServer.Entities.Models;
using ClerkServer.RandomUserAPI;

namespace ClerkServer.Extensions {

	public static class RandomUserServiceExtensions {
		
		/*
		 * Collect response from Random User API without filtering.
		 */
		public static async Task<List<User>> GetRandomUsersAsync(this RandomUserService randomUserService, int results) {
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
		 * Filters out duplicate emails from the API response.
		 */
		public static async Task<List<User>> GetUsersWithUniqueEmail(this RandomUserService randomUserService, int results) {
			var catalog = await randomUserService.CollectRandomUsersAsync(results);
			if (catalog == null)
				return new List<User>();

			return catalog.Results
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