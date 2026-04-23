using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackItApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "Requester";
        public string Department { get; set; } = string.Empty;
        public string Team { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Ticket> CreatedTickets { get; set; } = new List<Ticket>();
        public ICollection<TicketAssignment> AssignedTickets { get; set; } = new List<TicketAssignment>();    
    }
}