using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.Data;
using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application.UseCases.Lessons.UpdateLesson
{
    public class UpdateLessonHandler : IRequestHandler<UpdateLessonRequest, Result<UpdateLessonResponse>>
    {
        private readonly ITruckDemoContext _context;

        public UpdateLessonHandler(ITruckDemoContext context)
        {
            _context = context;
        }

        public async Task<Result<UpdateLessonResponse>> Handle(UpdateLessonRequest request, CancellationToken cancellationToken)
        {
            var lessonUpdate = await _context.Lessons.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.LessonId);

            if(lessonUpdate == null)
            {
                return "No existe la lección con la id otorgada";
            }

            var section = await _context.Sections.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.SectionId);

            if(section == null)
            {
                return "No existe la seccion con el id otorgado";
            }

            lessonUpdate.Title = request.Title;
            lessonUpdate.Content = request.Content;
            lessonUpdate.SectionId = request.SectionId;
            lessonUpdate.Order = request.Order;

            _context.Lessons.Update(lessonUpdate);

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateLessonResponse(lessonUpdate.Id,
                lessonUpdate.Title,
                lessonUpdate.Content,
                lessonUpdate.SectionId,
                lessonUpdate.Order);
        }
    }
}
