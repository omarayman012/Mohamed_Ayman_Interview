using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastEndpointApi.Application.DTOs.Mark
{
    public class GetAllMarksRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? StudentName { get; set; }
        public string? ClassName { get; set; }
    }
}
