using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.UseCases.Lessons.CreateLesson
{
    public record CreateLessonResponse(Guid LessonId,
        string Title,
        string Content,
        Guid SectionId,
        int Order);
}
