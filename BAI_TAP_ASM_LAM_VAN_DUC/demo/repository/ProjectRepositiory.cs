using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.repository
{
    public class ProjectRepositiory : IProjectRepository
    {
        public JIRAContext _db;
        public ProjectRepositiory(JIRAContext db)
        {
            _db = db;
        }
        public List<Project> ListProject()
        {
            throw new NotImplementedException();
        }
    }
}
