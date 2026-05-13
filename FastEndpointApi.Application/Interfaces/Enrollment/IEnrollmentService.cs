using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastEndpointApi.Application.Interfaces.Enrollment
{
    public interface IEnrollmentService
    {
        Result<EnrollmentResponse> Enroll(CreateEnrollmentRequest request);

        Result<PaginatedList<EnrollmentListResponse>> GetAll(
     int pageNumber,
     int pageSize,
     string? studentName,
     string? className);
        Result<bool> Delete(int id);
    }
}
