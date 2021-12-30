import { FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { setExpirationTimerAction } from '../../state/states/settings/settings.actions';
import { ToastrService } from 'ngx-toastr';
import { OrganizerApiService } from 'src/app/services/api/api.service';

import * as SettingsSelectors from '../../state/states/settings/settings.selector'

@Component({
  selector: 'notes-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {
  public openMenu: boolean = false;

  expiriationMinutes = new FormControl(0, Validators.max(60));

  constructor(
    private store: Store<NoteSettingsState>,
    private toaster: ToastrService,
    private organizerApiService: OrganizerApiService,

  ) { }

  ngOnInit() {
    this.store.select(SettingsSelectors.getMinutesTillExpireSelector).subscribe(minutes => this.expiriationMinutes.setValue(minutes));
  }

  clickMenu(){
    this.openMenu = !this.openMenu;
  }

  updateExpiriationTime(){
    const noteName = this.store.select(SettingsSelectors.getNoteNameSelector);
    const newExpiriationTimeMinutes = this.expiriationMinutes.value;
    const updateExpiriationTimeDto = {}
    this.organizerApiService.updateNoteExpiriationTime(updateExpiriationTimeDto);
    this.store.dispatch(setExpirationTimerAction({expirationTimer : this.expiriationMinutes.value}))

    this.toaster.info('Notes expiration timer has been changed');
  }
}
