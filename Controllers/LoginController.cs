using BookWebApp.Business.Utilities;
using BookWebApp.Models;
using BookWebApp.Models.ViewModels.Login;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading.Tasks;

namespace BookWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login_VM login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(login.Username) ??
                           await _userManager.FindByEmailAsync(login.Username);
                
                if(user == null)
                {
                    // Hata mesajlarını ModelState'e ekleyerek View tarafında gösteriyoruz
                    ModelState.AddModelError("Error", "Username or Password is wrong");
                }
                else
                {
                    bool sifreDogruMu = await _userManager.CheckPasswordAsync(user, login.Password);
                    if (sifreDogruMu)
                    {
                        //Kullanıcı var ve sifre dogru ise..
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Username or Password is wrong");
                    }
                }  
            }
            return View(login);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserCreate_VM user)
        {
            if (ModelState.IsValid)
            {
                AppUser newUser = new AppUser
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Address = user.Address,
                    UserName = user.Username,
                    Email = user.Email,
                };
                var result = await _userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                { 
                    await _userManager.AddToRoleAsync(newUser, "User");
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    // Hata mesajlarını ModelState'e ekleyerek View tarafında gösteriyoruz
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(user);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
