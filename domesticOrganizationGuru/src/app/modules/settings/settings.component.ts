import { FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { getMinutesTillExpireSelector } from 'src/app/state/states/settings/settings.selector';
import { setExpirationTimerAction } from '../../state/states/settings/settings.actions';

@Component({
  selector: 'notes-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {
  public openMenu: boolean = false;

  expiriationMinutes = new FormControl(0, Validators.max(60));

  constructor(
    private store: Store<NoteSettingsState>) { }

  ngOnInit() {
    this.store.select(getMinutesTillExpireSelector).subscribe(minutes => this.expiriationMinutes.setValue(minutes));
  }

  clickMenu(){
    this.openMenu = !this.openMenu;
  }

  submit(){
    this.store.dispatch(setExpirationTimerAction({expirationTimer : this.expiriationMinutes.value}))
  }

  updateExpiriationTime(){
    this.store.dispatch(setExpirationTimerAction({expirationTimer : this.expiriationMinutes.value}))
  }
}
