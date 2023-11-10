using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.DTO.Identity.Users;

namespace TruckDemo_v1.Application.UseCases.Users.GetAllUsers
{
    public record GetAllUsersResponse(IEnumerable<GetAllUserItem> Items);
}
