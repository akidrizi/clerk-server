using System;
using System.ComponentModel.DataAnnotations;

namespace ClerkServer.Entities.Models {

	public class User {

		public long Id { get; set; }

		[MaxLength(250)]
		public string Email { get; set; }

		[MaxLength(250)]
		public string Name { get; set; }

		[MaxLength(20)]
		public string PhoneNumber { get; set; }

		public string Picture { get; set; }

		public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

	}

}