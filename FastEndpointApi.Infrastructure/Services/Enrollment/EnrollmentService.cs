using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Enrollment;
using FastEndpointApi.Application.Interfaces.Enrollment;
using FastEndpointApi.Infrastructure.Persistence;
using System;
using System.Linq;
using EnrollmentEntity = FastEndpointApi.Domain.Entities.Enrollment;

namespace FastEndpointApi.Infrastructure.Services.Enrollment
{
    public class EnrollmentService : IEnrollmentService
    {
        public Result<EnrollmentResponse> Enroll(CreateEnrollmentRequest request)
        {
            if (!FakeDb.Students.ContainsKey(request.StudentId))
                return Result<EnrollmentResponse>.Failure("Student not found", 404);

            if (!FakeDb.Classes.ContainsKey(request.ClassId))
                return Result<EnrollmentResponse>.Failure("Class not found", 404);

            var exists = FakeDb.Enrollments.Values.Any(e =>
                e.StudentId == request.StudentId &&
                e.ClassId == request.ClassId);

            if (exists)
                return Result<EnrollmentResponse>.Failure("Already enrolled", 409);

            var enrollment = new EnrollmentEntity
            {
                Id = new Random().Next(1, 100000),
                StudentId = request.StudentId,
                ClassId = request.ClassId,
                EnrollmentDate = DateTime.UtcNow
            };

            FakeDb.Enrollments.TryAdd(enrollment.Id, enrollment);

            return Result<EnrollmentResponse>
                .Success(Map(enrollment), "Created", 201);
        }

        public Result<PaginatedList<EnrollmentListResponse>> GetAll(
            int pageNumber,
            int pageSize,
            string? studentName,
            string? className)
        {
            var query = FakeDb.Enrollments.Values.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(studentName))
            {
                query = query.Where(e =>
                {
                    FakeDb.Students.TryGetValue(e.StudentId, out var student);

                    var fullName = student != null
                        ? $"{student.FirstName} {student.LastName}"
                        : "";

                    return fullName.Contains(studentName, StringComparison.OrdinalIgnoreCase);
                });
            }

            if (!string.IsNullOrWhiteSpace(className))
            {
                query = query.Where(e =>
                {
                    FakeDb.Classes.TryGetValue(e.ClassId, out var cls);

                    return (cls?.Name ?? "")
                        .Contains(className, StringComparison.OrdinalIgnoreCase);
                });
            }

            var mapped = query
                .Select(MapList)
                .AsQueryable();

            var paginated = PaginatedList<EnrollmentListResponse>
                .CreateAsync(mapped, pageNumber, pageSize)
                .Result;

            return Result<PaginatedList<EnrollmentListResponse>>
                .Success(paginated, "Fetched", 200);
        }

        public Result<bool> Delete(int id)
        {
            var removed = FakeDb.Enrollments.TryRemove(id, out _);

            if (!removed)
                return Result<bool>.Failure("Not found", 404);

            return Result<bool>.Success(true, "Deleted", 200);
        }

        private EnrollmentResponse Map(EnrollmentEntity e)
        {
            return new EnrollmentResponse
            {
                Id = e.Id,
                StudentId = e.StudentId,
                ClassId = e.ClassId,
                EnrollmentDate = e.EnrollmentDate
            };
        }

        private EnrollmentListResponse MapList(EnrollmentEntity e)
        {
            FakeDb.Students.TryGetValue(e.StudentId, out var student);
            FakeDb.Classes.TryGetValue(e.ClassId, out var cls);

            return new EnrollmentListResponse
            {
                Id = e.Id,

                StudentName = student != null
                    ? $"{student.FirstName} {student.LastName}"
                    : "Unknown",

                ClassName = cls?.Name ?? "Unknown",

                EnrollmentDate = e.EnrollmentDate
            };
        }
    }
}