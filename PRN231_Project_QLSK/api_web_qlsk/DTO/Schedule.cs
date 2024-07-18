using System;
using System.Collections.Generic;

namespace api_web_qlsk.DTO
{
    public  class ScheduleDTO
    {
        public int ScheduleId { get; set; }
        public int? EventId { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public string? location { get; set; }
        public DateTime? CreatedAt { get; set; }

    
    }
}
