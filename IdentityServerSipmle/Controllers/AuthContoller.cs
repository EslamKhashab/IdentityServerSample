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
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthContoller(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user!=null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, loginDto.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
          
            throw new Exception();
        }
        public class LoginDto
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class RegisterDto
        {
            public string Email { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }

    }
}
