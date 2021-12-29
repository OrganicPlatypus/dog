import { NotesSessionDto } from './../api/service-proxy/service-proxy';
import { TodoItem } from './../../modules/to-do-list/models/to-do';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { getNoteNameSelector } from 'src/app/state/states/settings/settings.selector';
import { ConfigurationApiService } from '../api/configuration-api.service';
import { OrganizerApiService } from '../api/api.service';
import { NotesSignalService } from '../signalR/notes.signal.service';

import * as SettingsActions from '../../state/states/settings/settings.actions';
import * as NotesActions from '../../state/states/notes/notes.actions';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

@Injectable()
export class JoinLandingGuard implements CanActivate {
  connectionEstablished: boolean = false;
  redirectToSession: string = '';

  constructor(
    private router: Router,
    private store: Store<NoteSettingsState>,
    private configurationApiService: ConfigurationApiService,
    private organizerApiService: OrganizerApiService,
    public signalrService: NotesSignalService
    ) {}

  canActivate(routeSnapshot: ActivatedRouteSnapshot) {
    let isThereAName = false;
    this.redirectToSession = routeSnapshot.data['joinSessionRedirect'];
    let sessionName = routeSnapshot.params['name'];
    this.configurationApiService.landingHomeConfiguration()
      .subscribe(minutes => {
        this.bindSessionValues(minutes, sessionName);
      })
      return isThereAName;
    }

    private bindSessionValues(minutes: number, sessionName: string){
      this.organizerApiService
      .joinTheNote(sessionName)
        .subscribe((notesPack) => {
            let todoItems : TodoItem[] = []
            if(notesPack.notes && notesPack.notes.length > 0){
              notesPack.notes?.map((note) => {
                let todoItem = new TodoItem(note.noteText)
                todoItem.isComplete = note.isComplete;
                todoItems.push(todoItem)
              })
            }
            this.store.dispatch(SettingsActions.setExpirationTimerAction({expirationTimer : minutes}))
            this.store.dispatch(NotesActions.setExistingNotesAction({ notes : todoItems}))
            this.store.dispatch(SettingsActions.setNoteNameAction({ noteName : sessionName}))
            this.signalrService.joinGroup(sessionName);
            this.connectionEstablished = true;
            this.router.navigate([this.redirectToSession])
        });
    }
}
