import { NotesSignalService } from 'src/app/services/signalR/notes.signal.service';
import { setExpirationDateAction } from 'src/app/state/states/settings/settings.actions';
import { OrganizerApiService } from 'src/app/services/api/api.service';
import { CreateNotesPackDto } from './../../api/service-proxy/service-proxy';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';

import * as SettingsSelectors from '../../../state/states/settings/settings.selector'
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotesService {

constructor(
  private store: Store<NoteSettingsState>,
  public organizerApiService: OrganizerApiService,
  public signalrService: NotesSignalService,
  public router: Router) { }

public CreateNotes(newPassword: string){
    this.store
      .select(SettingsSelectors.getSettingsStateSelector)
      .subscribe(settings => {
        const notesPack: CreateNotesPackDto = <CreateNotesPackDto>{
          expirationMinutesRange: settings.minutesUntilExpire,
          noteName: settings.noteName,
          password: newPassword != '' ? newPassword : null
        };
        this.organizerApiService
          .createNote(notesPack)
            .subscribe((expirationDateDto) => {
              this.store.dispatch(setExpirationDateAction({ expirationDate : new Date( expirationDateDto.expirationDate! )}));
              this.signalrService.joinGroup( notesPack.noteName! );
              // this.router.navigate(['/to-do']);
              });
      });
  }
}
