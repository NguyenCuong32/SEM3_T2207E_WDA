using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.repository
{
    interface IProjectRepository
    {
        public List<Project> ListProject();
    }
}
