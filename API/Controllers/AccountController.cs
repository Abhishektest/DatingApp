using API.Data;
using API.DTO;
using API.Entites;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context,
                                 ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task <ActionResult<UserDto>> Register(ResigterDto resigterDto)
        {
            if (await UserExist(resigterDto.userName)) return BadRequest("User Already Exist");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = resigterDto.userName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(resigterDto.Password)),// We need to hash the code
                PasswordSallt = hmac.Key //Initlaise a new instance of Hmacsh class with a random generated key. so we set it to password salt
            };
            _context.Users.Add(user); //We are not adding anything to db ,Just we are tracking the same in EF.
            await _context.SaveChangesAsync(); //Now here we save the user in DB.	
            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                 .SingleOrDefaultAsync(x => x.UserName == loginDto.userName);
            if (user == null) return Unauthorized("Invalid username");
            using var hmac = new HMACSHA512(user.PasswordSallt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int i =0; i<computedHash.Length;i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

        }
        private async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
