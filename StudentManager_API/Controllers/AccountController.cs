using Infrastructure.Data.Queries.LoginQuery;
using Infrastructure.Services;
using Infrastructure.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManager_Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StudentManager_API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
		private readonly IJwtGenerator _generator;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _generator = jwtGenerator;
        }

        [HttpPost("Login")]
		public async Task<ActionResult> Login(LoginQuery request)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);
			if (user == null)
			{
				return Unauthorized();
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

			if (result.Succeeded)
			{
                var response = new
                {
                    access_token = _generator.CreateJwtToken(user),
                    username = user.UserName,
                };
                return Ok(response);
			}
            else
            {
                return Unauthorized();
            }
		}
	}
}
