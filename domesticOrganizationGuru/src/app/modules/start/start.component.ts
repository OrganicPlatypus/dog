import { NotesSignalService } from './../../services/signalR/notes.signal.service';
import { TodoItem } from './../to-do-list/models/to-do';
import { Client, CreateNotesPackDto, NoteSettingsDto } from '../../services/api/service-proxy/service-proxy';
import { OrganizerApiService } from 'src/app/services/api/api.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, Validators } from '@angular/forms';
import { ConfigurationApiService } from 'src/app/services/api/configuration-api.service';
import { Store } from '@ngrx/store';

import * as SettingsActions from '../../state/states/settings/settings.actions';
import * as NotesActions from '../../state/states/notes/notes.actions';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.scss']
})
export class StartComponent implements OnInit {
  initialExpirationSpan = 0;

  noteName = new FormControl('', [Validators.minLength(1), Validators.maxLength(100)]);
  joinSessionByName = new FormControl('', [Validators.minLength(1), Validators.maxLength(100)]);

  hubHelloMessage: string ="";

  constructor(
    private store: Store<NoteSettingsState>,
    public router: Router,
    public nswagServiceProxy: Client,
    public organizerApiService: OrganizerApiService,
    public configurationApiService: ConfigurationApiService,
    public signalrService: NotesSignalService
  ) {}

  ngOnInit() {
    this.configurationApiService.landingHomeConfiguration().subscribe(minutes => {
      this.initialExpirationSpan = minutes;
      this.store.dispatch(SettingsActions.setExpirationTimerAction({expirationTimer : minutes}))
    })
  }

  public createNotePack() {
    const noteName = this.noteName.value;
    const notesPack: CreateNotesPackDto = <CreateNotesPackDto> {
      expirationMinutesRange: this.initialExpirationSpan,
      noteName: noteName
    };
    this.organizerApiService
      .createNote(notesPack)
        .subscribe((expirationDateDto) => {
          this.store.dispatch( SettingsActions.setNoteNameAction({noteName : noteName}))
          this.store.dispatch( SettingsActions
            .setExpirationDateAction({ expirationDate : new Date( expirationDateDto.expirationDate! ) }
            )
          )
          this.signalrService.joinGroup( noteName );
          this.router.navigate(['/to-do']);
        });
    this.noteName.setValue('');
  }

  public joinSession(){
    const sessionName = this.joinSessionByName.value;
    this.organizerApiService
      .joinTheNote(sessionName)
        .subscribe((notesPack) => {
          if(notesPack){
            let todoItems : TodoItem[] = []
            notesPack.notes?.map((note) => {
              let todoItem = new TodoItem(note.noteText);
              todoItem.isComplete = note.isComplete;
              todoItems.push(todoItem);
            })
            console.log('notesPack', notesPack);
            this.store.dispatch(NotesActions.setExistingNotesAction({ notes : todoItems}));
            this.store.dispatch(SettingsActions.setNoteNameAction({ noteName : sessionName}));
            this.store.dispatch(SettingsActions.setExpirationDateAction({expirationDate : new Date( notesPack.expirationDate! )}))
            this.store.dispatch(SettingsActions.setExpirationTimerAction({expirationTimer : notesPack.expirationMinutesRange!}));
            this.signalrService.joinGroup(sessionName);
            this.router.navigate(['/to-do']);
          }
        });
    this.joinSessionByName.setValue('');
  }
}
