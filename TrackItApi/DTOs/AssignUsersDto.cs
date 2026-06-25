using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackItApi.DTOs
{
    public class AssignUsersDto
    {
        public List<int> UserIds { get; set; } = new();
    }
}