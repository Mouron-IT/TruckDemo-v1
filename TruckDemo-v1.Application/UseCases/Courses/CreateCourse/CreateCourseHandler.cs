using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.Data;
using TruckDemo_v1.Application.DTO.Result;
using TruckDemo_v1.Domain.Entities;

namespace TruckDemo_v1.Application.UseCases.Courses.CreateCourse
{
    public class CreateCourseHandler : IRequestHandler<CreateCourseRequest, Result<CreateCourseResponse>>
    {
        private readonly ITruckDemoContext _context;

        public CreateCourseHandler(ITruckDemoContext context)
        {
            _context = context;
        }

        public async Task<Result<CreateCourseResponse>> Handle(CreateCourseRequest request, CancellationToken cancellationToken)
        {
            
            Course course = new(request.Title,
                request.Content,
                DateTime.Now,
                request.Subtitle,
                null,
                DateTime.Now);

            await _context.Courses.AddAsync(course, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateCourseResponse(course.Id,
                course.Title,
                course.Content,
                course.CreatedAt,
                course.Subtitle);
        }
    }
}
