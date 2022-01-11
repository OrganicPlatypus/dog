import { FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { setExpirationTimerAction } from '../../state/states/settings/settings.actions';
import { ToastrService } from 'ngx-toastr';
import { OrganizerApiService } from 'src/app/services/api/api.service';
import { UpdateNoteExpiriationTimeDto } from 'src/app/services/api/service-proxy/service-proxy';
import { NotesSignalService } from 'src/app/services/signalR/notes.signal.service';
import { NoteInformationService } from 'src/app/services/domain/note-information/note-information.service';

import * as SettingsSelectors from '../../state/states/settings/settings.selector'

@Component({
  selector: 'notes-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {
  public openMenu: boolean = false;
  public expiriationTimeSpan = this.noteInformationService.getExpirationTimeMinutes();
  expiriationMinutes = new FormControl(0, Validators.max(60));
  expirationDate = new FormControl(new Date());
  isValueChanged: boolean = false;

  constructor(
    private store: Store<NoteSettingsState>,
    private toaster: ToastrService,
    private organizerApiService: OrganizerApiService,
    public signalrService: NotesSignalService,
    private noteInformationService: NoteInformationService,
  ) { }

  ngOnInit() {
    this.store.select(SettingsSelectors.getExpirationDateSelector).subscribe(date => {
      this.expirationDate.setValue(date)
    });
    this.store.select(SettingsSelectors.getMinutesTillExpireSelector).subscribe(minutes => this.expiriationMinutes.setValue(minutes));

    this.isValueChangedDetection();
  }

  clickMenu(){
    this.openMenu = !this.openMenu;
  }

  updateExpiriationTime(){
    let noteName: string = "";
    this.store.select(SettingsSelectors.getNoteNameSelector).subscribe(name=>{
      noteName = name!;
    })
    const newExpiriationTimeMinutes = this.expiriationMinutes.value;
    const connectionId = this.signalrService.connection.connectionId;
    const updateExpiriationTimeDto = <UpdateNoteExpiriationTimeDto>{
      connectionId: connectionId,
      noteName: noteName,
      expirationMinutesRange: newExpiriationTimeMinutes,
    }
    this.organizerApiService.updateNoteExpiriationTime(updateExpiriationTimeDto).subscribe();
    this.store.dispatch(setExpirationTimerAction({expirationTimer : this.expiriationMinutes.value}));

    this.toaster.info('Notes expiration timer has been changed');
  }

  isValueChangedDetection() {
    const initialValue = this.expiriationMinutes.value;
    this.expiriationMinutes.valueChanges.subscribe(() => {
      this.isValueChanged = Object.keys(initialValue)
        .some(key => this.expiriationMinutes.value[key] != initialValue[key]);
    });
  }

}
