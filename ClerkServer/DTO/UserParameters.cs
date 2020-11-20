using System.ComponentModel.DataAnnotations;

namespace ClerkServer.DTO {

	public class UserParameters {

		[Range(1, 100)]
		public int Limit { get; set; } = 10;

		[EmailAddress]
		public string Email { get; set; }

		public long? Starting_After { get; set; }

		public long? Ending_Before { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "Page number must be an integer bigger than {1}")]
		public int Page { get; set; } = 1;

	}

}