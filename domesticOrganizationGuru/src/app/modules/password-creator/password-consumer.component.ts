import { setExpirationDateAction } from 'src/app/state/states/settings/settings.actions';
import { NotesSignalService } from 'src/app/services/signalR/notes.signal.service';
import { OrganizerApiService } from 'src/app/services/api/api.service';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';

import * as SettingsSelectors from '../../state/states/settings/settings.selector'
import * as SettingsActions from '../../state/states/settings/settings.actions';
import * as NotesActions from '../../state/states/notes/notes.actions';
import { TodoItem } from '../to-do-list/models/to-do';

@Component({
  selector: 'password-consumer',
  templateUrl: './password-consumer.component.html',
  styleUrls: ['./password-consumer.component.scss']
})
export class PasswordConsumerComponent implements OnInit {
  @Input()
  isOpen: boolean = false;

  isPasswordRequested:boolean = false;

  password = new FormControl('', [Validators.required]);

  noteName: string = "";

  constructor(
    private store: Store<NoteSettingsState>,
    public organizerApiService: OrganizerApiService,
    public signalrService: NotesSignalService,
    public router: Router
    ) {}

  ngOnInit() {
      this.store.select(SettingsSelectors.getNoteNameSelector)
    .subscribe(noteName => { this.noteName = noteName! }
    )
  }

  JoinNoteShareing(){
    this.joinNoteSession()
  }

  public joinNoteSession(){
    this.organizerApiService
      .joinNoteSessionWithPassword(this.noteName, this.password.value)
        .subscribe((notesPack) => {
          if(notesPack){
            let todoItems : TodoItem[] = []
            notesPack.notes?.map((note) => {
              let todoItem = new TodoItem(note.noteText);
              todoItem.isComplete = note.isComplete;
              todoItems.push(todoItem);
            })
            this.store.dispatch(NotesActions.setExistingNotesAction({ notes : todoItems}));
            this.store.dispatch(SettingsActions.setExpirationDateAction({expirationDate : new Date( notesPack.expirationDate! )}))
            this.store.dispatch(SettingsActions.setExpirationTimerAction({expirationTimer : notesPack.expirationMinutesRange!}));
            this.signalrService.joinGroup(this.noteName);
            this.router.navigate(['/to-do']);
          }
        });
    this.password.setValue('');
  }
}
