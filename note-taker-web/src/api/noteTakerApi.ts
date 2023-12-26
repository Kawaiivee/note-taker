import type { AddNoteRequest } from '@/classes/contracts';
import type { AuthorModel, NoteModel } from '@/classes/types';

import httpClient from './httpClient';
const ENDPOINT = '/Note';

const getNotes = async () => {
  let notes = [];
  const response = await httpClient.get(`${ENDPOINT}/get-all-notes`);
  notes = response.data;
  return notes;
}

const addNote = async (request: AddNoteRequest) => {
  const response = await httpClient.post(`${ENDPOINT}/add-note`, {
    authorName: request?.authorName,
    noteTitle: request?.noteTitle,
    noteText: request?.noteText
  });
}

const deleteNote = async (id: string) => {
  const response = await httpClient.delete(`${ENDPOINT}/delete-note-by-id/${id}`);
}

const updateNote = async (note: NoteModel) => {
  const response = await httpClient.put(`${ENDPOINT}/edit-note`, note);
}

export { getNotes, addNote, deleteNote, updateNote };