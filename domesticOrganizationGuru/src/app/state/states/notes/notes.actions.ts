import { TodoItem } from './../../../modules/to-do-list/models/to-do';
import { createAction, props } from "@ngrx/store";

export const setExistingNotesActionName = "[Notes state] Sets existing notes";
export const setExistingNotesAction = createAction(setExistingNotesActionName, props<{ notes: TodoItem[] }>());

export const clearNotesStateActionName = "[Notes state] Clear notes state";
export const clearNotesStateAction = createAction(clearNotesStateActionName);
