using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api_web_qlsk.Models;
using Microsoft.AspNetCore.Authorization;
namespace api_web_qlsk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly EventDBContext db = new EventDBContext();

        
        [HttpGet("getAll")]
       
        public IActionResult getUserLogin()
        {

          var user = db.Users.ToList();
            return Ok(user);
        }

    }
}
