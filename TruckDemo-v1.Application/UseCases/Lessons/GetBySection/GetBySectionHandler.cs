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
using TruckDemo_v1.Domain.Entities;

namespace TruckDemo_v1.Application.UseCases.Lessons.GetBySection
{
    public class GetBySectionHandler : IRequestHandler<GetBySectionRequest, Result<GetBySectionResponse>>
    {
        private readonly ITruckDemoContext _context;

        public GetBySectionHandler(ITruckDemoContext context)
        {
            _context = context;
        }

        public async Task<Result<GetBySectionResponse>> Handle(GetBySectionRequest request, CancellationToken cancellationToken)
        {
            var section = await _context.Sections.AsNoTracking().Include(x => x.Lessons).FirstOrDefaultAsync(x => x.Id == request.SectionId);

            if(section == null) {

                return "La seccion con el id otorgado no existe";

            }
            var allLessons = section.Lessons.Select(x => new LessonDTO(
                x.Id,
                x.Title,
                x.Content,
                x.SectionId,
                x.Order)
                ).ToList();

            if (!allLessons.Any() || allLessons == null) {

                return "No existen lecciones vinculadas a la seccion requerida";
            
            }
            return new GetBySectionResponse(allLessons);
        }
    }
}
