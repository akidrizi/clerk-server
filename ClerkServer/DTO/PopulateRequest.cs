using System.ComponentModel.DataAnnotations;

namespace ClerkServer.DTO {

	public class PopulateRequest {

		[Required(ErrorMessage = "{0} must be a number between 1 to 5000")]
		[Range(1, 5000)]
		public int Users { get; set; }

	}

}