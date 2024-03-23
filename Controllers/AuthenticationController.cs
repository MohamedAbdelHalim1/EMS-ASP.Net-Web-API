using BackendLibrary.Repositories.Contracts;
using BackendLibrary.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //this is called primary constractor
    public class AuthenticationController(IUserAccount UserAccount) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> CreateAsync(Register user)
        {
            if (user == null) return BadRequest("Model is Empty");
            var result = await UserAccount.CreateAsync(user);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignInAsync(Login user)
        {
            if (user == null) return BadRequest("Model is Empty");
            var result = await UserAccount.SignInAsync(user);
            return Ok(result);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenInfo refreshToken)
        {
            if (refreshToken is null) return BadRequest("Model is empty");
            var result = await UserAccount.RefreshTokenAsync(refreshToken);
            return Ok(result);
        }
    }
}
