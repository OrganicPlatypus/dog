import { createFeatureSelector, createSelector } from '@ngrx/store';
import { NoteSettingsState } from './settings.inteface';

const getSettingsState = createFeatureSelector<NoteSettingsState>('settings');

export const getMinutesTillExpireSelector = createSelector(getSettingsState, (state:NoteSettingsState) => state.minutesUntilExpire);
export const getNoteNameSelector = createSelector(getSettingsState, (state:NoteSettingsState) => state.noteName);
export const getExpirationDateSelector = createSelector(getSettingsState, (state:NoteSettingsState) => state.expirationDate);
export const getSettingsStateSelector = createSelector(getSettingsState, (state:NoteSettingsState) => state);
