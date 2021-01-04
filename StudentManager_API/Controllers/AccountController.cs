using Infrastructure.Data.Queries.LoginQuery;
using Infrastructure.Data.Queries.RegisterQuery;
using Infrastructure.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManager_Core.Identity;
using System.Threading.Tasks;
using Infrastructure.Data.Constants;

namespace StudentManager_API.Controllers
{
    /// <summary>
    /// User account management
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
		private readonly IJwtGenerator _generator;

        /// <summary>
        /// Configuring controller with help of DI
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="jwtGenerator"></param>
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _generator = jwtGenerator;
        }

        /// <summary>
        /// Allow user to log in
        /// </summary>
        /// <remarks>Allows anonymous users</remarks>
        /// <param name="request">Contains login data (Email, Password)</param>
        /// <returns>If login successful, return JWT token</returns>
        /// <response code="200">If login success</response>
        /// <response code="400">If model validation error</response>
        /// <response code="401">If login failed</response>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginQuery request)
		{
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user is not null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                    if (result.Succeeded)
                    {

                        var response = new
                        {
                            access_token = _generator.CreateJwtToken(user, await _userManager.GetRolesAsync(user)),
                            username = user.UserName,
                        };
                        return Ok(response);
                    }
                }
                return Unauthorized();
            }
            return ValidationProblem();
        }

        /// <summary>
        /// Register of new user
        /// </summary>
        /// <remarks>Allows anonymous users</remarks>
        /// <param name="registerQuery">Contains register data</param>
        /// <returns>
        /// Message with register status
        /// </returns>
        /// <response code="200">If register succes</response>
        /// <response code="401">If model errors</response>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterQuery registerQuery)
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
                    await _userManager.AddToRoleAsync(newUser, IdentityRoleConstants.STUDENT);
                    var response = new { Message = "Register successful! Wait for confirmation." };
                    return Ok(response);
                }
            }
            return ValidationProblem();
        }
	}
}
