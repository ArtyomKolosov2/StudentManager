using Infrastructure.Data.Queries.RoleQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManager_Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data.Constants;

namespace StudentManager_API.Controllers
{
    /// <summary>
    /// Role managment
    /// </summary>
    /// <response code="401">If user is unauthorized</response>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Configuring controller with help of DI
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public RoleController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/Role
        /// <summary>
        /// List of all roles
        /// </summary>
        /// <returns>List of roles</returns>
        /// <response code="401">If user is unauthorized</response>
        [HttpGet]
        public ActionResult<IEnumerable<IdentityRole>> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }

        // GET: api/Role/id
        /// <summary>
        /// Get a role by id
        /// </summary>
        /// <param name="id">Role id</param>
        /// <returns>Founded role</returns>
        /// <response code="200">If search was successful</response>
        /// <response code="401">If user is unauthorized</response>
        /// <response code="404">If the role wasn't found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<IdentityRole>> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is not null)
            {
                return Ok(role);
            }
            return NotFound();
        }

        // POST: api/Role/name
        /// <summary>
        /// Create a new role
        /// </summary>
        /// <remarks>Access: admin, moderator, developer</remarks>
        /// <param name="name">Name of the new role</param>
        /// <response code="200">If creation was successful</response>
        /// <response code="400">If the params were invalid</response>
        /// <response code="401">If user is unauthorized</response>
        /// <response code="403">If user doesn't have access</response>
        [Authorize(Roles = IdentityRoleConstants.ADMIN + "," +
            IdentityRoleConstants.MODERATOR + "," +
            IdentityRoleConstants.DEV)]
        [HttpPost("{name}")]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return BadRequest();
        }

        // DELETE: api/Role/id
        /// <summary>
        /// Delete a role by id
        /// </summary>
        /// <remarks>Access: admin, moderator, developer</remarks>
        /// <param name="id">Role id</param>
        /// <response code="200">If deletion was succesful</response>
        /// <response code="401">If user is unauthorized</response>
        /// <response code="403">If user doesn't have access</response>
        /// <response code="404">If the role wasn't found</response>
        [Authorize(Roles =
            IdentityRoleConstants.ADMIN + "," +
            IdentityRoleConstants.MODERATOR + "," +
            IdentityRoleConstants.DEV)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return NotFound();
        }

        // PUT: api/Role/GetUserRoles/userId
        /// <summary>
        /// Get user roles
        /// </summary>
        /// <remarks>Access: admin, moderator, developer</remarks>
        /// <param name="userId">User id</param>
        /// <returns>Info about user and his roles</returns>
        /// <response code="200">If search was succesful</response>
        /// <response code="401">If user is unauthorized</response>
        /// <response code="403">If user doesn't have access</response>
        /// <response code="404">If the user wasn't found</response>

        [HttpGet("[action]/{userId}")]
        [Authorize(Roles =
            IdentityRoleConstants.ADMIN + "," +
            IdentityRoleConstants.MODERATOR + "," +
            IdentityRoleConstants.DEV)]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
           
            var user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var model = new ChangeRoleQuery
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return Ok(model);
            }

            return NotFound();
        }

        /// <summary>
        /// Editing user roles
        /// </summary>
        /// <param name="changeRolesQuery"></param>
        /// <param name="id"></param>
        /// <remarks>Access: admin, moderator, developer</remarks>
        /// <response code="200">If edit was succesful</response>
        /// <response code="400">If the user ID from request is not equal to the ID found by the user manager</response>
        /// <response code="401">If user is unauthorized</response>
        /// <response code="403">If user doesn't have access</response>
        /// <response code="404">If the user wasn't found</response>
        [Authorize(Roles = 
            IdentityRoleConstants.ADMIN + "," + 
            IdentityRoleConstants.MODERATOR + "," + 
            IdentityRoleConstants.DEV)]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> EditUserRoles(ChangeRoleQuery changeRolesQuery, string id)
        {
            var user = await _userManager.FindByEmailAsync(changeRolesQuery.UserEmail);

            if (user is not null)
            {
                if (user.Id == id)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var allRoles = _roleManager.Roles.ToList();
                    var addedRoles = changeRolesQuery.UserRoles.Except(userRoles);
                    var removedRoles = userRoles.Except(changeRolesQuery.UserRoles);

                    await _userManager.AddToRolesAsync(user, addedRoles);

                    await _userManager.RemoveFromRolesAsync(user, removedRoles);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }

            return NotFound();
        }
    }
}
