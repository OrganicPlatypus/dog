import { createReducer, on } from '@ngrx/store';
import * as RootActions  from './settings.actions';
import { initialSettingsState, NoteSettingsState } from './settings.inteface';

export const settingsReducer = createReducer(
  initialSettingsState,
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
  on( RootActions.setExpirationDateAction, (state: NoteSettingsState, action) => ({
    ...state,
    expirationDate: action.expirationDate
  })
),
  on(RootActions.clearStateAction, () => ({
      ...initialSettingsState
    })
  )
)
