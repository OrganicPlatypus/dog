import { TodoItem } from './../../../modules/to-do-list/models/to-do';
import { State } from "../../app.state";

export interface NotesState extends State{
  notes: NotePointsState
}

export const initialNotesState: NotePointsState = {
  notes: []
}

export interface NotePointsState {
  notes: TodoItem[];
}
