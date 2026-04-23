using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackItApi.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public bool IsAssigned { get; set; } = false;
        public int? AssignedToUserId { get; set; } = null;
        public User? AssignedToUser { get; set;} = null;

    }
}