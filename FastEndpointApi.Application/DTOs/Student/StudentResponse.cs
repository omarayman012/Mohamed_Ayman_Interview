using FastEndpointApi.Application.DTOs.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastEndpointApi.Application.DTOs.Student
{
    public class StudentResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public List<ClassResponse> Classes { get; set; } = new List<ClassResponse>();
    } 
    public class ListClassResponse
    {
        public List<ClassResponse> Students { get; set; } = new List<ClassResponse>();
       
    }   
}
