using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.DTO.Result;
using TruckDemo_v1.Application.Managers;
using TruckDemo_v1.Domain.Entities.Identity;

namespace TruckDemo_v1.Application.UseCases.Users.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, Result<LoginResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtManager _jwtManager;

        public LoginHandler(UserManager<ApplicationUser> userManager, IJwtManager jwtManager)
        {
            _userManager = userManager;
            _jwtManager = jwtManager;
        }

        public async Task<Result<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                return "Usuario no encontrado";
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {

                var claims = await _userManager.GetClaimsAsync(user);

                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    claims.Add(new(JwtClaimTypes.Role, role));
                }

                string token = _jwtManager.GenerateJwt(user, claims);

                return new LoginResponse(token);
            }

            return "Usuario o contraseña incorrectos";
        }
    }
}
