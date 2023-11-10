using System.Net;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using TruckDemo.Function.Extensions;
using TruckDemo.Function.Middlewere;
using TruckDemo_v1.Application.UseCases.Lessons.CreateLesson;
using TruckDemo_v1.Application.UseCases.Lessons.DeleteLesson;
using TruckDemo_v1.Application.UseCases.Lessons.GetById;
using TruckDemo_v1.Application.UseCases.Lessons.GetBySection;
using TruckDemo_v1.Application.UseCases.Lessons.UpdateLesson;

namespace TruckDemo.Function.Functions
{
    public class LessonsFunction
    {
        private readonly IMediator _mediator;

        public LessonsFunction(IMediator mediator)
        {
           _mediator = mediator;
        }

        [Function("GetLessonsBySection")]
        [AuthentificationMiddlewere("reader")]
        public async Task<HttpResponseData> GetLessonsBySection([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "lessons/sectionid")] HttpRequestData req)
        {
            string sectionid = req.Query["sectionid"];

            Guid parse = Guid.Parse(sectionid);

            var request = new GetBySectionRequest(parse);
            return await _mediator.Send(request).ToResponseData(req);
        }

        [Function("GetLessonsById")]
        public async Task<HttpResponseData> GetLessonsById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "lessons/lessonid")] HttpRequestData req)
        {
            string id = req.Query["lessonid"];

            Guid parse = Guid.Parse(id);

            var request = new GetByIdRequest(parse);
            return await _mediator.Send(request).ToResponseData(req);
        }

        [Function("PostLesson")]
        public async Task<HttpResponseData> PostLesson([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "lessons")] HttpRequestData req)
        {
            var request = await req.ReadFromJsonAsync<CreateLessonRequest>();
            return await _mediator.Send(request!).ToResponseData(req);
        }

        [Function("DeleteLesson")]
        public async Task<HttpResponseData> DeleteLesson([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "lessons/lessonid")] HttpRequestData req)
        {
            string id = req.Query["lessonid"];

            Guid parse = Guid.Parse(id);

            var request = new DeleteLessonRequest(parse);
            return await _mediator.Send(request).ToResponseData(req);
        }

        [Function("UpdateLesson")]
        public async Task<HttpResponseData> UpdateLesson([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "lessons")] HttpRequestData req)
        {
            var request = await req.ReadFromJsonAsync<UpdateLessonRequest>();
            return await _mediator.Send(request!).ToResponseData(req);
        }
    }
}
