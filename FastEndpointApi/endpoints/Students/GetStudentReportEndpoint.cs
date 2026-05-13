using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Student;
using FastEndpointApi.Application.Interfaces.Students;
using FastEndpoints;

namespace FastEndpointApi.endpoints.Students
{
    public class GetStudentReportEndpoint
    : Endpoint<GetStudentReportRequest, Result<StudentReportResponse>>
    {
        private readonly IStudentService _service;

        public GetStudentReportEndpoint(IStudentService service)
        {
            _service = service;
        }

        public override void Configure()
        {
            Get("/api/students/{id}/report");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetStudentReportRequest req, CancellationToken ct)
        {
            var result = _service.GetReport(req.Id);

            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }
}
