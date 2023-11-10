using MediatR;

using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application.UseCases.Users.LoginOculus
{
    public record LoginOculusRequest(string Code) : IRequest<Result<LoginOculusResponse>>;

}
