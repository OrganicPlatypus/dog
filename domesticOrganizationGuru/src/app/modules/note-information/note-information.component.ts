import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
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
export class NoteInformationComponent implements OnInit, OnChanges {
  noteName: string = '';

  public expiriationTimeSpan = this.noteInformationService.getExpirationTimeMinutes();
  public expirationDate = this.noteInformationService.getExpirationDate();

  constructor(
    private store: Store<NoteSettingsState>,
    private noteInformationService: NoteInformationService,
  ) { }

  ngOnChanges(changes: SimpleChanges): void {
  }

  ngOnInit() {
    this.store.select(getNoteNameSelector)
    .subscribe( results => {
      this.noteName = results!
    })

    this.expiriationTimeSpan = this.noteInformationService.getExpirationTimeMinutes();
    this.expirationDate = this.noteInformationService.getExpirationDate();
  }

  expiriationTimeSpanValue(): number {
    return this.expiriationTimeSpan.value;
  }

  expiriationDateValue(): string {
    return this.expirationDate.value;
  }
}
