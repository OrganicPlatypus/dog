import { createReducer, on } from '@ngrx/store';
import * as RootActions  from './root.actions';
import { initialState, NoteSettingsState } from './app.state';

export const rootReducer = createReducer(
  initialState,
  on( RootActions.setExpirationTimerAction, (state: NoteSettingsState, action) => ({
      ...state,
      minutesUntilExpire: action.expirationTimer
    })
  ),
  on( RootActions.setNoteNameAction, (state: NoteSettingsState, action) => ({
      ...state,
      noteName: action.noteName
    })
  ),
  on(RootActions.clearStateAction, () => ({
      ...initialState
    })
  )
)
