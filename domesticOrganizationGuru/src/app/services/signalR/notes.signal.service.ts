import { setExistingNotesAction } from './../../state/states/notes/notes.actions';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { TodoItem } from './../../modules/to-do-list/models/to-do';
import { NoteDto } from 'src/app/services/api/service-proxy/service-proxy';
import { Inject, Injectable, InjectionToken, Optional } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import * as SignalMethods from './signal-methods';
import { Store } from '@ngrx/store';
import { setExpirationDateAction, setExpirationTimerAction } from 'src/app/state/states/settings/settings.actions';

export const API_SIGNALR_URL = new InjectionToken<string>('API_SIGNALR_URL')

@Injectable({
  providedIn: 'any'
})
export class NotesSignalService {
  connection!: signalR.HubConnection;
  signalRUrl: string;
  isBeingEdited = false;

  constructor(
    private store: Store<NoteSettingsState>,
    @Optional() @Inject(API_SIGNALR_URL) signalRUrl? : string
  ) {
    this.signalRUrl = signalRUrl!;
   }

  public subscribeOnUpdatedNotesState(): void {
    let notesDto: NoteDto[] | undefined = undefined;
      this.connection.on(SignalMethods.UpdateNotesState, (data: NoteDto[]) => {
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

  public subscribeOnUpdatedExpirationDate(): void {
      let expiriationDate: Date | undefined = undefined;
        this.connection.on(SignalMethods.UpdateExpiriationTimeState, (data: Date) => {
          expiriationDate = new Date( data )
          if(expiriationDate){
            this.store.dispatch(setExpirationDateAction({ expirationDate : expiriationDate}))
          }
        })
        return expiriationDate
      }

      // public subscribeOnUpdatedExpirationSpan(): void {
      //   let expirationSpan: number | undefined = undefined;
      //     this.connection.on(SignalMethods.UpdateExpiriationTimeState, (data: number) => {
      //       expirationSpan = data
      //       if(expirationSpan){
      //         this.store.dispatch(setExpirationTimerAction({ expirationTimer : expirationSpan}))
      //       }
      //     })
      //     return expirationSpan
      //   }

  public joinGroup(noteName: string): void {
    this.connection
      .invoke(SignalMethods.CreateGroup, noteName)
      .catch((error) => {
        alert(`joinGroup Error!, see logs for details.${error}`);
      }
    );
  }

  public isEditingMarker(noteName: string, isEditing: boolean): void {
    this.connection
      .invoke(SignalMethods.MarkIsEditing, noteName, isEditing)
      .catch((error) => {
        alert(`Error!, see logs for details.${error}`);
      }
    );
  }

  public isEditingListener() {
      this.connection.on(SignalMethods.MarkIsEditing, (isEditingResponse: boolean) => {
        this.isBeingEdited = isEditingResponse;
      })
    }

  public startConnection(): void {
    this.connection = new signalR.HubConnectionBuilder()
        .withUrl(this.signalRUrl)
        .withAutomaticReconnect()
        .configureLogging(signalR.LogLevel.Information)
        .build();

      this.connection
        .start()
        .catch((error) => {
          console.warn(`Error while starting connection: ${error}`);
          setTimeout(this.startConnection.bind(this), 5000);
        });
    }
}
