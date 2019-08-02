using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using UserAuthenticaton.Web.Models;
using UserAuthenticaton.Web.Services;

namespace UserAuthenticaton.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService loginService;
        private ICacheService cacheService;

        public LoginController(ILoginService loginService, ICacheService cacheService)
        {
            this.loginService = loginService;
            this.cacheService = cacheService;
        }

        public IActionResult Index(string returnUrl)
        {
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Authenticate(EndUser endUser)
        {
            try
            {
                AuthenticationResponse result = await loginService.LogIn(endUser);
                if (result != null)
                {
                    if (result.Authenticated)
                    {
                        this.cacheService.SetToken(result.AccessToken);
                        if (TempData["ReturnUrl"] != null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());
                        }
                        else
                        {
                            return Json(new { Message = "Login succeed" });
                        }
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}