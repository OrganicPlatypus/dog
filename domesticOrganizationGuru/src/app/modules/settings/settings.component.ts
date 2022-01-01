import { FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { setExpirationTimerAction } from '../../state/states/settings/settings.actions';
import { ToastrService } from 'ngx-toastr';
import { OrganizerApiService } from 'src/app/services/api/api.service';

import * as SettingsSelectors from '../../state/states/settings/settings.selector'
import { UpdateNoteExpiriationTimeDto } from 'src/app/services/api/service-proxy/service-proxy';
import { NotesSignalService } from 'src/app/services/signalR/notes.signal.service';
import { ToDoService } from '../to-do-list/services/to-do-service.service';

@Component({
  selector: 'notes-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {
  public openMenu: boolean = false;
  public expiriationTimeSpan = this.toDoService.getExpirationTime();
  expiriationMinutes = new FormControl(0, Validators.max(60));

  constructor(
    private store: Store<NoteSettingsState>,
    private toaster: ToastrService,
    private organizerApiService: OrganizerApiService,
    public signalrService: NotesSignalService,
    private toDoService: ToDoService,
  ) { }

  ngOnInit() {
    this.store.select(SettingsSelectors.getMinutesTillExpireSelector).subscribe(minutes => this.expiriationMinutes.setValue(minutes));
  }

  clickMenu(){
    this.openMenu = !this.openMenu;
  }

  updateExpiriationTime(){
    let noteName: string = "";
    this.store.select(SettingsSelectors.getNoteNameSelector).subscribe(name=>{
      noteName = name!;
    })
    // const newExpiriationTimeMinutes = this.expiriationTimeSpan.value;
    const newExpiriationTimeMinutes = this.expiriationMinutes.value;
    const connectionId = this.signalrService.connection.connectionId;
    const updateExpiriationTimeDto = <UpdateNoteExpiriationTimeDto>{
      connectionId: connectionId,
      noteName: noteName,
      expirationMinutesRange: newExpiriationTimeMinutes,
    }
    this.organizerApiService.updateNoteExpiriationTime(updateExpiriationTimeDto).subscribe();
    this.store.dispatch(setExpirationTimerAction({expirationTimer : this.expiriationMinutes.value}))
    // this.store.dispatch(setExpirationTimerAction({expirationTimer : this.expiriationTimeSpan.value}))

    this.toaster.info('Notes expiration timer has been changed');
  }
}
