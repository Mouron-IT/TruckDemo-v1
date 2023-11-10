using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Domain.Entities.Identity;

namespace TruckDemo_v1.Application.Managers
{
    public interface IJwtManager
    {
        string GenerateJwt(ApplicationUser user, IEnumerable<Claim> claims);
    }
}
