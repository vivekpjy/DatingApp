using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services {
    public class TokenService: ITokenService{

        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config){
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            // var tokenKey = config["TokenKey"];
            // if (string.IsNullOrEmpty(tokenKey) || Encoding.UTF8.GetByteCount(tokenKey) < 64)
            // {
            //     throw new ArgumentException("Token key must be at least 64 bytes long.");
            // }
            // _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        }
        public string CreateToken(AppUser user){
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
           
        }
    }
}