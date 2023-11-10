using MediatR;

using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application.UseCases.Assistants.Question
{
    public record QuestionRequest(string Name, string Question) : IRequest<Result<QuestionResponse>>;

}
