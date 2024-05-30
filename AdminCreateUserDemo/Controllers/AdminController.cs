using AdminCreateUserDemo.Data;
using AdminCreateUserDemo.Models;
using AdminCreateUserDemo.Services.NewFolder.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminCreateUserDemo.Controllers
{
    
    public sealed class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AdminController(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser(int isCreated = 0)
        {
            ViewBag.IsCreated = isCreated;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Admin", new { userId = user.Id, token = token }, protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(model.Email, "Confirm your email", $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.");

                    return RedirectToAction("CreateUser", new { isCreated = 1 });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                var temporaryPassword = "B@" + (Guid.NewGuid().ToString("N").Substring(0, 4));
                var a = await _userManager.AddPasswordAsync(user, temporaryPassword);
                await _emailSender.SendEmailAsync(user.Email, "Temporary Password", $"Your temporary password is {temporaryPassword}");

                return RedirectToAction("Login","Account", new {isCheck = 1});
            }

            return View("Error");
        }
    }
}
