using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;

namespace Tesla.Ordering.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Abc()
        {
            return Content("Tesla.Ordering.Api");
        }

        [HttpGet]
        public IActionResult ShowRequestUri()
        {
            return Content(Request.GetDisplayUrl());
        }

        [HttpGet]
        public IActionResult ShowHeaders()
        {
            var sb = new StringBuilder();
            Request.Headers.ToList().ForEach(item =>
            {
                sb.AppendLine($"{item.Key}:{item.Value}");
            });

            return Content(sb.ToString());
        }
    }
}
