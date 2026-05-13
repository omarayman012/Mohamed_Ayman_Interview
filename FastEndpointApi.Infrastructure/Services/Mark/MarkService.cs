using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Mark;
using FastEndpointApi.Application.Interfaces.Mark;
using FastEndpointApi.Domain.Entities;
using FastEndpointApi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FastEndpointApi.Infrastructure.Services.Marks
{
    public class MarkService : IMarkService
    {
        public Result<MarkResponse> Create(CreateMarkRequest request)
        {
            if (!FakeDb.Students.ContainsKey(request.StudentId))
                return Result<MarkResponse>.Failure("Student not found", 404);

            if (!FakeDb.Classes.ContainsKey(request.ClassId))
                return Result<MarkResponse>.Failure("Class not found", 404);

            var isEnrolled = FakeDb.Enrollments.Values.Any(e =>
                e.StudentId == request.StudentId &&
                e.ClassId == request.ClassId);

            if (!isEnrolled)
                return Result<MarkResponse>.Failure("Student not enrolled in this class", 400);

            var mark = new Mark
            {
                Id = new Random().Next(1, 100000),
                StudentId = request.StudentId,
                ClassId = request.ClassId,
                ExamMark = request.ExamMark,
                AssignmentMark = request.AssignmentMark
            };

            FakeDb.Marks.TryAdd(mark.Id, mark);

            return Result<MarkResponse>.Success(Map(mark), "Created", 201);
        }

        public Result<PaginatedList<MarkListResponse>> GetAll(
      int pageNumber,
      int pageSize,
      string? studentName,
      string? className)
        {
            var query = FakeDb.Marks.Values.AsEnumerable();

            // 👇 فلترة بالاسم فقط

            if (!string.IsNullOrWhiteSpace(studentName))
            {
                query = query.Where(m =>
                {
                    FakeDb.Students.TryGetValue(m.StudentId, out var student);

                    var fullName = student != null
                        ? $"{student.FirstName} {student.LastName}"
                        : "";

                    return fullName.Contains(studentName, StringComparison.OrdinalIgnoreCase);
                });
            }

            if (!string.IsNullOrWhiteSpace(className))
            {
                query = query.Where(m =>
                {
                    FakeDb.Classes.TryGetValue(m.ClassId, out var cls);

                    var name = cls?.Name ?? "";

                    return name.Contains(className, StringComparison.OrdinalIgnoreCase);
                });
            }

            var mapped = query
                .Select(MapToListResponse)
                .AsQueryable();

            var paginated = PaginatedList<MarkListResponse>
                .CreateAsync(mapped, pageNumber, pageSize)
                .Result;

            return Result<PaginatedList<MarkListResponse>>
                .Success(paginated, "Fetched", 200);
        }
        public Result<bool> Delete(int id)
        {
            if (!FakeDb.Marks.TryRemove(id, out _))
                return Result<bool>.Failure("Not found", 404);

            return Result<bool>.Success(true, "Deleted", 200);
        }

        private MarkResponse Map(Mark m)
        {
            return new MarkResponse
            {
                Id = m.Id,
                StudentId = m.StudentId,
                ClassId = m.ClassId,
                ExamMark = m.ExamMark,
                AssignmentMark = m.AssignmentMark,
                TotalMark = m.TotalMark
            };
        }
        private MarkListResponse MapToListResponse(Mark m)
        {
            FakeDb.Students.TryGetValue(m.StudentId, out var student);
            FakeDb.Classes.TryGetValue(m.ClassId, out var cls);

            return new MarkListResponse
            {
                StudentName = student != null ? $"{student.FirstName} {student.LastName}" : "Unknown",
                ClassName = cls?.Name ?? "Unknown",

                ExamMark = m.ExamMark,
                AssignmentMark = m.AssignmentMark,
                TotalMark = m.TotalMark
            };
        }
    }
}