export interface Assignee {
  userId: number;
  fullName: string;
}

export interface Ticket {
  id: number;
  title: string;
  description: string;
  status: string;
  createdAt: string;
  assetId: number | null;
  assetName: string | null;
  createdByUserId: number | null;
  createdByUserName: string | null;
  assignees: Assignee[];
}