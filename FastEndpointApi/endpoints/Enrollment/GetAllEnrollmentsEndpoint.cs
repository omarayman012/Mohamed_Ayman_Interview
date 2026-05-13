using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Enrollment;
using FastEndpointApi.Application.Interfaces.Enrollment;
using FastEndpoints;

namespace FastEndpointApi.endpoints.Enrollment
{
    public class GetAllEnrollmentsEndpoint
        : Endpoint<GetAllEnrollmentsRequest, Result<PaginatedList<EnrollmentResponse>>>
    {
        private readonly IEnrollmentService _service;

        public GetAllEnrollmentsEndpoint(IEnrollmentService service)
        {
            _service = service;
        }

        public override void Configure()
        {
            Get("/enrollments");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAllEnrollmentsRequest req, CancellationToken ct)
        {
            var result = _service.GetAll(
                req.Page,
                req.PageSize,
                req.StudentName,
                req.ClassName);

            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }
}