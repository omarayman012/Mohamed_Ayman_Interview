using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Enrollment;
using FastEndpointApi.Application.Interfaces.Enrollment;
using FastEndpoints;

namespace FastEndpointApi.endpoints.Enrollment
{
    public class DeleteEnrollmentEndpoint
      : Endpoint<DeleteEnrollmentRequest, Result<bool>>
    {
        private readonly IEnrollmentService _service;

        public DeleteEnrollmentEndpoint(IEnrollmentService service)
        {
            _service = service;
        }

        public override void Configure()
        {
            Delete("/enrollments/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteEnrollmentRequest req, CancellationToken ct)
        {
            var result = _service.Delete(req.Id);

            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }
}