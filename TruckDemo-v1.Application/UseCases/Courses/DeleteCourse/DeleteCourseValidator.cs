using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.UseCases.Courses.DeleteCourse
{
    public class DeleteCourseValidator : AbstractValidator<DeleteCourseRequest>
    {
        public DeleteCourseValidator() {
        
            RuleFor(x => x.CourseId).NotNull().NotEmpty().WithMessage("El id del curso a eliminar no debe ser nulo ni vacio");
        
        }
    }
}
