using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application.UseCases.Lessons.CreateLesson
{
    public record CreateLessonRequest(string Title,
        string Content,
        Guid SectionId,
        int Order) : IRequest<Result<CreateLessonResponse>>;
}
