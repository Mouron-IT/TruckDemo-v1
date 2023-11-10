using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Domain.Entities;
using TruckDemo_v1.Domain.Entities.Identity;

namespace TruckDemo_v1.Application.Data
{
    public interface ITruckDemoContext
    {
        DbSet<ApplicationUser> Users { get; }

        DbSet<Role> Roles { get; }
        DbSet<RoleUser> UserRoles { get; }
        DbSet<ClaimUser> UserClaims { get; }
        DbSet<Course> Courses { get; }

        DbSet<Lesson> Lessons { get; }

        DbSet<Section> Sections { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
