using System;
using System.Collections.Generic;

#nullable disable

namespace demo.Models
{
    public partial class Project
    {
        public int ProjectId { get; set; }
        public string ProjectKey { get; set; }
        public string ProjectName { get; set; }
        public string ProjectLead { get; set; }
        public string UserCrate { get; set; }
    }
}
