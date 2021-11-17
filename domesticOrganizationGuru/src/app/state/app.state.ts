export interface State {
  // minutesUntilExpire: number | undefined;
  // noteName: string | undefined;
}

export interface SettingsState extends State{
  settings: NoteSettingsState
}

export interface NoteSettingsState {
  minutesUntilExpire: number | undefined;
  noteName: string | undefined;
}

export const initialState: NoteSettingsState = {
  minutesUntilExpire: 0,
  noteName: ""
}


// export const initialState: State = {
//   minutesUntilExpire: 0,
//   noteName: ""
// }
