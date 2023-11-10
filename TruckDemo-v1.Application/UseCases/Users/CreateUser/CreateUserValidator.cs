using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Application.UseCases.Users.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator() {

            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Ingrese un email valido.");

            RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(6).WithMessage("Ingrese una contraseña valida con al menos 6 caracteres");

            RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("Ingrese un nombre");
            RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("Ingrese un apellido");


        }
    }
}
