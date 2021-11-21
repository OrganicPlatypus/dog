import { createFeatureSelector, createSelector } from '@ngrx/store';
import { NotePointsState } from './notes.inteface';

const getSettingsState = createFeatureSelector<NotePointsState>('notes');

export const getExistingNotesSelector = createSelector(getSettingsState, (state:NotePointsState) => state.notes);
