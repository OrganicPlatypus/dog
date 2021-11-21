import { createReducer, on } from '@ngrx/store';
import * as NotesActions  from './notes.actions';
import { initialNotesState, NotePointsState } from './notes.inteface';

export const notesReducer = createReducer(
  initialNotesState,
  on( NotesActions.setExistingNotesAction, (state: NotePointsState, action) => ({
      ...state,
      notes: action.notes
    })
  ),
  on(NotesActions.clearStateAction, () => ({
      ...initialNotesState
    })
  )
)
