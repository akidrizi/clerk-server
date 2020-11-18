using System.Linq;
using System.Threading.Tasks;
using ClerkServer.Constants;
using ClerkServer.Contracts;
using ClerkServer.Domain;
using ClerkServer.Extensions;
using ClerkServer.RandomUserAPI;
using Microsoft.AspNetCore.Mvc;

namespace ClerkServer.Controllers {

	[ApiController]
	[Route("1.0")]
	public class ClerkController : ControllerBase {

		private readonly IRepositoryWrapper _repository;
		private readonly RandomUserService _randomUserService;

		public ClerkController(IRepositoryWrapper repository, RandomUserService randomUserService) {
			_repository = repository;
			_randomUserService = randomUserService;
		}

		[HttpGet("clerks")]
		public async Task<IActionResult> GetAllUsers() {
			var users = await _repository.User.GetAllUsersAsync();

			return Ok(users);
		}

		[HttpGet("clerks/{id}", Name = "GetUserById")]
		public async Task<IActionResult> GetUserById(long id) {
			var user = await _repository.User.GetUserByIdAsync(id);
			if (user == null) {
				ModelState.AddModelError("Not Found", string.Format(APIMessages.UserIdNotFound, id));
				return BadRequest();
			}

			return Ok(user);
		}

		[HttpPost("populate")]
		public async Task<IActionResult> Post([FromBody]PopulateRequest request) {
			var users = await _randomUserService.GetUsersWithUniqueEmail(request.Users);
			if (!users.Any()) {
				ModelState.AddModelError("Unavailable", APIMessages.RandomUserAPIUnavailable);
				return StatusCode(503);
			}

			await _repository.User.BulkInsertUsersAsync(users);
			var stored = await _repository.SaveAsync();

			return Ok(new PopulateResponse {
				Results = users.Count,
				Stored = stored
			});
		}

	}

}