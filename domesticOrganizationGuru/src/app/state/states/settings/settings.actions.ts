import { createAction, props } from "@ngrx/store";

export const setExpirationTimerActionName = "[Root state] Sets expiration timer in minutes";
export const setExpirationTimerAction = createAction(setExpirationTimerActionName, props<{ expirationTimer: number }>());

export const setNoteNameActionName = "[Root state] Sets note name";
export const setNoteNameAction = createAction(setNoteNameActionName, props<{ noteName: string }>());

export const clearStateActionName = "[Root state] Clear settings state";
export const clearStateAction = createAction(clearStateActionName);
