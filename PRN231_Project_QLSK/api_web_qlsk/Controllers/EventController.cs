using api_web_qlsk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace api_web_qlsk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EventController : ControllerBase
    {


        private readonly EventDBContext db = new EventDBContext();
        private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        [HttpGet("getTop12")]

        public IActionResult getTop12EventNewest(string username)
        {

            try
            {
                var events = db.Events.Where(x => x.User.Username == username && (x.EndTime > DateTime.Now || x.EndTime == null) && x.TrangThai == 1).OrderBy(x => x.StartTime).ToList();
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
                var events = db.Events.Where(x =>  x.TrangThai == 1).OrderByDescending(x => x.EventId).ToList();
                return Ok(events);

            }
            catch (Exception ex)
            {
                return BadRequest("thất bại");
            }



        }

        [HttpGet("{id}")]
        public IActionResult getEventById(int id)
        {
            try
            {
                var events = db.Events.FirstOrDefault(x => x.EventId == id && x.TrangThai == 1);

                if (events != null)
                {

                    return Ok(events);

                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest("thất bại");
            }

        }
        [HttpGet("Schedule/{id}")]
        public IActionResult getScheduleByEventId(int id)
        {
            try
            {
                var Schedules = db.Schedules.Where(x => x.EventId == id && x.TrangThai == 1).ToList();

                if (Schedules != null)
                {

                    return Ok(Schedules);

                }
                return NotFound();

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
            string anhImg = formData["image"];
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
            even.Image = anhImg;
            even.TrangThai = 1;
            db.Events.Add(even);
            db.SaveChanges();
            if (schedule.Count > 0)
            {
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
                    Schedule.TrangThai = 1;
                    db.Schedules.Add(Schedule);
                    db.SaveChanges();

                }
            }

            return Ok(new { success = true, message = "Thêm sự kiện thành công " });

        }

        [HttpPut]

        public async Task<IActionResult> updateEventAsync([FromForm] IFormCollection formData)
        {

            try
            {


                // Lấy các giá trị từ formData
                string id = formData["idEvent"];
                string title = formData["title"];
                string anhImg = formData["image"];
                string location = formData["location"];
                string startDate = formData["startDate"];
                string endDate = formData["endDate"];
                string description = formData["description"];
                string username = formData["username"];
                string scheduleJson = formData["schedule"];
                var schedule = JsonSerializer.Deserialize<List<DTO.ScheduleDTO>>(scheduleJson);
                string scheduleIdJson = formData["listId"];
                var listId = JsonSerializer.Deserialize<List<string>>(scheduleIdJson);

                var user = db.Users.FirstOrDefault(x => x.Username.Equals(username));

                var even = db.Events.FirstOrDefault(x => x.EventId == int.Parse(id));
                if (even != null)
                {
                    even.Title = title;
                    even.Location = location;
                    even.StartTime = DateTime.Parse(startDate);
                    even.EndTime = DateTime.Parse(endDate);
                    even.Description = description;
                    even.CreatedAt = DateTime.Now;
                    even.UserId = user.UserId;
                    even.Image = anhImg;
                    db.SaveChanges();
                }


                var listNotExit = new List<Schedule>();
                if (listId != null && listId.Count > 0)
                {
                    var AllEventByEvenId = db.Schedules.Where(x => x.EventId == even.EventId).ToList();
                    foreach (var f in AllEventByEvenId)
                    {
                        if (!listId.Contains(f.ScheduleId.ToString()))
                        {
                            listNotExit.Add(f);
                        }
                    }

                    db.RemoveRange(listNotExit);
                    db.SaveChanges();

                }


                if (schedule.Count > 0)
                {
                    foreach (var s in schedule)
                    {
                        var Schedule = new Schedule();
                        Schedule.Title = s.title;
                        Schedule.Location = s.location;
                        Schedule.StartTime = s.startTime;
                        Schedule.EndTime = s.endTime;
                        Schedule.CreatedAt = DateTime.Now;
                        Schedule.EventId = even.EventId;
                        Schedule.Description = s.description;

                        db.Schedules.Add(Schedule);
                        db.SaveChanges();

                    }
                }

            }
            catch
            {
                return BadRequest();
            }

            return Ok(new { success = true, message = "Cập nhật sự kiện thành công " });

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> delete(int id)
        {
            try
            {

                var even = db.Events.FirstOrDefault(x => x.EventId == id);
                if (even != null)
                {
                    even.TrangThai = 0;
                    db.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
