using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application.UseCases.Users.ApplyRoles
{
    public record ApplyRoleRequest(Guid UserId, string Role) : IRequest<Result>;
}
