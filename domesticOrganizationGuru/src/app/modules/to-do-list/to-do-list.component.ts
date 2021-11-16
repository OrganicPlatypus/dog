import { Component, OnInit } from '@angular/core';
import { OrganizerApiService } from 'src/app/services/api/api.service';
import { UpdateNoteRequestDto } from 'src/app/services/service-proxy/service-proxy';
import { TodoItem } from './models/to-do';
import { ToDoService } from './services/to-do-service.service';

@Component({
  selector: 'to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.scss']
})
export class ToDoListComponent implements OnInit {
  public toDoList = this.toDoService.getToDoList();
  public todoItem = new TodoItem('');

  editValue: boolean = false;

  notesPackName: string = "";
  notesLifespan: number = 0;
  constructor(
    private toDoService: ToDoService,
    private organizerApiService: OrganizerApiService
  ) {
  }

  ngOnInit() {
    this.toDoList = this.toDoService.getToDoList();
    //TODO: dodać guarda z przekierowaniem na landing page jeżeli w storze nie ma dodanej nazwy notatek.
  }

  updateNotesPack() {
    this.toDoService.addItem(new TodoItem(this.todoItem.noteText));

    const updateNoteRequestDto = <UpdateNoteRequestDto> {
      noteName: this.notesPackName,
      expirationMinutesRange: this.notesLifespan,

      notesPack: this.toDoList.value
    }
    this.organizerApiService.updateNotePack(updateNoteRequestDto).subscribe();

    this.todoItem.noteText = undefined;
  }

  addNewItem() {
    this.toDoService.addItem(new TodoItem(this.todoItem.noteText));

    this.todoItem.noteText = undefined;
  }

  deleteNoteItem(noteToRemove: TodoItem) {
    this.toDoService.removeNoteItem(noteToRemove);
  }

  editItem(i: any) {

  }

  markItemAsDone(item: any) {
    // this.inputValue.content = item.content;
    // this.inputValue.isDone = true;
    // this.todoDoc = this.afs.doc(`Todolist/${item.id}`);
    // this.todoDoc.update(this.inputValue);
    // this.inputValue.content = "";
    //this.openSnackBar("Item Done!", "Dismiss");
  }
  markItemAsNotDone(item: any) {
    // this.inputValue.content = item.content;
    // this.inputValue.isDone = false;
    // this.todoDoc = this.afs.doc(`Todolist/${item.id}`);
    // this.todoDoc.update(this.inputValue);
    // this.inputValue.content = "";
    // this.openSnackBar("Item Not Done!", "Dismiss");
  }
  saveNewItem() {
    if (true) {

      // this.inputValue.isDone = false;
      // this.inputValue.datemodified = new Date();
      // this.todoDoc = this.afs.doc(`Todolist/${this.inputId}`);
      // this.todoDoc.update(this.inputValue);
      // this.editValue = false;
      // this.inputValue.content = "";
      // this.openSnackBar("Updated Successfuly!", "Dismiss");
    }
  }
  // openSnackBar(message: string, action: string) {
  //   this.snackBar.open(message, action, {
  //     duration: 2000,
  //     verticalPosition: 'top',
  //   });
  // }

  // toggleCheck(i) {

  // }
}
