using FastEndpointApi.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastEndpointApi.Infrastructure.Persistence
{
    public static class FakeDb
    {
        public static ConcurrentDictionary<int, Student> Students = new();
        public static ConcurrentDictionary<int, Class> Classes = new();
        public static ConcurrentDictionary<int, Enrollment> Enrollments = new();
        public static ConcurrentDictionary<int, Mark> Marks = new();
    }
}
