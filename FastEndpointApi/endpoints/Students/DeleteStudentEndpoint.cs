using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.Interfaces.Students;
using FastEndpoints;
using FastEndpointApi.Application.DTOs.Student;

namespace FastEndpointApi.endpoints.Students
{
    public class DeleteStudentEndpoint
        : Endpoint<DeleteStudentRequest, Result<bool>>
    {
        private readonly IStudentService _studentService;

        public DeleteStudentEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }
         
        public override void Configure()
        {
            Delete("/students/{id}");
            AllowAnonymous();
        } 

        public override async Task HandleAsync(DeleteStudentRequest req, CancellationToken ct)
        {
            var result = _studentService.Delete(req.Id);

            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }
}