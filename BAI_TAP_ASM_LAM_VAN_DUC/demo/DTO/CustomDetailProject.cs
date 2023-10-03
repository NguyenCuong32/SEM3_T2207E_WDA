using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.DTO
{
    public class CustomDetailProject
    {
        public Project project { get; set; }
        public List<ProjectJob> projectJobs { get; set; }
    }
}
