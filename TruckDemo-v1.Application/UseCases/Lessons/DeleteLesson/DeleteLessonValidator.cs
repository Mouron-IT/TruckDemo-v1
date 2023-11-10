using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.UseCases.Lessons.DeleteLesson
{
    public class DeleteLessonValidator :AbstractValidator<DeleteLessonRequest>
    {
        public DeleteLessonValidator() {
        
            RuleFor(x => x.LessonId ).NotNull().NotEmpty().WithMessage("El id de la lección a eliminar no debe ser nulo ni vacio");
        
        }
    }
}
