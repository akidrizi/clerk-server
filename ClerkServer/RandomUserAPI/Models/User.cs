﻿namespace ClerkServer.RandomUserAPI.Models {

	public class User {

		public string Gender { get; set; }
		public Location Location { get; set; }
		public string Email { get; set; }
		public Login Login { get; set; }
		public Dob Dob { get; set; }
		public string Phone { get; set; }
		public string Cell { get; set; }
		public Id Id { get; set; }
		public Picture Picture { get; set; }
		public string Nat { get; set; }

	}

}