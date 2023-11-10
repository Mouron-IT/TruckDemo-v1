using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application.UseCases.Courses.UpdateCourse
{
    public record UpdateCourseRequest(Guid CourseId,
        string Title,
        string Content,
        string? Subtitle) : IRequest<Result<UpdateCourseResponse>>;
} 
