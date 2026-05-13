using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Student;
using FastEndpointApi.Application.Interfaces.Students;
using FastEndpoints;

namespace FastEndpointApi.endpoints.Students
{
    public class CreateStudentEndpoint : Endpoint<CreateStudentRequest, Result<StudentResponse>>
    {
        private readonly IStudentService _studentService;

        public CreateStudentEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public override void Configure()
        {
            Post("/students");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateStudentRequest req, CancellationToken ct)
        {
            var result = _studentService.Create(req);

            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }
}
