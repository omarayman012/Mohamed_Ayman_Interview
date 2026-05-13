using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastEndpointApi.Application.DTOs.Enrollment
{
    public class EnrollmentListResponse
    {
        public int Id { get; set; }  

        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
