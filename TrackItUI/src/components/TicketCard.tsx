import type { Ticket } from "../types/Ticket";

interface TicketCardProps {
  ticket: Ticket;
}

function TicketCard({ ticket } : TicketCardProps) {
  const date = new Date(ticket.createdAt)
  return (
    <div>
      <h2>{ticket.title}</h2>
      <p>{ticket.status}</p>
      <p>{ticket.description}</p>
      <p>Created: {date.toLocaleDateString("en-US", {
            month: "long",
            day: "numeric",
            year: "numeric",
          })}
      </p>
    </div>
  );
}

export default TicketCard;