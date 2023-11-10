using MediatR;

using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application.UseCases.Users.GenerateOculusCode
{
    public record GenerateOculusCodeRequest(Guid UserId) : IRequest<Result<GenerateOculusCodeResponse>>;

}
