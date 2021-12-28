import { NotePointsState } from './../../../state/states/notes/notes.inteface';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { TodoItem } from '../models/to-do';
import { Store } from '@ngrx/store';
import { getExistingNotesSelector } from './../../../state/states/notes/notes.selector';
import { clearNotesStateAction, setExistingNotesAction } from './../../../state/states/notes/notes.actions';

@Injectable({
  providedIn: 'root'
})

export class ToDoService {
    todos: TodoItem[] = []
    private todoList = new BehaviorSubject<TodoItem[]>(this.todos);

    constructor(private store: Store<NotePointsState>) { }

    public getToDoList = () => {
      this.store.select(getExistingNotesSelector)
      .subscribe( notes => {
        if(notes.length === 0){
          this.todos.length = 0
          this.todoList.next(this.todos);
        }
        if(notes.length > 0){
          console.log('inside', notes)
          this.todos.length = 0
          notes
            .map(note => {
              this.todos.push(note)
              this.todoList.next(this.todos);
          })
          //this.store.dispatch(clearNotesStateAction())
        }
      })
      return this.todoList;
    }

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

