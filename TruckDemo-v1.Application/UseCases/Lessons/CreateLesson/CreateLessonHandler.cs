using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.Data;
using TruckDemo_v1.Application.DTO.Result;
using TruckDemo_v1.Domain.Entities;

namespace TruckDemo_v1.Application.UseCases.Lessons.CreateLesson
{
    public class CreateLessonHandler : IRequestHandler<CreateLessonRequest, Result<CreateLessonResponse>>
    {
        private readonly ITruckDemoContext _context;

        public CreateLessonHandler(ITruckDemoContext context)
        {
            _context = context;
        }

        public async Task<Result<CreateLessonResponse>> Handle(CreateLessonRequest request, CancellationToken cancellationToken)
        {

            var section = await _context.Sections.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.SectionId);

            if(section==null)
            {
                return "No existe la seccion con el id otorgado";
            }

            Lesson newLesson = new(request.Title,request.Content,request.SectionId,request.Order);

            await _context.Lessons.AddAsync(newLesson, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateLessonResponse(newLesson.Id, newLesson.Title, newLesson.Content, newLesson.SectionId, newLesson.Order);
        }
    }
}
