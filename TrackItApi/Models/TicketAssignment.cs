using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackItApi.Models
{
    public class TicketAssignment
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}