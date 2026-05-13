using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Student;
using FastEndpointApi.Application.Interfaces.Students;
using FastEndpoints;

namespace FastEndpointApi.endpoints.Students
{
    public class GetAllStudentsEndpoint
     : Endpoint<GetAllStudentsRequest, Result<PaginatedList<StudentResponse>>>
    {
        private readonly IStudentService _studentService;

        public GetAllStudentsEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public override void Configure()
        {
            Get("/students");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAllStudentsRequest req, CancellationToken ct)
        {
            var result = _studentService.GetAll(
                req.Page,
                req.PageSize,
                req.Name,
                req.Age
            );

            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }
}
