using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.Data;
using TruckDemo_v1.Application.DTO.Identity.Users;
using TruckDemo_v1.Application.DTO.Result;
using TruckDemo_v1.Domain.Entities.Identity;

namespace TruckDemo_v1.Application.UseCases.Users.GetAllUsers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, Result<GetAllUsersResponse>>
    {
        private readonly ITruckDemoContext _context;

        public GetAllUsersHandler(ITruckDemoContext context)
        {
            _context = context;
        }

        public async Task<Result<GetAllUsersResponse>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.Users
            .Join(_context.UserRoles, user => user.Id, userRole => userRole.UserId, (user, userRole) => new { user, userRole })
            .Join(_context.Roles, ur => ur.userRole.RoleId, role => role.Id, (ur, role) => new { ur.user, role })
            .Join(_context.UserClaims, ur => ur.user.Id, claim => claim.UserId, (ur, claim) => new { ur.user, ur.role.Name, claim })
            .ToListAsync();


            var users = result.Select(x => new GetAllUserItem(
                    x.user.Id,
                    x.user.Email,
                    x.Name,
                    x.claim.ClaimType == "Name" ? x.claim.ClaimValue : null,
                    x.claim.ClaimType == "FamilyName" ? x.claim.ClaimValue : null

                )
            );
            return new GetAllUsersResponse( users );
            
        }
    }
}
