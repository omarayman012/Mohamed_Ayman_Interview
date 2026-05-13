using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Student;
using FastEndpointApi.Application.Interfaces.Students;
using FastEndpoints;

namespace FastEndpointApi.endpoints.Students
{
    public class UpdateStudentEndpoint : Endpoint<UpdateStudentRequest, Result<StudentResponse>>
    {
        private readonly IStudentService _studentService;

        public UpdateStudentEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public override void Configure()
        {
            Put("/students");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateStudentRequest req, CancellationToken ct)
        {
            var result = _studentService.Update(req);

            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }
}
