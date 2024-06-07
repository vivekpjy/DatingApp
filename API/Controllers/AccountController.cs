using API.Data;

using System.Security.Cryptography;
using System.Text;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using API.Interfaces;
using API.Services;

namespace API.Controllers{
    public class AccountController : BaseApiController{

        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService){
            _context = context;
             _tokenService = tokenService;
        }

        [HttpPost("register")] //POST: api/account/register

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){
            if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser{
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x=>
            x.UserName == loginDto.Username.ToLower());

            if(user == null) return Unauthorized("invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int i=0;i<ComputeHash.Length;i++){
                if(ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        //checking user exist or not in db
        private async Task<bool> UserExists(string username){
            return await _context.Users.AnyAsync(x=>x.UserName == username.ToLower());
        }
    }
}