using System.Threading.Tasks;
using ClerkServer.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClerkServer.Controllers {

	[ApiController]
	[Route("1.0")]
	public class ClerkController : ControllerBase {

		[HttpGet("clerks")]
		public async Task<IActionResult> Get() {
			return Ok(new User());
		}

	}

}