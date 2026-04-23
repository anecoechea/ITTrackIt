using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackItApi.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Open";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? AssetId { get; set; }
        public Asset? Asset { get; set; }
        public int? CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }
        public ICollection<TicketAssignment> Assignees { get; set; } = new List<TicketAssignment>();
        
    }
}