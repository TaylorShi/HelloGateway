using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Tesla.Mobile.Gateway.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> Login()
        {
            return await Task.FromResult("请先登录");
        }

        /// <summary>
        /// 使用Cookie进行登录
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CookieLogin(string userName)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim("Name", userName));
            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return Content("login");
        }

        /// <summary>
        /// 使用JWT进行登录
        /// </summary>
        /// <param name="securityKey"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> JwtLogin([FromServices]SymmetricSecurityKey securityKey, string userName)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Name", userName));

            // 加密算法选择HmacSha256
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // 构造JWT的Token
            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );
            // 生成JWT Token的字符串
            var t = new JwtSecurityTokenHandler().WriteToken(token);
            return Content(t);
        }
    }
}
