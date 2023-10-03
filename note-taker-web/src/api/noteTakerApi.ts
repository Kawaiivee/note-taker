import type { AddNoteRequest } from '@/classes/contracts';
import type { AuthorModel, NoteModel } from '@/classes/types';

import httpClient from './httpClient';
const ENDPOINT = '/Note';

const getNotes = async () => {
  let notes = [];
  const response = await httpClient.get(`${ENDPOINT}/getNotes`);
  notes = response.data;
  return notes;
}

const addNote = async (request: AddNoteRequest) => {
  const response = await httpClient.post(`${ENDPOINT}/addNote`, {
    authorName: request?.authorName,
    noteTitle: request?.noteTitle,
    noteText: request?.noteText
  });
}

export { getNotes, addNote };