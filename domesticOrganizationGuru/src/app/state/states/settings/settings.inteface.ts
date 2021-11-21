import { State } from "../../app.state";

export interface SettingsState extends State{
  settings: NoteSettingsState
}

export const initialSettingsState: NoteSettingsState = {
  minutesUntilExpire: 0,
  noteName: ""
}

export interface NoteSettingsState {
  minutesUntilExpire: number | undefined;
  noteName: string | undefined;
}
