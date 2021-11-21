import { CreateNotesPackDto } from './../../services/service-proxy/service-proxy';
import { OrganizerApiService } from 'src/app/services/api/api.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { ConfigurationApiService } from 'src/app/services/api/configuration-api.service';
import { Store } from '@ngrx/store';

import * as SettingsActions from '../../state/states/settings/settings.actions';
import { getMinutesTillExpireSelector } from '../../state/states/settings/settings.selector';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';


@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.scss']
})
export class StartComponent implements OnInit {
  initialExpirationSpan = 0;

  noteName = new FormControl('');
  joinSessionByName = new FormControl('');

  constructor(
    private store: Store<NoteSettingsState>,
    public router: Router,

    public organizerApiService: OrganizerApiService,
    public configurationApiService: ConfigurationApiService
  ) {}

  ngOnInit() {
    this.configurationApiService.landingHomeConfiguration().subscribe(minutes => {
      this.initialExpirationSpan = minutes;
      this.store.dispatch(SettingsActions.setExpirationTimerAction({expirationTimer : minutes}))
    })
  }

  public createNotePack() {
    const notesPack: CreateNotesPackDto = <CreateNotesPackDto> {
      expirationMinutesRange: this.initialExpirationSpan,
      noteName: this.noteName.value
    }
    this.organizerApiService.createNote(notesPack).subscribe((noteName) => {
      this.store.dispatch(SettingsActions.setNoteNameAction({noteName : noteName}))
      this.router.navigate(['/to-do']);
    });
  }

  public joinSession(){
    this.organizerApiService.joinTheNote(this.joinSessionByName.value).subscribe((notesPack) => {
    if(notesPack){
      this.router.navigate(['/to-do']);
    }
  });
  }
}
