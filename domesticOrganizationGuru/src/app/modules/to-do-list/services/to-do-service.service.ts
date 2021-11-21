import { Injectable, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { TodoItem } from '../models/to-do';

@Injectable({
  providedIn: 'root'
})

export class ToDoService implements OnInit{
    private todos: TodoItem[] = []
    private todoList = new BehaviorSubject<TodoItem[]>(this.todos);

    constructor() { }

    ngOnInit(){
      //TODO: Get notes if any
    }

    public getToDoList = () => this.todoList;

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
      noteStatusToBeChanged.isComplete = completionFlag;
      this.todoList.next(this.todos);
      }

    public editItem(editedItem: TodoItem, noteInput: string) {
      editedItem.noteText = noteInput;
      this.todoList.next(this.todos);
    }
  }

