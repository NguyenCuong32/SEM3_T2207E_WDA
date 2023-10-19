using AJAX_Example_API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AJAX_Example_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [EnableCors("CorsPolicy")]
        //[EnableCors("AnotherCorsPolicy")]
        //[DisableCors] // Disable cors
        [HttpGet]
        public IEnumerable<PersonalDetail> GetAllStudents()
        {
            List<PersonalDetail> students = new List<PersonalDetail>
           {
           new PersonalDetail{
                              RegNo = "2023-0001",
                              Name = "Nguyễn Thanh Bình",
                              Address = "Hà Nội",
                              PhoneNo = "9849845061",
                              AdmissionDate = DateTime.Now
                              },
           new PersonalDetail{
                              RegNo = "2023-0002",
                              Name = "Nguyễn Thị Hoa",
                              Address = "Bắc Ninh",
                              PhoneNo = "9849845062",
                              AdmissionDate = DateTime.Now
                             },
           };
            return students;
        }
    }
}
