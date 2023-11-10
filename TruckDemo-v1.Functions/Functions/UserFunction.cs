using System.Net;
using Azure.Core;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using TruckDemo.Function.Extensions;
using TruckDemo.Function.Middlewere;
using TruckDemo_v1.Application.UseCases.Users.ApplyRoles;
using TruckDemo_v1.Application.UseCases.Users.CreateDefaultUser;
using TruckDemo_v1.Application.UseCases.Users.CreateUser;
using TruckDemo_v1.Application.UseCases.Users.GetAllUsers;
using TruckDemo_v1.Application.UseCases.Users.Login;

namespace TruckDemo.Function.Functions
{
    public class UserFunction
    {
        private readonly IMediator _mediator;

        public UserFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Function("Login")]
        public async Task<HttpResponseData> Login([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user/login")] HttpRequestData req)
        {
            var request = await req.ReadFromJsonAsync<LoginRequest>();
            return await _mediator.Send(request!).ToResponseData(req);
        }

        [Function("CreateDefaultUser")]
        public async Task<HttpResponseData> CreateDefaultUser([HttpTrigger(AuthorizationLevel.Anonymous,"post",Route = "user/default")] HttpRequestData req)
        {
            var request = new CreateDefaultUserRequest();
            return await _mediator.Send(request).ToResponseData(req);
        }

        [Function("CreateUser")]
        [AuthentificationMiddlewere("admin")]
        public async Task<HttpResponseData> CreateUser([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route ="user")] HttpRequestData req)
        {
            var request = await req.ReadFromJsonAsync<CreateUserRequest>();
            return await _mediator.Send(request!).ToResponseData(req);
        }

        [Function("ApplyRole")]
        [AuthentificationMiddlewere("admin")]
        public async Task<HttpResponseData> ApplyRole([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "user/id")] HttpRequestData req)
        {
            var request = await req.ReadFromJsonAsync<ApplyRoleRequest>();
            return await _mediator.Send(request!).ToResponseData(req);
        }

        [Function("GetAllUsers")]
        [AuthentificationMiddlewere("admin")]
        public async Task<HttpResponseData> GetAllUsers([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user")] HttpRequestData req)
        {
            var request = new GetAllUsersRequest();
            return await _mediator.Send(request).ToResponseData(req);
        }


        [Function("GetAllRoles")]
        [AuthentificationMiddlewere("admin")]
        public async Task<HttpResponseData> GetAllRoles([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/role")] HttpRequestData req)
        {
            var request = new GetAllUsersRequest();
            return await _mediator.Send(request).ToResponseData(req);
        }

    }
}
