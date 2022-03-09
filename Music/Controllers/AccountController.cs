using IdentityManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Music.Controllers;
using Music.Models;

namespace IdentityManager.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ILogger<HomeController> _logger;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }


        //SING UP
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Register(string? returnurl = null)
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                //create roles
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            RegisterViewModel registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }


        //SING UP
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.RoleSelected != null && model.RoleSelected.Length > 0 && model.RoleSelected == "Admin")
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    if (User.IsInRole("User"))
                    {
                        return RedirectToAction("Payment", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Payment", "Home");
                    }

                }
                AddErrors(result);
            }
            return View(model);
        }


        //SING IN
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }


        //SING IN
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginViewModel model, string? returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnurl);
                }
                //**********************************
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction("VerifyAuthenticatorCode", new { returnurl = returnurl ,RememberMe=model.RememberMe});
                }
                if (result.IsLockedOut)
                    return View("Lockout");
                }
                else
                {
                 ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                _logger.LogInformation("Invalid login attempt.");
                _logger.LogError("Invalid login attempt.");
            
                return View(model);
                }
               
            return View(model);
        }


        //LOG OUT CODE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        //Forgot Password

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {

            /**
             *   if (ModelState.IsValid)
              {
                  var user = await _userManager.FindByEmailAsync(model.Email);
                  if (user == null)
                  {
                      return RedirectToAction("ForgotPasswordConfirmation");
                  }

                  var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                  var callbackurl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                  await _emailSender.SendEmailAsync(model.Email, "Reset Password - Identity Manager",
                      "Please reset your password by clicking here: <a href=\"" + callbackurl + "\">link</a>");

                  return RedirectToAction("ForgotPasswordConfirmation");
              }

              **/
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveAuthenticator()
        {
            var user = await _userManager.GetUserAsync(User);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            await _userManager.SetTwoFactorEnabledAsync(user, false);
            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> EnableAuthenticator()
        {

            var user = await _userManager.GetUserAsync(User);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            var token = await _userManager.GetAuthenticatorKeyAsync(user);
            var model = new TwoFactorAuthenticationViewModel() { Token = token };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EnableAuthenticator(TwoFactorAuthenticationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var succeeded = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, model.Code);
                if (succeeded)
                {
                    await _userManager.SetTwoFactorEnabledAsync(user, true);
                }
                else
                {
                    ModelState.AddModelError("Verify", "Your two factor auth code could not be avalidated.");
                    
                    return View(model);
                }

            }
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult AuthenticatorConfirmation()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> VerifyAuthenticatorCode(bool rememberMe, string? returnUrl = null)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(new VerifyAuthenticatorViewModel { ReturnUrl = returnUrl });
            // return View(new VerifyAuthenticatorViewModel { ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyAuthenticatorCode(VerifyAuthenticatorViewModel model)
        {
            model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(model.Code, model.RememberMe, rememberClient: false);

            //var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(model.Code, model.RememberMe, rememberClient: false);

            if (result.Succeeded)
            {
                return LocalRedirect(model.ReturnUrl);
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Code.");
                return View(model);
            }

        }
    }
}
