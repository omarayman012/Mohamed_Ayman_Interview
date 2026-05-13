using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastEndpointApi.Application.DTOs.Enrollment
{
    public class CreateEnrollmentRequest
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
    }
}
