using FastEndpointApi.Application.Common;
using FastEndpointApi.Application.DTOs.Class;
using FastEndpointApi.Application.Interfaces.Class;
using FastEndpointApi.Domain.Entities;
using FastEndpointApi.Infrastructure.Persistence;
using System;
using System.Linq;

namespace FastEndpointApi.Infrastructure.Services.Classes
{
    public class ClassService : IClassService
    {
        public Result<ClassResponse> Create(CreateClassRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.Teacher))
            {
                return Result<ClassResponse>.Failure("Name and Teacher are required", 400);
            }

            var exists = FakeDb.Classes.Values
                .Any(c => c.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                return Result<ClassResponse>
                    .Failure("Class name already exists", 409);
            }

            var cls = new Class
            {
                Id = new Random().Next(1, 100000),
                Name = request.Name,
                Teacher = request.Teacher,
                Description = request.Description
            };

            FakeDb.Classes.TryAdd(cls.Id, cls);

            return Result<ClassResponse>
                .Success(Map(cls), "Created", 201);
        }

        public Result<PaginatedList<ClassResponse>> GetAll(
            int pageNumber,
            int pageSize,
            string? name,
            string? teacher)
        {
            var query = FakeDb.Classes.Values
                .Select(Map)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x =>
                    x.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(teacher))
            {
                query = query.Where(x =>
                    x.Teacher.Contains(teacher, StringComparison.OrdinalIgnoreCase));
            }

            var paginated = PaginatedList<ClassResponse>
                .CreateAsync(query, pageNumber, pageSize)
                .Result;

            return Result<PaginatedList<ClassResponse>>
                .Success(paginated, "Fetched", 200);
        }
        public Result<ClassAverageResponse> GetAverageMarks(int classId)
        {
            if (!FakeDb.Classes.TryGetValue(classId, out var cls))
                return Result<ClassAverageResponse>.Failure("Class not found", 404);

            var marks = FakeDb.Marks.Values
                .Where(m => m.ClassId == classId)
                .ToList();

            if (!marks.Any())
                return Result<ClassAverageResponse>.Failure("No marks found for this class", 404);

            var avg = marks.Average(x => x.TotalMark);

            var result = new ClassAverageResponse
            {
                ClassId = cls.Id,
                ClassName = cls.Name,
                AverageMark = avg,
                StudentsCount = marks.Select(x => x.StudentId).Distinct().Count()
            };

            return Result<ClassAverageResponse>.Success(result, "Fetched", 200);
        }
        public Result<bool> Delete(int id)
        {
            if (!FakeDb.Classes.TryRemove(id, out _))
                return Result<bool>.Failure("Not found", 404);

            return Result<bool>.Success(true, "Deleted", 200);
        }

        private ClassResponse Map(Class c)
        {
            return new ClassResponse
            {
                Id = c.Id,
                Name = c.Name,
                Teacher = c.Teacher,
                Description = c.Description
            };
        }
    }
}