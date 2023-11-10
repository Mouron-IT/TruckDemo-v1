using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.UseCases.Lessons.GetBySection
{
    public class GetBySectionValidator : AbstractValidator<GetBySectionRequest>
    {
        public GetBySectionValidator() {
        
            RuleFor(x => x.SectionId).NotNull().NotEmpty().WithMessage("El id de la seccion no debe ser nulo ni vacio");

        }
    }
}
