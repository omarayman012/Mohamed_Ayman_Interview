using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastEndpointApi.Application.Interfaces.Students
{
    public interface IStudentService
    {
        Result<StudentResponse> Create(CreateStudentRequest request);

        Result<PaginatedList<StudentResponse>> GetAll(int pageNumber, int pageSize, string? name, int? age);
        Result<StudentResponse> GetById(int id);

        Result<StudentResponse> Update(UpdateStudentRequest request);
        Result<StudentReportResponse> GetReport(int studentId);
        Result<bool> Delete(int id);
    }
}
