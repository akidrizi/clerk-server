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
			client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");

			Client = client;
		}

		public async Task<Catalog> GetRandomUsersAsync(int results = 0) {
			var response = await Client.GetAsync($"?results={results}&exc=cell,dob,id,gender,location,login,nat,registered");

			response.EnsureSuccessStatusCode();
			
			return await response.Content.ReadAsJsonAsync<Catalog>();
		}

	}

}