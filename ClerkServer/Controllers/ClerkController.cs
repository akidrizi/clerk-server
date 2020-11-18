using System.Linq;
using System.Threading.Tasks;
using ClerkServer.Constants;
using ClerkServer.Contracts;
using ClerkServer.Domain;
using ClerkServer.Entities.Models;
using ClerkServer.Extensions;
using ClerkServer.RandomUserAPI;
using Microsoft.AspNetCore.Http;
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

		/// <summary>
		/// Retrieves all users from the database. Response is paginated. Accepts query parameters.
		/// </summary>
		/// <remarks>
		/// - **Query Parameters**
		/// - limit: number of users to be returned. limit should range between 1 and 100.
		/// - starting_after:
		/// - ending_before:
		/// - email:
		/// </remarks>
		[HttpGet("clerks")]
		[ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllUsers() {
			var users = await _repository.User.GetAllUsersAsync();

			return Ok(users);
		}

		/// <summary>
		/// Retrieve a user by user Id.
		/// </summary>
		/// <remarks>
		/// - id: long integer.
		/// </remarks>
		[HttpGet("clerks/{id}", Name = "GetUserById")]
		[ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetUserById(long id) {
			var user = await _repository.User.GetUserByIdAsync(id);
			if (user == null) {
				ModelState.AddModelError("Not Found", string.Format(APIMessages.UserIdNotFound, id));
				return BadRequest();
			}

			return Ok(user);
		}

		/// <summary>
		/// Retrieves random users from api.randomuser.me. Inserts unique users to database. 
		/// </summary>
		/// <remarks>
		/// - Required: users 0-5000. Number of results from **api.randomuser.me**.
		/// - Unique users are these have a unique email in the database.
		/// - Filters out duplicate emails from **api.randomuser.me** result.
		/// - Returns Service Unavailable (503) status code when **api.randomuser.me** quota exceeds.
		/// </remarks>
		[HttpPost("populate")]
		[ProducesResponseType(typeof(PopulateResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
		public async Task<IActionResult> Populate([FromBody]PopulateRequest request) {
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