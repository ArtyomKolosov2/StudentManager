using Infrastructure.Data.Constants;
using Infrastructure.Data.Queries.UserQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManager_Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManager_API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Access: admin, moderator, developer</remarks>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [Authorize(Roles = IdentityRoleConstants.ADMIN + "," +
            IdentityRoleConstants.MODERATOR + "," +
            IdentityRoleConstants.DEV)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// Configuring of controller
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: api/User
        /// <summary>
        /// Get all users
        /// </summary>
        /// <remarks>Access: admin, moderator, developer</remarks>
        /// <returns>List of all users</returns>
        /// <response code="200">If ok</response>
        /// <response code="401">If user is unauthorized</response>
        /// <response code="403">If user doesn't have access</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _userManager.Users.ToListAsync());
        }

        // GET api/User/id
        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <remarks>Access: admin, moderator, developer</remarks>
        /// <returns>List of all users</returns>
        /// <response code="200">If ok</response>
        /// <response code="401">If user is unauthorized</response>
        /// <response code="403">If user doesn't have access</response>
        /// <response code="204">If user doesn't found</response>
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                return Ok(user);
            }
         
            return NoContent();
        }

        // POST api/User
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userQuery"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody]CreateUserQuery userQuery)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    DisplayFirstName = userQuery.FirstName ?? string.Empty,
                    DisplayLastName = userQuery.LastName ?? string.Empty,
                    UserName = userQuery.Login,
                    IsRegistrationConfirmed = true,
                    Email = userQuery.Email,
                };
                var identityResult = await _userManager.CreateAsync(newUser, userQuery.Password);
                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newUser, userQuery.UserRoles);
                    return Ok();
                }
                else
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return ValidationProblem();
        }

        // PUT api/User/id
        /// <summary>
        /// Update exisiting user
        /// </summary>
        /// <remarks>Doesn't updates user roles. Check Roles contoroller</remarks>>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <response code="200">If ok</response>
        /// <response code="401">If user is unauthorized</response>
        /// <response code="403">If user doesn't have access</response>
        /// <response code="204">If user doesn't found</response>
        [HttpPut("{id}")]
        public void UpdateUser(string id, [FromBody] string value)
        {
        }

        // DELETE api/User/5
        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">If ok</response>
        /// <response code="401">If user is unauthorized</response>
        /// <response code="403">If user doesn't have access</response>
        /// <response code="204">If user doesn't found</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is not null)
            {
                var deleteResult = await _userManager.DeleteAsync(user);
                if (deleteResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    foreach (var error in deleteResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return NoContent();
        }
    }
}
