using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastEndpointApi.Application.DTOs.Class
{
    public class ClassAverageResponse
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public decimal AverageMark { get; set; }
        public int StudentsCount { get; set; }
    }
}
