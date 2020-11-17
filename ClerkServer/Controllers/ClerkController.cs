using System.Linq;
using System.Threading.Tasks;
using ClerkServer.Contracts;
using ClerkServer.Domain;
using ClerkServer.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClerkServer.Controllers {

	[ApiController]
	[Route("1.0")]
	public class ClerkController : ControllerBase {

		private readonly IRepositoryWrapper _repository;

		public ClerkController(IRepositoryWrapper repository) {
			_repository = repository;
		}

		[HttpGet("clerks")]
		public async Task<IActionResult> GetAllUsers() {
			var users = await _repository.User.GetAllUsersAsync();

			return Ok(users);
		}

		[HttpGet("clerks/{id}" , Name = "GetUserById")]
		public async Task<IActionResult> GetUserById(long id) {
			var user = await _repository.User.GetUserByIdAsync(id);
			if (user == null) return BadRequest();

			return Ok(user);
		}

		[HttpPost("clerks")]
		public async Task<IActionResult> CreateUser(UserDto request) {
			if (!ModelState.IsValid) {
				return BadRequest(new {
					error = "Bad Request sent from Client",
					message = ModelState.Values.SelectMany(v => v.Errors)
				});
			}

			var user = await _repository.User.FindByEmailAsync(request.Email);
			if (user != null) {
				return BadRequest(new {
					error = "Bad Request sent from Client",
					message = $"Email {request.Email} is in use"
				});
			}
			
			var createdUser = _repository.User.CreateUser(request);
			await _repository.SaveAsync();
			
			return CreatedAtRoute("GetUserById", new { id = createdUser.Id }, createdUser);
		}
		
		[HttpPost("populate")]
		public async Task<IActionResult> Post() {
			return Ok(new User());
		}

	}

}