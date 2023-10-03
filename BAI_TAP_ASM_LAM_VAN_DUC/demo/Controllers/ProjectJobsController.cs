using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demo.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System;
using Dapper;
using System.Data;
using demo.DTO;
using System.Collections.Generic;

namespace demo.Controllers
{
    [Authorize]
    public class ProjectJobsController : Controller
    {
        private readonly JIRAContext _context;

        public ProjectJobsController(JIRAContext context)
        {
            _context = context;
            
        }
        // GET: ProjectJobs
        public async Task<IActionResult> Index()
        {
            User _UserLogin = JsonSerializer.Deserialize<User>(HttpContext.User.FindFirst("UserLogin").Value);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("username", _UserLogin.Username);
            List<CustomJobs> jobs = _context.Database.GetDbConnection().Query<CustomJobs>("Proc_ListProjectJob", parameters, commandType: CommandType.StoredProcedure).ToList();
            _context.Database.CloseConnection();
            string message = TempData["Message"] as string;
            if (message != null && message.Length >0)
            {
                ViewBag.Message = message;
            }
            return View(jobs);
        }

        // GET: ProjectJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var projectJob = await _context.ProjectJobs
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (projectJob == null)
            {
                return NotFound();
            }
            return View(projectJob);
        }

        // GET: ProjectJobs/Create
        public IActionResult Create(int ProjectId, string ProjectKey)
        {
            ProjectJob project = new ProjectJob();
            project.ProjectId = ProjectId;
            project.ProjectKey = ProjectKey;
            return View(project);
        }

        // POST: ProjectJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,ProjectKey,ProjectId,TitleTask,DescriptionTask,TypeTask,DeadLineTask,PriorityTask,LevelTask,UserCreate,UserImplement,TaskCreateDate,TaskUpdateDate,StatusTask")] ProjectJob projectJob)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projectJob);
        }

        // GET: ProjectJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectJob = await _context.ProjectJobs.FindAsync(id);
            if (projectJob == null)
            {
                return NotFound();
            }
            return View(projectJob);
        }

        // POST: ProjectJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<dynamic> Edit(int id, [Bind("TaskId,UserCreate,ProjectKey,ProjectId,TitleTask,DescriptionTask,TypeTask,DeadLineTask,PriorityTask,LevelTask,UserImplement,TaskCreateDate,TaskUpdateDate,StatusTask")] ProjectJob projectJob)
        {

            try
            {


                ProjectJob job = _context.ProjectJobs.FirstOrDefault(x => x.TaskId == id);
                if (job != null)
                {
                    job.PriorityTask = projectJob.PriorityTask;
                    job.ProjectKey = projectJob.ProjectKey;
                    job.ProjectId = projectJob.ProjectId;
                    job.TitleTask = projectJob.TitleTask;
                    job.DescriptionTask = projectJob.DescriptionTask;
                    job.DeadLineTask = projectJob.DeadLineTask;
                    job.LevelTask = projectJob.LevelTask;
                    job.UserImplement = projectJob.UserImplement;
                    job.TaskUpdateDate = DateTime.Now;
                    _context.Update(job);
                    _context.SaveChanges();

                    return Ok(projectJob);

                }
                else
                {
                    return BadRequest("Không tìm thấy task : " + projectJob.TitleTask);

                }

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest("Đã xảy ra lỗi : " + ex.Message);
            }
        }

        public async Task<IActionResult> ChangeStatusTask(int TaskId,string StatusTask)
        {
          
            if (ModelState.IsValid)
            {
                try
                {
                    ProjectJob job = _context.ProjectJobs.FirstOrDefault(x => x.TaskId == TaskId);
                    if (job != null)
                    {
                        job.StatusTask = StatusTask;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        TempData["Message"] = "Không tìm thấy công việc cần thay đổi trạng thái làm.";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectJobExists(TaskId))
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
            return RedirectToAction(nameof(Index));
        }

        // GET: ProjectJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectJob = await _context.ProjectJobs
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (projectJob == null)
            {
                return NotFound();
            }

            return View(projectJob);
        }

        // POST: ProjectJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectJob = await _context.ProjectJobs.FindAsync(id);
            _context.ProjectJobs.Remove(projectJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectJobExists(int id)
        {
            return _context.ProjectJobs.Any(e => e.TaskId == id);
        }

       
    }
}
