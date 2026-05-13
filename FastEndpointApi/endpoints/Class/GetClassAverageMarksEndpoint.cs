using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Class;
using FastEndpointApi.Application.Interfaces.Class;
using FastEndpoints;

namespace FastEndpointApi.endpoints.Class
{
    public class GetClassAverageMarksEndpoint
      : Endpoint<GetClassAverageRequest, Result<ClassAverageResponse>>
    {
        private readonly IClassService _classService;

        public GetClassAverageMarksEndpoint(IClassService classService)
        {
            _classService = classService;
        }

        public override void Configure()
        {
            Get("/api/classes/{classId}/average-marks");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetClassAverageRequest req, CancellationToken ct)
        {
            var result = _classService.GetAverageMarks(req.ClassId);

            HttpContext.Response.StatusCode = result.StatusCode;
            await HttpContext.Response.WriteAsJsonAsync(result, ct);
        }
    }
}