using api_web_qlsk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace api_web_qlsk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {


        private readonly EventDBContext db = new EventDBContext();
        private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

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
        [HttpPost]

        public async Task<IActionResult> SaveEventAsync([FromForm] IFormCollection formData)
        {
            // Lấy các giá trị từ formData
            string title = formData["title"];
            IFormFile anhImg = formData.Files["image"];
            string location = formData["location"];
            string startDate = formData["startDate"];
            string endDate = formData["endDate"];
            string description = formData["description"];
            string username = formData["username"];
            string scheduleJson = formData["schedule"];
            var schedule = JsonSerializer.Deserialize<List<Schedule>>(scheduleJson);

            var user = db.Users.FirstOrDefault(x => x.Username.Equals(username));

            var even = new Event();
            even.Title = title;
            even.Location = location;
            even.StartTime = DateTime.Parse(startDate);
            even.EndTime = DateTime.Parse(endDate);
            even.Description = description;
            even.CreatedAt = DateTime.Now;
            even.UserId = user.UserId;
            db.Events.Add(even);
            db.SaveChanges();


            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }

            string imagePath = null;
            if (anhImg != null && anhImg.Length > 0)
            {
                string uniqueFileName = Path.GetRandomFileName() + Path.GetExtension(anhImg.FileName);
                imagePath = Path.Combine(_storagePath, uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await anhImg.CopyToAsync(stream);
                }
                even.Image = imagePath;
                db.SaveChanges();

            }

            foreach (var s in schedule)
            {
                var Schedule = new Schedule();
                Schedule.Title = s.Title;
                Schedule.Location = s.Location;
                Schedule.StartTime = s.StartTime;
                Schedule.EndTime = s.EndTime;
                Schedule.CreatedAt = DateTime.Now;
                Schedule.EventId = even.EventId;
                Schedule.Description = s.Description;

                db.Schedules.Add(Schedule);
                db.SaveChanges();

            }

            return Ok(new { success = true, message = "Data received successfully" });

        }

    }
}
