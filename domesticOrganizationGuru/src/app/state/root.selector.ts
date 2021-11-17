import { createFeatureSelector, createSelector, Store } from '@ngrx/store';
import { NoteSettingsState } from './app.state';

const getSettingsState = createFeatureSelector<NoteSettingsState>('settings');

export const getMinutesTillExpireSelector = createSelector(getSettingsState, (state:NoteSettingsState) => state.minutesUntilExpire);
export const getNoteNameSelector = createSelector(getSettingsState, (state:NoteSettingsState) => state.noteName);

