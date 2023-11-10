using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.UseCases.Lessons.GetById
{
    public class GetByIdValidator : AbstractValidator<GetByIdRequest>
    {
        public GetByIdValidator() {
        
            RuleFor(x=>x.LessonId).NotNull().NotEmpty().WithMessage("El id de la lección a buscar no debe ser nulo ni vacio");
        }
    }
}
