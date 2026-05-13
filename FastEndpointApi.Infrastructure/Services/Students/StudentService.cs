using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Class;
using FastEndpointApi.Application.DTOs.Student;
using FastEndpointApi.Application.Interfaces.Students;
using FastEndpointApi.Domain.Entities;
using FastEndpointApi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastEndpointApi.Infrastructure.Services.Students
{

    public class StudentService : IStudentService
    {
        public Result<StudentResponse> Create(CreateStudentRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName) ||
                 string.IsNullOrWhiteSpace(request.LastName))
            {
                return Result<StudentResponse>.Failure("FirstName and LastName are required", 400);
            }

            if (request.Age <= 0)
            {
                return Result<StudentResponse>.Failure("Age must be greater than 0", 400);
            }
            var student = new Student
            {
                Id = new Random().Next(1, 100000),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age
            };

            FakeDb.Students.TryAdd(student.Id, student);

            return Result<StudentResponse>.Success(Map(student), "Created", 201);
        }

        public Result<PaginatedList<StudentResponse>> GetAll(int pageNumber, int pageSize, string? name, int? age)
        {
            var query = FakeDb.Students.Values
                .Select(Map)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x =>
                    x.FullName.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (age.HasValue)
            {
                query = query.Where(x => x.Age == age.Value);
            }

            var paginated = PaginatedList<StudentResponse>
                .CreateAsync(query, pageNumber, pageSize)
                .Result;

            return Result<PaginatedList<StudentResponse>>
                .Success(paginated, "Fetched", 200);
        }

        public Result<StudentResponse> GetById(int id)
        {
            if (!FakeDb.Students.TryGetValue(id, out var student))
                return Result<StudentResponse>.Failure("Not found", 404);

            return Result<StudentResponse>.Success(Map(student));
        }

        public Result<StudentResponse> Update(UpdateStudentRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName) ||
              string.IsNullOrWhiteSpace(request.LastName))
            {
                return Result<StudentResponse>.Failure("FirstName and LastName are required", 400);
            }

            if (request.Age <= 0)
            {
                return Result<StudentResponse>.Failure("Age must be greater than 0", 400);
            }
            if (!FakeDb.Students.TryGetValue(request.Id, out var student))
                return Result<StudentResponse>.Failure("Not found", 404);

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Age = request.Age;

            return Result<StudentResponse>.Success(Map(student), "Updated", 200);
        }
        public Result<StudentReportResponse> GetReport(int studentId)
        {
            if (!FakeDb.Students.TryGetValue(studentId, out var student))
                return Result<StudentReportResponse>.Failure("Not found", 404);

            var enrollments = FakeDb.Enrollments.Values
                .Where(e => e.StudentId == studentId)
                .ToList();

            var marks = FakeDb.Marks.Values
                .Where(m => m.StudentId == studentId)
                .ToList();

            var reportClasses = enrollments.Select(e =>
            {
                var cls = FakeDb.Classes[e.ClassId];

                var mark = marks.FirstOrDefault(m => m.ClassId == e.ClassId);

                return new ClassMarkInfo
                {
                    ClassId = cls.Id,
                    ClassName = cls.Name,
                    TotalMark = mark?.TotalMark ?? 0
                };
            }).ToList();

            var avg = reportClasses.Any()
                ? reportClasses.Average(x => x.TotalMark)
                : 0;

            var result = new StudentReportResponse
            {
                StudentId = student.Id,
                StudentName = $"{student.FirstName} {student.LastName}",
                Classes = reportClasses,
                Average = avg
            };

            return Result<StudentReportResponse>.Success(result, "Report generated", 200);
        }
        public Result<bool> Delete(int id)
        {
            var removed = FakeDb.Students.TryRemove(id, out _);

            if (!removed)
                return Result<bool>.Failure("Not found", 404);

            return Result<bool>.Success(true, "Deleted", 200);
        }

        private StudentResponse Map(Student s)
        {
            return new StudentResponse
            {
                Id = s.Id,
                FullName = $"{s.FirstName} {s.LastName}",
                Age = s.Age,
                Classes = FakeDb.Enrollments.Values
                    .Where(e => e.StudentId == s.Id)
                    .Select(e =>
                    {
                        FakeDb.Classes.TryGetValue(e.ClassId, out var cls);
                        return new ClassResponse
                        {
                            Id = cls?.Id ?? 0,

                            Name = cls?.Name ?? "Unknown"
                        };
                    })
                    .ToList()

            };
        }
    }
}
