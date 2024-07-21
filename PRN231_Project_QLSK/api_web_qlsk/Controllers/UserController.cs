using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using api_web_qlsk.Models;
namespace api_web_qlsk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
		private readonly EventDBContext db = new EventDBContext();

		[HttpGet("getAllUser")]
		public IActionResult getAllNewest()
		{
			try
			{
				var user = db.Users.ToList();
				return Ok(user);

			}
			catch (Exception ex)
			{
				return BadRequest("thất bại");
			}



		}


	}
}
