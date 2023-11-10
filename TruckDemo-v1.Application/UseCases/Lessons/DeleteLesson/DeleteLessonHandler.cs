using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.Data;
using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application.UseCases.Lessons.DeleteLesson
{
    public class DeleteLessonHandler : IRequestHandler<DeleteLessonRequest, Result>
    {
        private readonly ITruckDemoContext _context;

        public DeleteLessonHandler(ITruckDemoContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteLessonRequest request, CancellationToken cancellationToken)
        {
            var deleteLesson = await _context.Lessons.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.LessonId);
            
            if(deleteLesson == null)
            {
                return "No existe la lección con el id dado";
            }

            _context.Lessons.Remove(deleteLesson);

            await _context.SaveChangesAsync(cancellationToken);

            return "Lección eliminada satisfactoriamente";
        }
    }
}
