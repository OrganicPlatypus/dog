import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { zip } from 'rxjs';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { getMinutesTillExpireSelector, getNoteNameSelector } from 'src/app/state/states/settings/settings.selector';
import { ToDoService } from '../to-do-list/services/to-do-service.service';

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
  public expiriationTimeSpan = this.toDoService.getExpirationTime();
  public expirationDate = this.toDoService.getExpirationDate();

  constructor(
    private store: Store<NoteSettingsState>,
    private toDoService: ToDoService,
  ) { }

  ngOnInit() {
    zip(
    this.store.select(getNoteNameSelector),
    this.store.select(getMinutesTillExpireSelector)
    )
    .subscribe( results => {
      this.noteName = results[0]!
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
