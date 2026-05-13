using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastEndpointApi.Application.Interfaces.Class
{
    public interface IClassService
    {
        Result<ClassResponse> Create(CreateClassRequest request);

        Result<PaginatedList<ClassResponse>> GetAll(
            int pageNumber,
            int pageSize,
            string? name,
            string? teacher);
        Result<ClassAverageResponse> GetAverageMarks(int classId);
        Result<bool> Delete(int id);
    }
}
