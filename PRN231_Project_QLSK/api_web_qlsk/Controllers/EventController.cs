using api_web_qlsk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_web_qlsk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {


        private readonly EventDBContext db = new EventDBContext();

        [HttpGet("getTop12")]

        public IActionResult getTop12EventNewest(string username)
        {

            try
            {
                    var events = db.Events.Where(x => x.User.Username == username && x.EndTime > DateTime.Now || x.EndTime == null).OrderBy(x => x.StartTime).Take(12).ToList();
                    return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest("thất bại");
            }

        }
        [HttpGet("getAllNew")]
        public IActionResult getAllNewest()
        {
            try
            {
                var events = db.Events.OrderByDescending(x => x.EventId).ToList();
                return Ok(events);

            }
            catch (Exception ex)
            {
                return BadRequest("thất bại");
            }



        }
    }
}
