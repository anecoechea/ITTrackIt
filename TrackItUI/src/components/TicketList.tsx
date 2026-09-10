import type { Ticket } from "../types/Ticket";
import TicketCard from "./TicketCard";

interface TicketListProps {
  tickets: Ticket[];
}

function TicketList({ tickets }: TicketListProps) {
  return (
    <div>
      {tickets.map((ticket) => (
        <TicketCard key={ticket.id} ticket={ticket} />
      ))}
    </div>
  );
}

export default TicketList;