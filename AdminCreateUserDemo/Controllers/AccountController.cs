using AdminCreateUserDemo.Data;
using AdminCreateUserDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminCreateUserDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login(int isCheck = 0)
        {
            ViewBag.IsCheck = isCheck;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    // Kiểm tra xem người dùng có cần thay đổi mật khẩu không
                    if (user.FirstLogin)
                    {
                        return RedirectToAction("ChangePassword", new { firstLogin = true });
                    }

                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("CreateUser", "Admin");
                    }

                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Login", "Account", new { isCheck = 2 }); //Trường hợp login sai thống tin
            }

            return RedirectToAction("Login", "Account", new { isCheck = 2 });
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword(bool firstLogin = false)
        {
            ViewBag.FirstLogin = firstLogin;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    user.FirstLogin = false;
                    await _userManager.UpdateAsync(user);

                    await _signInManager.RefreshSignInAsync(user);

                    // Check if user is an admin
                    bool isAdminResult = await _userManager.IsInRoleAsync(user, "Admin");

                    return Json(new
                    {
                        success = true,
                        isAdmin = isAdminResult,
                        message = "changed password successfully"
                    });
                }
                else
                {
                    string errorMessage = "Password change failed. Errors: ";
                    foreach (var error in result.Errors)
                    {
                        errorMessage += error.Description + " ";
                    }
                    return Json(new
                    {
                        success = false,
                        message = errorMessage
                    });
                }
            }

            return Json(new
            {
                success = false
            });
        }
    }
}