using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerSipmle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthContoller : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthContoller(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(RegisterDto registerDto)
        {
            var user = new IdentityUser { UserName = registerDto.UserName, Email = registerDto.Email };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            throw new Exception(result.Errors.FirstOrDefault().Description);
        }
        public class RegisterDto
        {
            public string Email { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
