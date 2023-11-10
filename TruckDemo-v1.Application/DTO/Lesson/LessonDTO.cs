using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.DTO.Lesson
{
    public record LessonDTO(Guid LessonId,
        string Title,
        string Content,
        Guid SectionId,
        int Order);
}
