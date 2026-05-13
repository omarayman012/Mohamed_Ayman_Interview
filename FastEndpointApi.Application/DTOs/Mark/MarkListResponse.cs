using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastEndpointApi.Application.DTOs.Mark
{
    public class MarkListResponse
    {
        public string StudentName { get; set; }
        public string ClassName { get; set; }

        public decimal ExamMark { get; set; }
        public decimal AssignmentMark { get; set; }
        public decimal TotalMark { get; set; }
    }
}
