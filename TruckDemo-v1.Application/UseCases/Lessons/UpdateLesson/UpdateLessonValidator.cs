using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.UseCases.Lessons.UpdateLesson
{
    public class UpdateLessonValidator : AbstractValidator<UpdateLessonRequest>
    {
        public UpdateLessonValidator() {
            RuleFor(x => x.LessonId).NotNull().NotEmpty().WithMessage("La Id de la lección a actualizar no debe ser nula ni vacia");
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("La lección a actualizar debe tener un titulo");
            RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage("La lección a actualizar debe tener contenido");
            RuleFor(x => x.SectionId).NotNull().NotEmpty().WithMessage("Se debe dar el id de seccion a la que pertenece la lección a actualizar");
            RuleFor(x => x.Order).NotNull().GreaterThan(0).WithMessage("Se debe ingresar un orden de la lección a actualizar y debe ser mayor a 0");
        }
    }
}
