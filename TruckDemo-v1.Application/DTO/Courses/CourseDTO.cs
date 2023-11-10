using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.DTO.Courses
{
    public record CourseDTO(Guid CourseId,
        string Title,
        string Content,
        DateTime CreatedAt,
        string? Subtitle,
        DateTime? ModifiedAt,
        DateTime? PublishAt);

}
