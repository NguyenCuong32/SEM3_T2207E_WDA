using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.EntityFrameworkCore;
using demo.Models;
using demo.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace demo.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly JIRAContext _context;
        private readonly IConfiguration _configuration;

        public ProjectsController(JIRAContext context , IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.ToListAsync());
        }


        public async Task<dynamic> ListProduct(string tukhoa)
        {
            return await _context.Projects.Where(x => x.ProjectKey.Contains(tukhoa) || x.ProjectName.Contains(tukhoa)).ToListAsync();
        }

        // GET: Projects/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CustomDetailProject project = new CustomDetailProject();
            project.project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            project.projectJobs = await _context.ProjectJobs
                            .Where(m => m.ProjectId == id).ToListAsync();
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProjectId,ProjectKey,ProjectName,ProjectLead,UserCrate")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,ProjectKey,ProjectName,ProjectLead,UserCrate")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public dynamic ListProductByUser()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                User _UserLogin = JsonSerializer.Deserialize<User>(HttpContext.User.FindFirst("UserLogin").Value);
                connection.Open();
                var parameters = new { username = _UserLogin.Username };
                var result = connection.Query<dynamic>("Proc_GetListProjectForLayout", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            //var thongTinChung = _context.Database.Query<dynamic>("Proc_HQ_ThongTinChungExportExcel", new
            //{
            //    @username = _UserLogin.Username,
            //}
            //, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }
        
    }

    
}
