using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.DTO.Result;
using TruckDemo_v1.Domain.Entities.Identity;
using TruckDemo_v1.Domain.Enum;

namespace TruckDemo_v1.Application.UseCases.Users.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, Result<CreateUserResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public CreateUserHandler(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<Result<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var existingRole = _roleManager.RoleExistsAsync(RoleName.Basic.ToString());

            if (await existingRole == false)
            {
                Role role = new();
                role.Name = RoleName.Basic.ToString();
                await _roleManager.CreateAsync(role);
            }

            ApplicationUser newUser = new(request.Email)
            {
                Email = request.Email
            };

            IEnumerable<Claim> claim = UserPersonalInfo(request);

            await _userManager.CreateAsync(newUser, request.Password);

            await _userManager.AddToRoleAsync(newUser, RoleName.Basic.ToString());

            await _userManager.AddClaimsAsync(newUser, claim);

            return new CreateUserResponse(newUser.Email, request.FirstName,
                request.LastName);
        }

        private IEnumerable<Claim> UserPersonalInfo(CreateUserRequest request)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, request.FirstName),
                new Claim(JwtClaimTypes.FamilyName, request.LastName),
            };

            return claims;
        }
    }
}
