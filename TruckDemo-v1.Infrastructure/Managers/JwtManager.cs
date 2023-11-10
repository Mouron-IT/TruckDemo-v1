using IdentityModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.Managers;
using TruckDemo_v1.Domain.Entities.Identity;
using TruckDemo_v1.Infraestructure.Options;

namespace TruckDemo_v1.Infraestructure.Managers
{
    internal class JwtManager : IJwtManager
    {
        private readonly SecurityOptions _securityOptions;

        public JwtManager(IOptions<SecurityOptions> securityOptions)
        {
            _securityOptions = securityOptions.Value;
        }

        public string GenerateJwt(ApplicationUser user, IEnumerable<Claim> claims)
        {

            var key = Encoding.ASCII.GetBytes
            (/*_securityOptions.SecretKey*/ "Equipo13");
            

            List<Claim> tokenClaims = new(claims)
            {
                new(JwtClaimTypes.Email, user.Email),
                new(JwtClaimTypes.Subject, user.Id.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(tokenClaims),
                Expires = DateTime.UtcNow.AddDays(7),
                //Issuer = _securityOptions.Issuer,
                Issuer = "Equipo13 ",
                //Audience = _securityOptions.Audience,
                Audience = "Backend",
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }

    }
}
