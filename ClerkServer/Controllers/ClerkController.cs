using Microsoft.AspNetCore.Mvc;

namespace ClerkServer.Controllers {

	[ApiController]
	[Route("1.0")]
	public class ClerkController : ControllerBase {

		[HttpGet("clerks")]
		public string Get() {
			return "Test";
		}

	}

}