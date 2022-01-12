import { NotePointsState } from '../../../state/states/notes/notes.inteface';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { TodoItem } from '../../../modules/to-do-list/models/to-do';
import { Store } from '@ngrx/store';
import { getExistingNotesSelector } from '../../../state/states/notes/notes.selector';
import { getMinutesTillExpireSelector } from 'src/app/state/states/settings/settings.selector';

@Injectable({
  providedIn: 'root'
})

export class ToDoService {

    todos: TodoItem[] = []
    expiriationTime: number = 0;
    expirationDate: string = '';
    private todoList = new BehaviorSubject<TodoItem[]>(this.todos);
    private expiriationTimeSpan = new BehaviorSubject<number>(this.expiriationTime);
    private expirationDateFormed = new BehaviorSubject<string>(this.expirationDate);

    constructor(private store: Store<NotePointsState>) { }

    public getToDoList = () => {
      this.store.select(getExistingNotesSelector)
      .subscribe( notes => {
        if(notes.length === 0){
          this.todos.length = 0
          this.todoList.next(this.todos);
        }
        if(notes.length > 0){
          this.todos.length = 0
          notes
            .map(note => {
              this.todos.push(note)
              this.todoList.next(this.todos);
          })
        }
      })
      return this.todoList;
    }

    // public getExpirationTime = () => {
    //   this.store.select(getMinutesTillExpireSelector)
    //     .subscribe(minutes => {
    //       if(minutes){
    //         this.expiriationTime = minutes!;
    //         this.expiriationTimeSpan.next(this.expiriationTime);
    //       }
    //     })
    //   return this.expiriationTimeSpan;
    // }

    // public getExpirationDate = () => {
    //   let expirationDate = new Date(Date.now() + this.expiriationTimeSpan.value! * 60000);
    //   this.expirationDate = `${expirationDate.toLocaleDateString()} ${expirationDate.toLocaleTimeString()}`;
    //   this.expirationDateFormed.next(this.expirationDate);
    //   return this.expirationDateFormed;
    // }

    public addItem = (newItem: TodoItem) =>
      this.todos.unshift(newItem) && this.todoList.next(this.todos);

    public removeNoteItem(noteToRemove: TodoItem) {
        const noteToRemoveIndex = this.todos.findIndex(note => note === noteToRemove);
        if (noteToRemoveIndex > -1) {
        this.todos.splice(noteToRemoveIndex, 1);
        this.todoList.next(this.todos);
        }
      }

    public completeItem(noteStatusToBeChanged: TodoItem, completionFlag: boolean) {
        const noteToRemoveIndex = this.todos.findIndex(note => note === noteStatusToBeChanged);
        this.todos[noteToRemoveIndex] = {
            noteText:noteStatusToBeChanged.noteText,
            isComplete : completionFlag
          } as TodoItem;
        this.todoList.next(this.todos);
      }

    public editItem(editedItem: TodoItem, noteInput: string) {
      const noteToRemoveIndex = this.todos.findIndex(note => note === editedItem);
      this.todos[noteToRemoveIndex] = {
          noteText: noteInput,
          isComplete : editedItem.isComplete
        } as TodoItem;
      this.todoList.next(this.todos);
    }

    resetNotes() {
      this.todoList = new BehaviorSubject<TodoItem[]>(this.todos);
    }
  }

