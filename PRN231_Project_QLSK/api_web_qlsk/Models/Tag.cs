using System;
using System.Collections.Generic;

namespace api_web_qlsk.Models
{
    public partial class Tag
    {
        public Tag()
        {
            Events = new HashSet<Event>();
            EventsNavigation = new HashSet<Event>();
        }

        public int TagId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Event> EventsNavigation { get; set; }
    }
}
