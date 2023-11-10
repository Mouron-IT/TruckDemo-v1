using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.DTO.Section
{
    public record SectionDTO(Guid Id,
        string Title,
        string Content,
        Guid CourseId,
        int Order);

}
