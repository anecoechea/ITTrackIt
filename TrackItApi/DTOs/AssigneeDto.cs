using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackItApi.DTOs
{
    public class AssigneeDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}