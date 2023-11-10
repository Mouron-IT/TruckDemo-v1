using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.Data;
using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application.UseCases.Courses.UpdateCourse
{
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseRequest, Result<UpdateCourseResponse>>
    {
        private readonly ITruckDemoContext _context;

        public UpdateCourseHandler(ITruckDemoContext context)
        {
            _context = context;
        }

        public async Task<Result<UpdateCourseResponse>> Handle(UpdateCourseRequest request, CancellationToken cancellationToken)
        {
            var courseUpdate = await _context.Courses.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.CourseId,cancellationToken);
           
            if (courseUpdate == null)
            {
                return "No existe el curso con la Id Dada";
            }

            courseUpdate.Title = request.Title;
            courseUpdate.Content = request.Content;
            courseUpdate.Subtitle = request.Subtitle;
            courseUpdate.LastUpdatedAt = DateTime.Now;

            _context.Courses.Update(courseUpdate);

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateCourseResponse(
                courseUpdate.Id,
                courseUpdate.Title,
                courseUpdate.Content,
                courseUpdate.CreatedAt,
                courseUpdate.Subtitle,
                courseUpdate.LastUpdatedAt,
                courseUpdate.PublishedAt);
        }
    }
}
