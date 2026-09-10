import type { Ticket } from "../types/Ticket";

const API_URL = "http://localhost:5219/api/Tickets";

export async function getTickets(): Promise<Ticket[]> {
  const response = await fetch(API_URL);

  if (!response.ok) {
    throw new Error("Failed to fetch tickets");
  }

  return response.json();
}