using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application.UseCases.Courses.CreateCourse
{
    public record CreateCourseResponse(Guid CourseId,string Title,
        string Content,
        DateTime CreatedAt,
        string? Subtitle
    );
}
