using System;
using System.Collections.Generic;

#nullable disable

namespace demo.Models
{
    public partial class PhanQuyenProject
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int ProjectId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
