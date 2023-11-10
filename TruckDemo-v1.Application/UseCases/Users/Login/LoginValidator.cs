using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.UseCases.Users.Login
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator() {

            RuleFor(x => x.Username).NotEmpty().EmailAddress().NotNull().WithMessage("Ingrese un Email.");
            RuleFor(X => X.Password).NotEmpty().NotNull().WithMessage("Ingrese un password");

        }
    }
}
