using System;
using System.Collections.Generic;

namespace api_web_qlsk.Models
{
    public partial class Schedule
    {
        public int ScheduleId { get; set; }
        public int? EventId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Location { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? TrangThai { get; set; }

        public virtual Event? Event { get; set; }
    }
}
