import { NotesSignalService } from './../../services/signalR/notes.signal.service';
import { TodoItem } from './../to-do-list/models/to-do';
import { Client, CreateNoteDto } from '../../services/api/service-proxy/service-proxy';
import { OrganizerApiService } from 'src/app/services/api/api.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfigurationApiService } from 'src/app/services/api/configuration-api.service';
import { Store } from '@ngrx/store';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';

import * as SettingsActions from '../../state/states/settings/settings.actions';
import * as NotesActions from '../../state/states/notes/notes.actions';
import * as SettingsSelectors from '../../state/states/settings/settings.selector'

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.scss']
})
export class StartComponent implements OnInit {
  initialExpirationSpan = 0;

  isPasswordRequested:boolean = false;

  createNote = new FormGroup({
    newPassword: new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(30)]),
    noteName: new FormControl('', [Validators.minLength(1), Validators.maxLength(100)])
  })

  joinSessionByName = new FormControl('', [Validators.minLength(1), Validators.maxLength(100)]);

  hubHelloMessage: string ="";
  isPasswordCreatorOpen: boolean;

  constructor(
    private store: Store<NoteSettingsState>,
    public router: Router,
    public nswagServiceProxy: Client,
    public organizerApiService: OrganizerApiService,
    public configurationApiService: ConfigurationApiService,
    public signalrService: NotesSignalService
  ) {
    this.isPasswordCreatorOpen = false
    this.createNote = new FormGroup({
      newPassword: new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(30)]),
      noteName: new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(100)])
    })
  }

  ngOnInit() {
    this.configurationApiService.landingHomeConfiguration().subscribe(minutes => {
      this.initialExpirationSpan = minutes;
      this.store.dispatch(SettingsActions.setExpirationTimerAction({expirationTimer : minutes}))
    })
  }

  public createNotePack() {
    this.store.dispatch( SettingsActions.setNoteNameAction({ noteName : this.createNote.get('noteName')?.value }));
    this.isPasswordCreatorOpen = true;
    const newPassword = this.createNote.get('newPassword')?.value;
    this.CreateNotes(newPassword);
    this.createNote.patchValue({
      noteName: ''
    });
  }

  public CreateNotes(newPassword: string){
    let noteInitialSettingsDto = new CreateNoteDto();
    this.store
      .select(SettingsSelectors.getSettingsStateSelector)
      .subscribe(settings => {
        noteInitialSettingsDto.expirationMinutesRange = settings.minutesUntilExpire,
        noteInitialSettingsDto.noteName = settings.noteName,
        noteInitialSettingsDto.password = newPassword != '' ? newPassword : undefined
        }
      );
      this.organizerApiService
      .createNote(noteInitialSettingsDto)
        .subscribe((expirationDateDto) => {
          this.store.dispatch(SettingsActions.setExpirationDateAction({ expirationDate : new Date( expirationDateDto.expirationDate! )}));
          this.signalrService.joinGroup( noteInitialSettingsDto.noteName! );
          this.router.navigate(['/to-do']);
          });
  }

  public joinSession(){
    const sessionName = this.joinSessionByName.value;

    this.organizerApiService.isPasswordRequired(sessionName).subscribe(
      isRequired => {
        if(isRequired){
          this.store.dispatch(SettingsActions.setNoteNameAction({ noteName : sessionName }))
          this.router.navigate([ '/auth' ]);
        }
        else{
          this.bindSessionValues(sessionName);
        }
      }
    )
  }

  bindSessionValues(sessionName: string){
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

  buttonEnabled(): boolean{
    return !this.isPasswordRequested || this.isPasswordRequested && this.createNote.valid;
  }

  resetPassword(): void {
    this.createNote.patchValue({
      newPassword: ''
    });
  }
}
