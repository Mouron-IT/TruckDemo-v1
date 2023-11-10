using IdentityModel;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using TruckDemo_v1.Application.Data;
using TruckDemo_v1.Application.DTO.Result;
using TruckDemo_v1.Application.Managers;
using TruckDemo_v1.Application.UseCases.Users.Login;
using TruckDemo_v1.Domain.Entities;
using TruckDemo_v1.Domain.Entities.Identity;

namespace TruckDemo_v1.Application.UseCases.Users.LoginOculus
{
    public class LoginOculusRequestHandler : IRequestHandler<LoginOculusRequest, Result<LoginOculusResponse>>
    {
        private readonly ITruckDemoContext _context;
        private readonly IJwtManager _jwtManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginOculusRequestHandler(ITruckDemoContext context, IJwtManager jwtManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _jwtManager = jwtManager;
            _userManager = userManager;
        }

        public async Task<Result<LoginOculusResponse>> Handle(LoginOculusRequest request, CancellationToken cancellationToken)
        {
            UserOculusCode? oculusCode = await _context.UserOculusCodes.AsNoTracking().SingleOrDefaultAsync(x => x.OculusCode == request.Code, cancellationToken);
            if (oculusCode == null)
            {
                return "El codigo no existe";
            }

            var user = await _context.Users.AsNoTracking().SingleAsync(x => x.Id == oculusCode.UserId, cancellationToken);


            var claims = await _userManager.GetClaimsAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new(JwtClaimTypes.Role, role));
            }

            string token = _jwtManager.GenerateJwt(user, claims);

            return new LoginOculusResponse(token);

        }
    }
}
