import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { zip } from 'rxjs';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { getMinutesTillExpireSelector, getNoteNameSelector } from 'src/app/state/states/settings/settings.selector';

@Component({
  selector: 'note-information',
  templateUrl: './note-information.component.html',
  styleUrls: ['./note-information.component.scss']
})
export class NoteInformationComponent implements OnInit {
  noteName: string = '';
  expirationSpan: number = 0;
  expirationDate: Date = new Date();

  constructor(
    private store: Store<NoteSettingsState>,
  ) { }

  ngOnInit() {
    zip(
    this.store.select(getNoteNameSelector),
    this.store.select(getMinutesTillExpireSelector)
    )
    .subscribe( results => {
      this.noteName = results[0]!
      this.expirationSpan = results[1]!
      this.expirationDate = new Date( Date.now() + results[1]!*6000 )
    })
  }

}
