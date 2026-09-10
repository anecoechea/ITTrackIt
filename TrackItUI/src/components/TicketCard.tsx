import type { Ticket } from "../types/Ticket";

interface TicketCardProps {
  ticket: Ticket;
}

function TicketCard({ ticket } : TicketCardProps) {
  return (
    <div>
      <h2>{ticket.title}</h2>
    </div>
  );
}

export default TicketCard;