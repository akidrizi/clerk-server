﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using ClerkServer.RandomUserAPI.Models;
using ClerkServer.Extensions;

namespace ClerkServer.RandomUserAPI {

	public class RandomUserService {

		public HttpClient Client { get; }

		public RandomUserService(HttpClient client) {
			client.BaseAddress = new Uri("https://randomuser.me/api/");
			client.DefaultRequestHeaders.Add("Accept", "*/*");
			client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Clerk");

			Client = client;
		}

		/*
		 * Collects data from RandomUser API.
		 *
		 * Returns null if service is unavailable.
		 */
		public async Task<Catalog> CollectRandomUsersAsync(int results = 0) {
			var response = await Client.GetAsync($"?results={results}&exc=cell,dob,id,gender,location,login,nat");
			if (!response.IsSuccessStatusCode)
				return null;				
			
			return await response.Content.ReadAsJsonAsync<Catalog>();
		}

	}

}