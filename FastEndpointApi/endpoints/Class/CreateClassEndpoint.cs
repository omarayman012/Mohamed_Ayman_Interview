using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Class;
using FastEndpointApi.Application.Interfaces.Class;
using FastEndpoints;

namespace FastEndpointApi.endpoints.Class
{
    public class CreateClassEndpoint
       : Endpoint<CreateClassRequest, Result<ClassResponse>>
    {
        private readonly IClassService _classService;

        public CreateClassEndpoint(IClassService classService)
        {
            _classService = classService;
        }

        public override void Configure()
        {
            Post("/classes");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateClassRequest req, CancellationToken ct)
        {
            var result = _classService.Create(req);

            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }
}
