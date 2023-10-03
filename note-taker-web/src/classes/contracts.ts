import type { NoteModel } from "./types";

export interface AddNoteRequest {
  authorName: string,
  noteTitle: string,
  noteText: string,
}

export interface GetNotesResponse {
  notes: NoteModel[];
}