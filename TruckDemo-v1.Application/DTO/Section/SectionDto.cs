using TruckDemo_v1.Application.DTO.Lesson;

namespace TruckDemo_v1.Application.DTO.Section
{
    public record SectionDTO(Guid Id,
        string Title,
        string Content,
        Guid CourseId,
        int Order,
        IEnumerable<LessonDTO> Lessons);

}
