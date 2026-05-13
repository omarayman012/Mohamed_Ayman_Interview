using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Mark;
using FastEndpointApi.Application.Interfaces.Mark;
using FastEndpoints;

namespace FastEndpointApi.endpoints.Marks
{
    public class DeleteMarkEndpoint
        : Endpoint<DeleteMarkRequest, Result<bool>>
    {
        private readonly IMarkService _markService;

        public DeleteMarkEndpoint(IMarkService markService)
        {
            _markService = markService;
        }

        public override void Configure()
        {
            Delete("/marks/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteMarkRequest req, CancellationToken ct)
        {
            var result = _markService.Delete(req.Id);

            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }
}