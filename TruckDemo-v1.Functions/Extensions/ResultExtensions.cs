using Microsoft.Azure.Functions.Worker.Http;
using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo.Function.Extensions
{
    public static class ResultExtensions
    {
        public static async Task<HttpResponseData> ToResponseData<T>(this Result<T> result, HttpRequestData request)
        {
            var response = request.CreateResponse();

            response.StatusCode = result.Succeeded ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest;

            await response.WriteAsJsonAsync(result);

            return response;
        }

        public static async Task<HttpResponseData> ToResponseData<T>(this Task<Result<T>> task, HttpRequestData request)
        {
            var result = await task;

            return await result.ToResponseData(request);
        }

        public static async Task<HttpResponseData> ToResponseData(this Result result, HttpRequestData request)
        {
            var response = request.CreateResponse();

            response.StatusCode = result.Succeeded ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest;

            await response.WriteAsJsonAsync(result);

            return response;
        }

        public static async Task<HttpResponseData> ToResponseData(this Task<Result> task, HttpRequestData request)
        {
            var result = await task;

            return await result.ToResponseData(request);
        }
    }
}
