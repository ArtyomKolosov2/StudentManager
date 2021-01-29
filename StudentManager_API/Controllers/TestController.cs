using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManager_API.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("test")]
        [Authorize]
        public ActionResult<string> GetResult()
        {
            return "tesssssstttttttt";
        }

        [AllowAnonymous]
        [HttpGet("test_arr")]
        public IEnumerable<string> GetStrings() => new List<string>
        {
            "string1",
            "string2",
            "string3",
            "string4"
        };
    }
}
