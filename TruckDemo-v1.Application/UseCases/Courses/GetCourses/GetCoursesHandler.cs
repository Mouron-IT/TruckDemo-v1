using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.Data;
using TruckDemo_v1.Application.DTO.Courses;
using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application.UseCases.Courses.GetCourses
{
    public class GetCoursesHandler : IRequestHandler<GetCoursesRequest, Result<GetCoursesResponse>>
    {
        private readonly ITruckDemoContext _context;

        public GetCoursesHandler(ITruckDemoContext context)
        {
            _context = context;
        }

        public async Task<Result<GetCoursesResponse>> Handle(GetCoursesRequest request, CancellationToken cancellationToken)
        {
            var courses = await _context.Courses.AsNoTracking().ToListAsync();
            
            if(!courses.Any() || courses == null)
            {
                return "No existen cursos disponibles";
            }

            var courseResponse = courses.Select(x => new CourseDTO (
                x.Id,
                x.Title,
                x.Content,
                x.CreatedAt,
                x.Subtitle,
                x.LastUpdatedAt,
                x.PublishedAt)
            );

            return new GetCoursesResponse(courseResponse);
        }
    }
}

