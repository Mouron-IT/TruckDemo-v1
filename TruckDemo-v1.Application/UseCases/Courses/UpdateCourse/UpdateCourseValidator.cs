using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.UseCases.Courses.UpdateCourse
{
    public class UpdateCourseValidator : AbstractValidator<UpdateCourseRequest>
    {
        public UpdateCourseValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty().NotNull().WithMessage("El id del curso a actualizar no puede ser nulo ni vacio");
            RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("El titulo del curso a actualizar no debe ser vacio ni nulo");
            RuleFor(x => x.Content).NotEmpty().NotNull().WithMessage("El contenido del curso a actualizar no debe ser vacio ni nulo");
        }
    }
}
