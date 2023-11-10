using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.DTO.Result;
using TruckDemo_v1.Domain.Entities.Identity;

namespace TruckDemo_v1.Application.UseCases.Users.ApplyRoles
{
    public class ApplyRoleHandler : IRequestHandler<ApplyRoleRequest, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplyRoleHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result> Handle(ApplyRoleRequest request, CancellationToken cancellationToken)
        {
            IList<string> getRoles = Enum.GetNames(typeof(Domain.Enum.RoleName));

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user == null)
            {
                return "Usuario no encontrado";
            }
            bool isInRole = await _userManager.IsInRoleAsync(user, request.Role);

            if (isInRole)
            {
                return "El usuario ya esta asignado al rol especificado";
            }
            else
            {
                await _userManager.AddToRoleAsync(user, request.Role);
            }

            return true;


        }
    }
}
