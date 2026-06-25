using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackItApi.DTOs
{
    public class CreateAssetDto
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
    }
}