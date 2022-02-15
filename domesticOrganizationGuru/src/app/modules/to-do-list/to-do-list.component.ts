import { NotesSignalService } from './../../services/signalR/notes.signal.service';
import { Component, OnInit } from '@angular/core';
import { OrganizerApiService } from 'src/app/services/api/api.service';
import { UpdateNoteRequestDto } from 'src/app/services/api/service-proxy/service-proxy';
import { TodoItem } from './models/to-do';
import { ToDoService } from '../../services/domain/services/to-do-service.service';
import { Store } from '@ngrx/store';
import * as SettingsSelectors from '../../state/states/settings/settings.selector'
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { NoteInformationService } from 'src/app/services/domain/note-information/note-information.service';

@Component({
  selector: 'to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.scss']
})
export class ToDoListComponent implements OnInit {
  public toDoList = this.toDoService.getToDoList();
  public todoItem = new TodoItem('');
  public expiriationTimeSpan = this.noteInformationService.getExpirationTimeMinutes();

  noteInput: string | undefined = undefined;

  editValue: boolean = false;

  notesPackName: string = '';


  newNotesState!: UpdateNoteRequestDto;

  constructor(
    private toDoService: ToDoService,
    private noteInformationService: NoteInformationService,
    private organizerApiService: OrganizerApiService,
    private store: Store<NoteSettingsState>,
    public signalrService: NotesSignalService
  ) {
  }

  ngOnInit() {
      this.store.select(SettingsSelectors.getNoteNameSelector)
    .subscribe(
      noteName => {
        this.notesPackName = noteName!
      }
    )

    this.signalrService.subscribeOnUpdatedNotesState();
    this.signalrService.subscribeOnUpdatedExpirationDate();
    this.signalrService.isEditingListener();
  }

  updateNotesPack() {
    const updateNoteRequestDto = <UpdateNoteRequestDto> {
      noteName: this.notesPackName,
      expirationMinutesRange: this.expiriationTimeSpan.value,
      notesPack: this.toDoList.value
    }
    this.organizerApiService.updateNotePack(updateNoteRequestDto).subscribe(()=>{
      this.toDoService.addItem(new TodoItem(this.noteInput));
      this.noteInput = undefined;
    });
  }

  addNewItem() {
    if (this.noteInput && this.noteInput !== ""){
      this.toDoService.addItem(new TodoItem(this.noteInput));
      this.updateSource();
      this.noteInput = "";
    }
  }

  deleteNoteItem(noteToRemove: TodoItem) {
    this.toDoService.removeNoteItem(noteToRemove);
    this.updateSource();
  }

  editItem(editedItem: TodoItem) {
    this.signalrService.isEditingMarker(this.notesPackName, true);
    this.todoItem = editedItem;
    this.noteInput = editedItem.noteText;
    this.editValue = true;
  }

  markItemAsDone(markedItem: TodoItem) {
    this.toDoService.completeItem(markedItem, true);
    this.updateSource();
  }

  markItemAsNotDone(markedItem: TodoItem) {
    this.toDoService.completeItem(markedItem, false);
    this.updateSource();
  }

  saveChangedItem() {
    if (this.noteInput && this.noteInput !== "") {
      this.toDoService.editItem(this.todoItem, this.noteInput);
      this.editValue = false;
      this.noteInput = undefined;
      this.todoItem = new TodoItem('');
      this.updateSource();
      this.signalrService.isEditingMarker(this.notesPackName, false);
    }
  }

  countLength(): string {
    return this.noteInput ? this.noteInput?.length.toString() : "0";
  }

  isEdited() : boolean {
    return this.signalrService.isBeingEdited == true;
  }

  private updateSource() {
    const updateNoteRequestDto = <UpdateNoteRequestDto>{
      noteName: this.notesPackName,
      expirationMinutesRange: this.expiriationTimeSpan.value,
      notesPack: this.toDoList.value,
      connectionId: this.signalrService.connection.connectionId
    };
    this.organizerApiService.updateNotePack(updateNoteRequestDto).subscribe(() => {
      this.noteInput = undefined;
    });
  }
}
