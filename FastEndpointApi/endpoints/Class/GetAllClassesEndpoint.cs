using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Class;
using FastEndpointApi.Application.Interfaces.Class;
using FastEndpoints;

namespace FastEndpointApi.endpoints.Class
{
    public class GetAllClassesEndpoint
     : Endpoint<GetAllClassesRequest, Result<PaginatedList<ClassResponse>>>
    {
        private readonly IClassService _classService;

        public GetAllClassesEndpoint(IClassService classService)
        {
            _classService = classService;
        }

        public override void Configure()
        {
            Get("/classes");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAllClassesRequest req, CancellationToken ct)
        {
            var result = _classService.GetAll(
                req.Page,
                req.PageSize,
                req.Name,
                req.Teacher
            );

            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }

}
