using Infrastructure.Data.Queries.LoginQuery;
using Infrastructure.Data.Queries.RegisterQuery;
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
            if (user is null)
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
        [HttpPost("Register")] 
        public async Task<ActionResult> Register(RegisterQuery registerQuery)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    DisplayFirstName = registerQuery.FirstName,
                    DisplayLastName = registerQuery.LastName,
                    UserName = registerQuery.Login,
                    Email = registerQuery.Email,
                };
                var identityResult = await _userManager.CreateAsync(newUser, registerQuery.Password);
                if (identityResult.Succeeded)
                {
                    await _signInManager.CheckPasswordSignInAsync(newUser, registerQuery.Password, false);
                    var response = new { Message = "Register successful! Wait for confirmation." };
                    return Ok(response);
                }
            }
            return BadRequest();
        }
        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new {Message = "Logout successful!" });
        }
	}
}
