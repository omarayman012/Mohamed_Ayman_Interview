using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Mark;
using FastEndpointApi.Application.Interfaces.Mark;
using FastEndpoints;

namespace FastEndpointApi.endpoints.Marks
{
    public class GetAllMarksEndpoint
        : Endpoint<GetAllMarksRequest, Result<PaginatedList<MarkResponse>>>
    {
        private readonly IMarkService _markService;

        public GetAllMarksEndpoint(IMarkService markService)
        {
            _markService = markService;
        }

        public override void Configure()
        {
            Get("/marks");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAllMarksRequest req, CancellationToken ct)
        {
            var result = _markService.GetAll(
                    req.Page,
                    req.PageSize,
                    req.StudentName,
                    req.ClassName);
            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }
}