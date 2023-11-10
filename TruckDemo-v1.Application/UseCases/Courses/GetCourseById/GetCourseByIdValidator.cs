using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.UseCases.Courses.GetCourseById
{
    public class GetCourseByIdValidator : AbstractValidator<GetCourseByIdRequest>
    {
        public GetCourseByIdValidator() { 
        
            RuleFor(X => X.CourseId).NotNull().NotEmpty().WithMessage("El id del curso a buscar no debe ser vacio ni nulo");
        
        }
    }
}
