import httpClient from './httpClient';
const ENDPOINT = '/Note';

const getNotes = () => {
  httpClient.get(`${ENDPOINT}/getNotes`);
}

const addNote = (note: any) => {
  httpClient.post(`${ENDPOINT}/addNote`, {
    id: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
    author: {
      id: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
      name: note?.author?.name ?? ''
    },
    title: note?.title ?? '',
    text: note?.text ?? ''
  });
}

export { getNotes, addNote };