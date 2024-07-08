using System;
using System.Collections.Generic;

namespace api_web_qlsk.Models
{
    public partial class User
    {
        public User()
        {
            Events = new HashSet<Event>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
