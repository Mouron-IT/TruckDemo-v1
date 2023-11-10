using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.DTO.Identity.Users
{
    public record GetAllUserItem(Guid Id, string Email,string role, string FirstName, string LastName);
}
