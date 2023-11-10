using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.UseCases.Courses.CreateCourse
{
    public class CreateCourseValidator : AbstractValidator<CreateCourseRequest>
    {
      public CreateCourseValidator() {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("El titulo del nuevo curso no puede ser nulo ni vacio");
            RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage("El contenido del nuevo curso no puede ser nulo ni vacio");
      }
    }
}
