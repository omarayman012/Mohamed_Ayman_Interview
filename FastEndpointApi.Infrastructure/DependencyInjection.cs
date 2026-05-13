using FastEndpointApi.Application.Interfaces.Class;
using FastEndpointApi.Application.Interfaces.Enrollment;
using FastEndpointApi.Application.Interfaces.Mark;
using FastEndpointApi.Application.Interfaces.Students;
using FastEndpointApi.Infrastructure.Services.Classes;
using FastEndpointApi.Infrastructure.Services.Enrollment;
using FastEndpointApi.Infrastructure.Services.Marks;
using FastEndpointApi.Infrastructure.Services.Students;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastEndpointApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IMarkService, MarkService>();
            return services;
        }
    }
}
