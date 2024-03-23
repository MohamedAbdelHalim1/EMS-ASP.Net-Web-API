using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackendLibrary.Repositories.Contracts;
using BackendLibrary.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace ProjectApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServicesController(IUserAccount UserAccount) : ControllerBase
    {
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await UserAccount.GetAllUsersAsync();
            if (result.Flag)
            {
                return Ok(result); // User updated successfully
            }
            else
            {
                return BadRequest(result); // Bad request, update failed
            }
        }
        [Authorize(Policy = "CombinedPolicy")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            var result = await UserAccount.GetSpecificUserAsync(userId);
            if (result.Flag)
            {
                return Ok(result); // User updated successfully
            }
            else
            {
                return BadRequest(result); // Bad request, update failed
            }
        }
        [Authorize(Policy = "CombinedPolicy")]
        [HttpPost("user")]
        public async Task<IActionResult> UpdateUserAsync(int id , UpdateUser updateUser)
        {
            var result = await UserAccount.UpdateUserAsync(id ,updateUser);
            if (result.Flag)
            {
                return Ok(result); // User updated successfully
            }
            else
            {
                return BadRequest(result); // Bad request, update failed
            }
        }
        [Authorize(Policy = "CombinedPolicy")]
        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            var result = await UserAccount.DeleteUserAsync(userId);

            if (result.Flag)
            {
                return Ok(result); // User deleted successfully
            }
            else
            {
                return NotFound(result); // User not found, return a 404 Not Found response
            }
        }





    }
}
