import { useEffect, useState } from "react";
import type { Ticket } from "./types/Ticket";
import { getTickets } from "./services/ticketService";
import TicketList from "./components/TicketList";

function App() {
  const [tickets, setTickets] = useState<Ticket[]>([]);

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

      <TicketList tickets={tickets} />
    </div>
  );
}

export default App;