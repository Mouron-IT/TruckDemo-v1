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


            List<Claim> tokenClaims = new(claims)
            {
                new(JwtClaimTypes.Email, user.Email),
                new(JwtClaimTypes.Subject, user.Id.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("S3cr3t_K3y!.123_S3cr3t_K3y!.123.123_S3cr3t_K3y!.123"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: "truck-backend",
                audience: "backend",
                claims: tokenClaims,
                expires: DateTime.Now.AddMinutes(720),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);


            return jwt;
        }

    }
}
