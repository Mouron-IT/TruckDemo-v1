using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.UseCases.Users.ApplyRoles
{
    public class ApplyRoleValidator : AbstractValidator<ApplyRoleRequest>
    {
        public ApplyRoleValidator() {

            RuleFor(x => x.UserId).NotEmpty().NotNull().WithMessage("El id de usuario no debe ser nulo ni vacio");
            RuleFor(x => x.Role).NotEmpty().NotNull().WithMessage("El rol no debe estar vacio ni ser nulo, ademas");
        
        }
    }
}
