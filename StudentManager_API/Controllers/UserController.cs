using Infrastructure.Data.Constants;
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

        /// <summary>
        /// Configuring of controller
        /// </summary>
        /// <param name="userManager"></param>
        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
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
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/User/id
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
