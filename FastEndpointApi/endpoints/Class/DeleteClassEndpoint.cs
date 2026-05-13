using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.Interfaces.Class;
using FastEndpoints;

namespace FastEndpointApi.endpoints.Class
{
    public class DeleteClassEndpoint
     : EndpointWithoutRequest<Result<bool>>
    {
        private readonly IClassService _classService;

        public DeleteClassEndpoint(IClassService classService)
        {
            _classService = classService;
        }

        public override void Configure()
        {
            Delete("/classes/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            int id = Route<int>("id");

            var result = _classService.Delete(id);

            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }
}
