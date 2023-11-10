using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.DTO.Lesson;
using TruckDemo_v1.Application.DTO.Section;
using TruckDemo_v1.Domain.Entities;

namespace TruckDemo_v1.Application.UseCases.Courses.GetCourseById
{
    public record GetCourseByIdResponse(Guid CourseId,
        string Title,
        string Content,
        DateTime CreatedAt,
        string? Subtitle,
        DateTime? ModifiedAt,
        DateTime? PublishAt,
        IEnumerable<SectionDTO>Sections,
        IEnumerable<LessonDTO> Lessons);
}
