using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Enrollment;
using FastEndpointApi.Application.Interfaces.Enrollment;
using FastEndpoints;

namespace FastEndpointApi.endpoints.Enrollment
{
    public class CreateEnrollmentEndpoint
       : Endpoint<CreateEnrollmentRequest, Result<EnrollmentResponse>>
    {
        private readonly IEnrollmentService _enrollmentService;

        public CreateEnrollmentEndpoint(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        public override void Configure()
        {
            Post("/enrollments");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateEnrollmentRequest req, CancellationToken ct)
        {
            var result = _enrollmentService.Enroll(req);

            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }
}
