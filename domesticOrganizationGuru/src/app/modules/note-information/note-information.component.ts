import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { NoteInformationService } from 'src/app/services/domain/note-information/note-information.service';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { getNoteNameSelector } from 'src/app/state/states/settings/settings.selector';

@Component({
  selector: 'note-information',
  templateUrl: './note-information.component.html',
  styleUrls: ['./note-information.component.scss']
})
export class NoteInformationComponent implements OnInit {
  noteName: string = '';

  public expiriationTimeSpanMinutes = this.noteInformationService.getExpirationTimeMinutes();
  public expiriationTimeSpanSeconds = this.noteInformationService.getExpirationTimeSeconds();
  public expirationDate = this.noteInformationService.getExpirationDate();

  constructor(
    private store: Store<NoteSettingsState>,
    private noteInformationService: NoteInformationService,
  ) { }

  ngOnInit() {
    this.store.select(getNoteNameSelector)
    .subscribe( results => {
      this.noteName = results!
    })

    this.expiriationTimeSpanMinutes = this.noteInformationService.getExpirationTimeMinutes();
    this.expirationDate = this.noteInformationService.getExpirationDate();
  }

  expiriationTimeSpanMinutesValue(): number {
    return this.expiriationTimeSpanMinutes.value;
  }

  expiriationTimeSpanSecondsValue(): number {
    return this.expiriationTimeSpanSeconds.value;
  }

  expiriationDateValue(): string {
    return this.expirationDate.value;
  }
}
