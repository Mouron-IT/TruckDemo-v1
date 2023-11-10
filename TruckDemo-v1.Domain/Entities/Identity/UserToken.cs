using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Domain.Entities.Identity
{
    public class UserToken : IdentityUserToken<Guid>
    {
    }
}
