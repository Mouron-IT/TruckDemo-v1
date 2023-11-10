using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.Data;
using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application.UseCases.Lessons.GetById
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, Result<GetByIdResponse>>
    {
        private readonly ITruckDemoContext _context;
        public GetByIdHandler(ITruckDemoContext context)
        {
            _context = context;
        }

        public async Task<Result<GetByIdResponse>> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            var lesson = await _context.Lessons.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.LessonId);

            if(lesson == null) {
                return "La lección con el id otorgado no existe";
            }

            return new GetByIdResponse(lesson.Id,lesson.Title,lesson.Content,lesson.SectionId,lesson.Order);
        }
    }
}
