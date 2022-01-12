import { createAction, props } from "@ngrx/store";

export const setExpirationTimerActionName = "[Root state] Sets expiration timer in minutes";
export const setExpirationTimerAction = createAction(setExpirationTimerActionName, props<{ expirationTimer: number }>());

export const setNoteNameActionName = "[Root state] Sets note name";
export const setNoteNameAction = createAction(setNoteNameActionName, props<{ noteName: string }>());

export const setExpirationDateActionName = "[Root state] Sets expiration date";
export const setExpirationDateAction = createAction(setExpirationDateActionName, props<{ expirationDate: Date }>());

export const clearStateActionName = "[Root state] Clear settings state";
export const clearStateAction = createAction(clearStateActionName);
