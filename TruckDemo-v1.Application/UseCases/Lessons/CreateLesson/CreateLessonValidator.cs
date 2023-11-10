using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.UseCases.Lessons.CreateLesson
{
    public class CreateLessonValidator : AbstractValidator<CreateLessonRequest>
    {
        public CreateLessonValidator() {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("La lección debe tener un titulo");
            RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage("La lección debe tener contenido");
            RuleFor(x => x.SectionId).NotNull().NotEmpty().WithMessage("Se debe dar el id de seccion a la que pertenece la lección");
            RuleFor(x => x.Order).NotNull().GreaterThan(0).WithMessage("Se debe ingresar un orden de la lección y debe ser mayor a 0");         
        }
    }
}
