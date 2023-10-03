using System;
using System.Collections.Generic;

#nullable disable

namespace demo.Models
{
    public partial class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool? Active { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
    }
}
