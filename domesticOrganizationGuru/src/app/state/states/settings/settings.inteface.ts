import { State } from "../../app.state";

export interface SettingsState extends State{
  settings: NoteSettingsState
}

export const initialSettingsState: NoteSettingsState = {
  minutesUntilExpire: 0,
  noteName: "",
  expirationDate: undefined
}

export interface NoteSettingsState {
  minutesUntilExpire: number | undefined;
  noteName: string | undefined;
  expirationDate: Date | undefined;
}
