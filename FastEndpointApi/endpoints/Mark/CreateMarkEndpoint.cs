using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Mark;
using FastEndpointApi.Application.Interfaces.Mark;
using FastEndpoints;

namespace FastEndpointApi.endpoints.Mark
{
    public class CreateMarkEndpoint
        : Endpoint<CreateMarkRequest, Result<MarkResponse>>
    {
        private readonly IMarkService _markService;

        public CreateMarkEndpoint(IMarkService markService)
        {
            _markService = markService;
        }

        public override void Configure()
        {
            Post("/marks");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateMarkRequest req, CancellationToken ct)
        {
            var result = _markService.Create(req);

            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }
}
