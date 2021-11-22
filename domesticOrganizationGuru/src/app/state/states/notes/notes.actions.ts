import { TodoItem } from './../../../modules/to-do-list/models/to-do';
import { createAction, props } from "@ngrx/store";

export const setExistingNotesActionName = "[Root state] Sets existing notes";
export const setExistingNotesAction = createAction(setExistingNotesActionName, props<{ notes: TodoItem[] }>());

export const clearStateActionName = "[Root state] Clear notes state";
export const clearStateAction = createAction(clearStateActionName);
