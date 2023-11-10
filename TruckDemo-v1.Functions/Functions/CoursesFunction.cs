using System.Net;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using TruckDemo.Function.Extensions;
using TruckDemo.Function.Middlewere;
using TruckDemo_v1.Application.UseCases.Courses.CreateCourse;
using TruckDemo_v1.Application.UseCases.Courses.DeleteCourse;
using TruckDemo_v1.Application.UseCases.Courses.GetCourseById;
using TruckDemo_v1.Application.UseCases.Courses.GetCourses;
using TruckDemo_v1.Application.UseCases.Courses.UpdateCourse;
using TruckDemo_v1.Application.UseCases.Lessons.CreateLesson;
using TruckDemo_v1.Application.UseCases.Lessons.GetBySection;

namespace TruckDemo.Function.Functions
{
    public class CoursesFunction
    {
        private readonly IMediator _mediator;

        public CoursesFunction(IMediator mediator)
        {
            _mediator =mediator;
        }

        [Function("GetCourses")]
        public async Task<HttpResponseData> GetCourses([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "course")] HttpRequestData req)
        {
            var request = new GetCoursesRequest();
            return await _mediator.Send(request).ToResponseData(req);
        }

        [Function("GetCourseById")]
        public async Task<HttpResponseData> GetCourseById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "course/id")] HttpRequestData req)
        {
            string id = req.Query["id"];

            Guid parse = Guid.Parse(id);

            var request = new GetCourseByIdRequest(parse);
            return await _mediator.Send(request).ToResponseData(req);
        }

        [Function("PostCourse")]
        [AuthentificationMiddlewere("Admin")]
        public async Task<HttpResponseData> PostCourse([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "course")] HttpRequestData req)
        {

            var request = await req.ReadFromJsonAsync<CreateCourseRequest>();
            return await _mediator.Send(request!).ToResponseData(req);
        }

        [Function("DeleteCourse")]
        public async Task<HttpResponseData> DeleteCourse([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "course/courseid")] HttpRequestData req)
        {
            string id = req.Query["courseid"];

            Guid parse = Guid.Parse(id);

            var request = new DeleteCourseRequest(parse);
            return await _mediator.Send(request).ToResponseData(req);
        }

        [Function("UpdateCourse")]
        public async Task<HttpResponseData> UpdateCourse([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "course/courseid")] HttpRequestData req)
        {
            var request = await req.ReadFromJsonAsync<UpdateCourseRequest>();
            return await _mediator.Send(request!).ToResponseData(req);
        }

    }
}
