using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Mark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastEndpointApi.Application.Interfaces.Mark
{

    public interface IMarkService
    {
        Result<MarkResponse> Create(CreateMarkRequest request);
        Result<PaginatedList<MarkListResponse>> GetAll(
              int pageNumber,
              int pageSize,
              string? studentName,
              string? className);


        Result<bool> Delete(int id);
    }
}
