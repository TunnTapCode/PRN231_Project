using System;
using System.Collections.Generic;

namespace api_web_qlsk.Models
{
    public partial class Event
    {
        public Event()
        {
            Schedules = new HashSet<Schedule>();
            Tags = new HashSet<Tag>();
        }

        public int EventId { get; set; }
        public int? UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Location { get; set; }
        public int? TagId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Image { get; set; }

        public virtual Tag? Tag { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
