using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tesla.Mobile.Gateway.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        /// <summary>
        /// 使用默认的身份验证方案
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Default()
        {
            return Content("Bank Account");
        }

        /// <summary>
        /// 使用Cookie身份验证方案
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult Cookie()
        {
            return Content(User.FindFirst("Name").Value);
        }

        /// <summary>
        /// 使用JWT身份验证方案
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Jwt()
        {
            return Content(User.FindFirst("Name").Value);
        }

        /// <summary>
        /// 同时使用Cookie和JWT身份验证方案
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + "," + CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult Hybird()
        {
            return Content(User.FindFirst("Name").Value);
        }
    }
}
