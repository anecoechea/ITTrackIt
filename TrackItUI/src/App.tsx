import { useEffect, useState } from "react";
import type { Ticket } from "./types/Ticket";
import { getTickets } from "./services/ticketService";
import TicketList from "./components/TicketList";
import TicketCard from "./components/TicketCard";

function App() {
  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [selectedTicket, setSelectedTicket] = useState<Ticket | null>(null);

  useEffect(() => {
    getTickets()
      .then((data) => {
        setTickets(data);
      })
      .catch((error) => {
        console.error(error);
      });
  }, []);

  return (
    <div>
      <h1>TrackIt</h1>

      <p>Tickets: {tickets.length}</p>

      <TicketList 
        tickets={tickets}
        setSelectedTicket={setSelectedTicket}
      />
      {selectedTicket && (
        <TicketCard 
        ticket={selectedTicket}
        onClose={() => setSelectedTicket(null)} 
        />
      )}
    </div>
  );
}

export default App;