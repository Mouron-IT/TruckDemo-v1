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
using TruckDemo_v1.Application.UseCases.Users.CreateUser;
using TruckDemo_v1.Domain.Entities.Identity;
using TruckDemo_v1.Domain.Enum;

namespace TruckDemo_v1.Application.UseCases.Users.CreateDefaultUser
{
    public class CreateDefaultUserHandler : IRequestHandler<CreateDefaultUserRequest, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public CreateDefaultUserHandler(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result> Handle(CreateDefaultUserRequest request, CancellationToken cancellationToken)
        {
            var existingUser = _userManager.FindByNameAsync("admin@deere.com.ar").Result;
            var existingRole = _roleManager.RoleExistsAsync(RoleName.Admin.ToString());
            if(await existingRole== false)
            {
                Role role = new();
                role.Name = RoleName.Admin.ToString();
                await _roleManager.CreateAsync(role);
            }

            if (existingUser == null)
            {
                ApplicationUser user = new("admin@deere.com.ar")
                {
                    Email = "admin@deere.com.ar"
                };
                var claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Name, "DefaultName"),
                    new Claim(JwtClaimTypes.FamilyName, "DefaultLastName"),
                };

                var result = _userManager.CreateAsync(user, "Admin1234#").Result;
                await _userManager.AddClaimsAsync(user, claims);

                if (result.Succeeded)
                {
                    
                  await _userManager.AddToRoleAsync(user, RoleName.Admin.ToString());

                   
                }

                return true;
            }
            return false;
        }
    }
}

