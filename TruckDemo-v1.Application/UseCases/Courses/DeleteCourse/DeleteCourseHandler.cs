using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.Data;
using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application.UseCases.Courses.DeleteCourse
{
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseRequest, Result>
    {
        private readonly ITruckDemoContext _context;

        public DeleteCourseHandler(ITruckDemoContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteCourseRequest request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.CourseId);
            if (course == null)
            {
                return "El curso con el id otorgado no existe";
            }

            _context.Courses.Remove(course);

            await _context.SaveChangesAsync(cancellationToken);

            return "Curso eliminado satisfactoriamente";
        }
    }
}
