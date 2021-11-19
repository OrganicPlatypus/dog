import { getNoteNameSelector } from '../../state/root.selector';
import { NoteSettingsState } from './../../state/app.state';
import { Component, OnInit } from '@angular/core';
import { OrganizerApiService } from 'src/app/services/api/api.service';
import { UpdateNoteRequestDto } from 'src/app/services/service-proxy/service-proxy';
import { TodoItem } from './models/to-do';
import { ToDoService } from './services/to-do-service.service';
import { Store } from '@ngrx/store';

@Component({
  selector: 'to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.scss']
})
export class ToDoListComponent implements OnInit {
  public toDoList = this.toDoService.getToDoList();
  public todoItem = new TodoItem('');

  noteInput: string | undefined = undefined;

  editValue: boolean = false;

  notesPackName: string = "";
  notesLifespan: number = 0;
  constructor(
    private toDoService: ToDoService,
    private organizerApiService: OrganizerApiService,
    private store: Store<NoteSettingsState>,
  ) {
  }

  ngOnInit() {
    this.toDoList = this.toDoService.getToDoList();
    //TODO: dodać guarda z przekierowaniem na landing page jeżeli w storze nie ma dodanej nazwy notatek.
    this.store.select(getNoteNameSelector).subscribe(x=> {console.log('getNoteNameSelector NAME: ', x)})
  }

  updateNotesPack() {
    this.toDoService.addItem(new TodoItem(this.noteInput));

    const updateNoteRequestDto = <UpdateNoteRequestDto> {
      noteName: this.notesPackName,
      expirationMinutesRange: this.notesLifespan,

      notesPack: this.toDoList.value
    }
    this.organizerApiService.updateNotePack(updateNoteRequestDto).subscribe();
    this.noteInput = undefined;
  }

  addNewItem() {
    if (this.noteInput && this.noteInput !== ""){

      this.toDoService.addItem(new TodoItem(this.noteInput));

      this.noteInput = undefined;
    }
  }

  deleteNoteItem(noteToRemove: TodoItem) {
    this.toDoService.removeNoteItem(noteToRemove);
  }

  editItem(editedItem: TodoItem) {
    this.todoItem = editedItem;
    this.noteInput = editedItem.noteText;
    this.editValue = true;
  }

  markItemAsDone(markedItem: TodoItem) {
    this.toDoService.completeItem(markedItem, true);
    //this.openSnackBar("Item Done!", "Dismiss");
  }
  markItemAsNotDone(markedItem: TodoItem) {
    this.toDoService.completeItem(markedItem, false);
    // this.openSnackBar("Item Not Done!", "Dismiss");
  }
  saveChangedItem() {
    if (this.noteInput && this.noteInput !== "") {
      this.toDoService.editItem(this.todoItem, this.noteInput);
      this.editValue = false;
      this.noteInput = undefined;
      this.todoItem = new TodoItem('');
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
