using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastEndpointApi.Application.DTOs.Student
{
    public class StudentReportResponse
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;

        public List<ClassMarkInfo> Classes { get; set; } = new();

        public decimal Average { get; set; }
    }

    public class ClassMarkInfo
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public decimal TotalMark { get; set; }
    }
}
