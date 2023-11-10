using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.Data;
using TruckDemo_v1.Application.DTO.Courses;
using TruckDemo_v1.Application.DTO.Lesson;
using TruckDemo_v1.Application.DTO.Result;
using TruckDemo_v1.Application.DTO.Section;
using TruckDemo_v1.Domain.Entities;

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
            var courses = await _context.Courses.Include(x => x.Sections)
                .ThenInclude(x => x.Lessons).AsNoTracking()
                
                .Select(c => new CourseDTO(c.Id,
                c.Title,
                c.Content,
               c.CreatedAt,
                c.Subtitle,
                c.Sections.Select(s => new SectionDTO(s.Id,
                s.Title,
                s.Content,
                s.CourseId,
                s.Order,

                s.Lessons.Select(l => new LessonDTO(
                l.Id,
                l.Title,
                "",
                l.SectionId,
                l.Order,
                l.GameCode))))))
                
                .ToListAsync();
            
            if(!courses.Any() || courses == null)
            {
                return "No existen cursos disponibles";
            }

            

            return new GetCoursesResponse(courses);
        }
    }
}

