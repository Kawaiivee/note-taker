export interface AuthorModel {
  id: string,
  name: string,
};

export interface NoteModel {
  id: string,
  author: AuthorModel,
  title: string,
  text: string,
};