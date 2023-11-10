using MediatR;

using Microsoft.EntityFrameworkCore;

using TruckDemo_v1.Application.Data;
using TruckDemo_v1.Application.DTO.Result;
using TruckDemo_v1.Domain.Entities;

namespace TruckDemo_v1.Application.UseCases.Users.GenerateOculusCode
{
    public class GenerateOculusCodeRequestHandler : IRequestHandler<GenerateOculusCodeRequest, Result<GenerateOculusCodeResponse>>
    {
        private readonly ITruckDemoContext _context;
        public GenerateOculusCodeRequestHandler(ITruckDemoContext context)
        {
            _context = context;
        }

        public async Task<Result<GenerateOculusCodeResponse>> Handle(GenerateOculusCodeRequest request, CancellationToken cancellationToken)
        {
           var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user is null)
                return "El usuario no existe";

            Random random = new Random();
            string code = "";
            bool codigoExistente = true;

            while (codigoExistente)
            {
                code = GenerateNewCode();
                codigoExistente = await ExistsCodeInDatabase(code);
            }

            UserOculusCode? oculusCode = await _context.UserOculusCodes.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            if(oculusCode != null)
            {
                oculusCode.OculusCode = code;
                _context.UserOculusCodes.Update(oculusCode);

            }
            else
            {
                oculusCode = new(user.Id, code);
                _context.UserOculusCodes.Add(oculusCode);

            }

            
            await _context.SaveChangesAsync(cancellationToken);

            return new GenerateOculusCodeResponse(user.Id, code);
        }

        static string GenerateNewCode()
        {
            Random random = new Random();
            string code = "";

            for (int i = 0; i < 6; i++)
            {
                int number = random.Next(10); // Genera un dígito aleatorio entre 0 y 9
                code += number.ToString();
            }

            return code;
        }

        Task<bool> ExistsCodeInDatabase(string code)
            => _context.UserOculusCodes.AnyAsync(x => x.OculusCode  == code);
    }
}
