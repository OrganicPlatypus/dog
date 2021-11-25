import { ActionReducerMap } from "@ngrx/store";
import { notesReducer } from "./states/notes/notes.reducer";
import { settingsReducer } from "./states/settings/settings.reducer";

export interface State {
}

export const reducers: ActionReducerMap<State> = {
  settings: settingsReducer,
  notes: notesReducer
};
