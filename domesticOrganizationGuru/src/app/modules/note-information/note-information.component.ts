import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { zip } from 'rxjs';
import { NoteInformationService } from 'src/app/services/domain/note-information/note-information.service';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { getMinutesTillExpireSelector, getNoteNameSelector } from 'src/app/state/states/settings/settings.selector';
import { ToDoService } from '../../services/domain/services/to-do-service.service';

@Component({
  selector: 'note-information',
  templateUrl: './note-information.component.html',
  styleUrls: ['./note-information.component.scss']
})
export class NoteInformationComponent implements OnInit {
  noteName: string = '';
  //expirationSpan: number = 0;
  //expirationDate: string ="";


  //TODO: extract note information service
  public expiriationTimeSpan = this.noteInformationService.getExpirationTime();
  public expirationDate = this.noteInformationService.getExpirationDate();

  constructor(
    private store: Store<NoteSettingsState>,
    private noteInformationService: NoteInformationService,
  ) { }

  ngOnInit() {
    this.store.select(getNoteNameSelector)
    .subscribe( results => {
      this.noteName = results!
      // this.formExpiriationPointDate(results[1]!);
      //this.formExpiriationPointDate(this.expiriationTimeSpan.value);
    })
  }

  // private formExpiriationPointDate(expirationSpan: number) {
  //   //this.expirationSpan = expirationSpan;
  //   let expirationDate = new Date(Date.now() + expirationSpan! * 6000);
  //   this.expirationDate = `${expirationDate.toLocaleDateString()} ${expirationDate.toLocaleTimeString()}`
  // }

  expiriationTimeSpanValue(): number {
    return this.expiriationTimeSpan.value;
  }

  expiriationDateValue(): string {
    return this.expirationDate.value;
  }
}
