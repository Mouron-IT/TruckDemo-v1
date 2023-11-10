using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.UseCases.Users.CreateUser
{
    public record CreateUserResponse(
        string Email,
        string FirstName,
        string LastName);
}
