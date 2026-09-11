import type { Ticket } from "../types/Ticket";
import "../styles/TicketList.css";

interface TicketListProps {
  tickets: Ticket[];
}

function TicketList({ tickets }: TicketListProps) {
  return (
    <table className="ticket-table">
      <thead>
        <tr>
          <th>ID</th>
          <th>Title</th>
          <th>Status</th>
          <th>Asset</th>
          <th>Created By</th>
          <th>Assignee</th>
          <th>Created</th>
        </tr>
      </thead>
      
      <tbody>
        {tickets.map((ticket) => (
          <tr key={ticket.id}>
          <td>{ticket.id}</td>
          <td>{ticket.title}</td>
          <td>
            <span className={`ticket-status ${ticket.status.toLowerCase().replace(" ", "-")}`}>
              {ticket.status}
            </span>
          </td>
          <td>{ticket.assetName}</td>
          <td>{ticket.createdByUserName}</td>
          <td>
            {
             ticket.assignees.map((assignee) =>
              assignee.fullName)
              .join(", ")            
            }
          </td>          
          <td>
            {new Date(ticket.createdAt).toLocaleDateString("en-US", {
              month: "short",
              day: "numeric",
              year: "numeric",
            })}
          </td>            
          </tr>
        ))}
      </tbody>
    </table> 
  );
}

export default TicketList;