import { setExistingNotesAction } from './../../state/states/notes/notes.actions';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { TodoItem } from './../../modules/to-do-list/models/to-do';
import { NoteDto, UpdateNoteRequestDto } from 'src/app/services/service-proxy/service-proxy';
import { Inject, Injectable, InjectionToken, Optional } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Store } from '@ngrx/store';

export const API_SIGNALR_URL = new InjectionToken<string>('API_SIGNALR_URL')

@Injectable({
  providedIn: 'any'
})
export class NotesSignalService {
  connection!: signalR.HubConnection;

  hubHelloMessage: BehaviorSubject<string>;
  currentNotesState: BehaviorSubject<UpdateNoteRequestDto>;
  signalRUrl: string;

  constructor(
    private store: Store<NoteSettingsState>,
    @Optional() @Inject(API_SIGNALR_URL) signalRUrl? : string
  ) {
    this.signalRUrl = signalRUrl!;
    this.hubHelloMessage = new BehaviorSubject<string>("");
    this.currentNotesState = new BehaviorSubject<UpdateNoteRequestDto>(new UpdateNoteRequestDto());
   }


   private setSignalrClientMethods(): void {
    this.connection.on('DisplayMessage', (message: string) => {
      this.hubHelloMessage.next(message);
    });
    }

  public subscribeOnCurrentlyUpdateNote(): void {
    let notesDto: NoteDto[] | undefined = undefined;
      this.connection.on('UpdateNotesState', (data: NoteDto[]) => {
        notesDto = data
        if(notesDto){
          let todoItems : TodoItem[] = []
          notesDto.map((note) => {
            let todoItem = new TodoItem(note.noteText)
            todoItem.isComplete = note.isComplete;
            todoItems.push(todoItem)
          })
          this.store.dispatch(setExistingNotesAction({ notes : todoItems}))
        }
      })
      return notesDto
    }

  public joinGroup(noteName: string) {
    this.connection
      .invoke('CreateGroup', noteName)
      .catch(error => {
        console.log(`SignalrDemoHub.Hello() error: ${error}`);
        alert('SignalrDemoHub.Hello() error!, see console for details.');
      }
    );
  }

  public startConnection(): void {
    this.connection = new signalR.HubConnectionBuilder()
        .withUrl(this.signalRUrl)
        .withAutomaticReconnect()
        .configureLogging(signalR.LogLevel.Information)
        .build();

      this.connection
        .start()
        .then(() => {
          console.log(`SignalR connection success! connectionId: ${this.connection.connectionId} `);
        })
        .catch((error) => {
          console.log(`Error while starting connection: ${error}`);
          setTimeout(this.startConnection.bind(this), 5000);
        });
    }
}
