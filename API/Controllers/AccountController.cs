using API.Dto;
using API.Extensions;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers
{
    public class AccountController(SignInManager<AppUser> signIn,ITokenService _tokenService) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register (RegisterDto register)
        {
            var user = new AppUser
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName = register.Email
            };
            var result = await signIn.UserManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }
                
            return Ok();
        }
        [HttpPost("login")]
        public async Task<ActionResult<AppUserDto>> Login (LoginDto login)
        {
            var user = await signIn.UserManager.FindByEmailAsync(login.Email);
            if (user == null) return Unauthorized();
            var result = await signIn.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded) return BadRequest();
            return new AppUserDto()
            { 
                Token = _tokenService.CreateToken(user),
            };
        }
        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await signIn.SignOutAsync();
            return NoContent();
        }
        [HttpGet("info-user")]
        [Authorize]
        public async Task<ActionResult> GetUserInfo()
        {
            if (User.Identity?.IsAuthenticated==false) return NotFound("Not Authorized");
            //var user = await signIn.UserManager.GetUserByEmailWithAddress(User);
            var user = await signIn.UserManager.GetUserByEmailWithAddress(User);
            if (user == null) return Unauthorized();
            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                Address = user.Address?.ToDto()
            });
        }
        [HttpGet]   
        public ActionResult GetAuthState()
        {
            return Ok(new
            {
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false 
            });
        }
        [HttpPost("address")]
        [Authorize]
        public async Task<ActionResult<Address>> CreateAddress(AddressDto addressDto)
        {
            var user = await signIn.UserManager.GetUserByEmail(User);
            if (user.Address == null)
            {
                user.Address = addressDto.ToEntity();
            }
            else 
            {
                user.Address.UpdateFromDto(addressDto);
            }
            var result =await signIn.UserManager.UpdateAsync(user);
            if(!result.Succeeded) return BadRequest("Problem updating address");
            return Ok(user.Address.ToDto());
           
        }

    }
}
