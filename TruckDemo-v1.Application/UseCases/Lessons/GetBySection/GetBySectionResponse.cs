using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.DTO.Lesson;

namespace TruckDemo_v1.Application.UseCases.Lessons.GetBySection
{
    public record GetBySectionResponse(IEnumerable<LessonDTO> Lessons);
}
