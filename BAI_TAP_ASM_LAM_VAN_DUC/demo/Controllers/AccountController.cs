using demo.Models;
using demo.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace demo.Controllers
{
    public class AccountController : Controller
    {
        public readonly IuserServices userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, UserServices userServices)
        {
            userService = userServices;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> LoginAsync(string username , string password,string ReturnUrl)
        {
            if (ReturnUrl == null || ReturnUrl == "/")
            {
                ReturnUrl = "/projectJobs/Index";

            }
            if (username == null || username.Length ==0)
            {
                ViewBag.Message = "Vui lòng nhập username.";
                return View();
            }
            if (password == null  || password.Length ==0)
            {
                ViewBag.Message = "Vui lòng nhập mật khẩu";
                return View();
            }
            User user = userService.GetUserByUsername(username);
            if (user != null)
            {
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, user.Password);
                if (isPasswordCorrect)
                {
                  
                  
                    List<Claim> claims = new List<Claim>() {
                          new Claim("UserLogin", JsonSerializer.Serialize(user)),
                     };
                    //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                    var principal = new ClaimsPrincipal(identity);
                    //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(10),
                        IsPersistent = true,
                    });

                    return Redirect(ReturnUrl);
                }
                else
                {
                    // Mật khẩu không hợp lệ, từ chối đăng nhập
                    ViewBag.Message = "Mật khẩu không hợp lệ.";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Tài khoản không tồn tại.";
                return View();

            }



        }

        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut    
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            return LocalRedirect("/Account/Login");
        }
    }
}
