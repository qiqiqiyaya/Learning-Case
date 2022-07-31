using System.Net;

namespace Customize_Router.Endpoints.Results
{
    public class SuccessResult<T> : IEndpointResult
    {
        private readonly T _data;

        public SuccessResult(T data)
        {
            _data = data;
        }

        public async Task ExecuteAsync(HttpContext context)
        {
            var result = new ResultDto<T>
            {
                Data = _data,
                Code = HttpStatusCode.OK
            };

            await context.Response.WriteAsJsonAsync(result);
        }

        internal class ResultDto<T>
        {
            public T Data { get; set; }

            public HttpStatusCode Code { get; set; }
        }
    }
}
