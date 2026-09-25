import type { Ticket } from "../types/Ticket";
import "../styles/TicketCard.css";

interface TicketCardProps {
  ticket: Ticket;
  onClose: () => void;
}

function TicketCard({ ticket, onClose }: TicketCardProps) {
  const date = new Date(ticket.createdAt)
  return (
    <div className="ticket-card">
      <h2>{ticket.title}</h2>
      <p className={`ticket-card-status ${ticket.status.toLowerCase().replace(" ", "-")}`}>
        {ticket.status}
      </p>
      <p className="ticket-card-description">{ticket.description}</p>
      <div className="ticket-card-meta-grid">
        <p className="ticket-card-meta"><strong>Asset:</strong> {ticket.assetName}</p>
        <p className="ticket-card-meta"><strong>Created By:</strong> {ticket.createdByUserName}</p>
        <p className="ticket-card-meta"><strong>Assignees:</strong> {ticket.assignees.map((assignee) =>
                assignee.fullName)
                .join(", ")}
        </p>
        <p className="ticket-card-meta">
            <strong>Created:</strong> {date.toLocaleDateString("en-US", {
              month: "long",
              day: "numeric",
              year: "numeric",
            })}
        </p>
      </div>
      <button onClick={onClose}>Close</button>
    </div>
  );
}

export default TicketCard;