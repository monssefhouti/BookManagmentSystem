using BookManagment.Models.Entities;
using BookManagment.Models.ViewModels;
using BookManagment.Models.ViewModels.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookManagment.Controllers
{

    public class AccountsController : Controller
    {
        private readonly UserManager<CustomUser> userManager;
        private readonly SignInManager<CustomUser> signInManager;
        
        public AccountsController(UserManager<CustomUser> userManager,SignInManager<CustomUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        //Logout
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            var model = new AccountRegisterViewModel();
            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(AccountRegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                CustomUser user = new()
                {
                    UserName = model.Username,
                    FullName = model.FullName,
                    Age = model.Age,
                    Gender = model.Gender,
                    Email = model.Email,
                    Telephone=model.Telephone
                };
                var Res = await userManager.CreateAsync(user, model.Password);
                if(Res.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false) ;
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(Res);
            }
            return View(model);
        }
        #endregion

        #region Logout
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Accounts");
        }
        #endregion

        #region Login
        [Route("Account/Login")]
        [Route("Accounts/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(AccountLoginViewModel model, string returnUrl)
        {
            if(ModelState.IsValid)
            {         
                var Res=await signInManager.PasswordSignInAsync(model.Username, model.Password,model.RememberMe,false);
                if(Res.Succeeded)
                {   if(!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                       
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please,check your username | Password");
                }
            }
            return View(model);
        }
        #endregion
        #region Extra Methods
        private void AddErrors(IdentityResult identityResult)
        {
            foreach (var errors in identityResult.Errors)
            {
                ModelState.AddModelError("",errors.Description);
            }
        }
        #endregion

        #region Profil
        [HttpGet]
        public async Task<IActionResult> Profil(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                CustomUser user = await userManager.FindByIdAsync(id);
                if(user!=null)
                {
                    ProfilViewModel model = new()
                    {
                        //FullName = user.FullName,
                        //Username = model.Username,
                        //Email=model.Email,
                        //Password=user.PasswordHash,
                        //ConfirmPassword=user.PasswordHash,  
                    };
                }
            }

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Profil(ProfilViewModel model)
        {
            if(ModelState.IsValid)
            {
                
            }
            return View(model);
        }
        #endregion
    }
}
