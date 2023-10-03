using Basketball_Shoes.Data;
using Basketball_Shoes.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BasketballShoes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext  context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult login()
        {

            return View();
        }

        [HttpPost]
        //public async Task<IActionResult> login(string username, string password)
        //{
        //    var user = _context.User.FirstOrDefault(u => u.username == username);
        //    if (user == null)
        //    {
        //        return Ok("Email khong ton tai");
        //    }

        //    if (user.password != password)
        //    {
        //        return Ok("mat khau khong dung");
        //    }

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.Email),

            //};

            //var claimsIdentity = new ClaimsIdentity(
            //    claims, CookieAuthenticationDefaults.AuthenticationScheme);


            //await HttpContext.SignInAsync(
            //    CookieAuthenticationDefaults.AuthenticationScheme,
            //    new ClaimsPrincipal(claimsIdentity));
            //;
            //return Redirect("/home");
            //return View();
        //}

        public IActionResult checkout()
        {
           
            return View();
        }

        public IActionResult info(int id)
        {
            var product = _context.Product.FirstOrDefault(p => p.id == id);
            if(product == null)
            {
                return NotFound();
            }
            else
            {
                return View(product);
            }
            
            
        }

        public IActionResult register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> register(string username, string password)
        {
            var newAccount = new User
            {
                username = username,
                password = password

            };
            _context.Add(newAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction("login");
        }


        [HttpGet]
        public IActionResult product()
        {
            var product = _context.Product.ToList();
            return View(product);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}