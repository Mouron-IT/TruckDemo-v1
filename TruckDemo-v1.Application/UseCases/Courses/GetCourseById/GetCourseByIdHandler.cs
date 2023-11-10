using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.Data;
using TruckDemo_v1.Application.DTO.Lesson;
using TruckDemo_v1.Application.DTO.Result;
using TruckDemo_v1.Application.DTO.Section;
using TruckDemo_v1.Domain.Entities;

namespace TruckDemo_v1.Application.UseCases.Courses.GetCourseById
{
    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdRequest, Result<GetCourseByIdResponse>>
    {
        private readonly ITruckDemoContext _context;

        public GetCourseByIdHandler(ITruckDemoContext context)
        {
            _context = context;
        }

        public async Task<Result<GetCourseByIdResponse>> Handle(GetCourseByIdRequest request, CancellationToken cancellationToken)
        {
            var Course = await _context.Courses.AsNoTracking()
                .Include(x => x.Sections)
                .ThenInclude(x => x.Lessons)
                .FirstOrDefaultAsync(x => x.Id == request.CourseId);


            if (Course == null)
            {
                return "El curso con el id otorgado no existe";
            }

            var seccions = Course.Sections.Select(x => new SectionDTO(
                x.Id,
                x.Title,
                x.Content,
                x.CourseId,
                x.Order)).ToList();

            var allLessons = Course.Sections.SelectMany(s => s.Lessons.Select(x => new LessonDTO(
                x.Id,
                x.Title,
                x.Content,
                x.SectionId,
                x.Order))
                ).ToList();

            return new GetCourseByIdResponse(Course.Id,
                Course.Title,
                Course.Content,
                Course.CreatedAt,
                Course.Subtitle,
                Course.LastUpdatedAt,
                Course.PublishedAt,
                seccions,
                allLessons
                );
        }
    }
}
