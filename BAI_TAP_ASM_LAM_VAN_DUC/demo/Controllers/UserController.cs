using demo.DTO;
using demo.Models;
using demo.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace demo.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        private readonly IuserServices _userServices;

        public UserController(ILogger<UserController> logger, UserServices userServices)
        {
            _userServices = userServices;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult UsersSystem(string tukhoa)
        {
            string message = TempData["MessageDelete"] as string;
            if (!string.IsNullOrEmpty(message))
            {
                // Sử dụng message ở đây
                ViewBag.Message = message;
            }

            List<User> list = new List<User>();
            if (tukhoa == null || tukhoa.Length == 0)
            {
                list = _userServices.GetAllUser();
            }
            else
            {
                if (tukhoa.Equals("All"))
                {
                    list = _userServices.GetAllUser();
                }
                else
                {
                    list = _userServices.FindUser(tukhoa);
                }
            }

            return View(list);

        }

        [Authorize]
        [Route("User/DeleteUser/{username}")]
        public IActionResult DeleteUser(string username)
        {
            response res = _userServices.DeleteUser(username);
            if (res.status == 200)
            {
                return Redirect("/User/UsersSystem");
            }
            else
            {
                TempData["MessageDelete"] = username;
                return Redirect("/User/UsersSystem");
            }
        }





        public IActionResult CreateUser()
        {
            return View();
        }




        [HttpPost]
        public IActionResult CreateUser(string url,User user)
        {
            response res = _userServices.CreateUser(user);
            if (res.status == 200)
            {
                ViewBag.Message = res.message;
                return Redirect(url);
            }
            else
            {
                ViewBag.Message = res.message;

                return View();
            }
        }




        [Authorize]
        [Route("/User/Edit/{username}")]
        public IActionResult EditUser(string username)
        {
            User res = _userServices.GetUserByUsername(username);

            return View(res);
        }


        [Authorize]
        [HttpPost]
        [Route("/User/Edit/{username}")]
        public IActionResult EditUser(User user, string username)
        {
            response res = _userServices.UpdateUser(user, username);
            if (res.status == 200)
            {
                ViewBag.Message = res.message;
                return Redirect("/User/UsersSystem");
            }
            else
            {
                User user1 = _userServices.GetUserByUsername(username);
                ViewBag.Message = res.message;
                return View(user1);
            }
        }


        [HttpGet]
        [Authorize]

        [Route("/Profile")]
        public IActionResult Profile()
        {
            User _UserLogin = JsonSerializer.Deserialize<User>(HttpContext.User.FindFirst("UserLogin").Value);
                User user = _userServices.GetUserByUsername(_UserLogin.Username);
            return View(user);
        }

        [HttpPost]
        [Authorize]

        [Route("/Profile")]
        public async Task<IActionResult> Profile([Bind("Active", "Username", "Avatar", "Email","FullName")] User user,IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string newFileName = user.Username + Path.GetExtension(file.FileName); // Thay đổi "newFileName" và "ext" theo nhu cầu của bạn
                string filePath = Path.Combine("wwwroot", "avatar", newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                user.Avatar = "/avatar/"+newFileName;
            }
            user.Active = true;
            response res = _userServices.UpdateUser(user, user.Username);
            if (res.status == 200)
            {
                ViewBag.Message = res.message;
                ViewBag.StatusCode = res.status.ToString();
                return View(user);
            }
            else
            {
                User user1 = _userServices.GetUserByUsername(user.Username);

                ViewBag.Message = res.message;
                ViewBag.StatusCode = res.status.ToString();

                return View(user1);
            }
        }




        public dynamic SearchUser(string tukhoa)
        {
            return _userServices.FindUser(tukhoa);
        }



    }
}
