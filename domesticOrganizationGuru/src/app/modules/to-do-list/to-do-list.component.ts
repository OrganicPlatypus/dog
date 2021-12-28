import { NotesSignalService } from './../../services/signalR/notes.signal.service';
import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { OrganizerApiService } from 'src/app/services/api/api.service';
import { UpdateNoteRequestDto } from 'src/app/services/api/service-proxy/service-proxy';
import { TodoItem } from './models/to-do';
import { ToDoService } from './services/to-do-service.service';
import { Store } from '@ngrx/store';
import { BehaviorSubject, zip } from 'rxjs';
import * as SettingsSelectors from '../../state/states/settings/settings.selector'
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';

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

  notesPackName: string = '';
  notesLifespan: number = 0;


  newNotesState!: UpdateNoteRequestDto;

  constructor(
    private toDoService: ToDoService,
    private organizerApiService: OrganizerApiService,
    private store: Store<NoteSettingsState>,
    public signalrService: NotesSignalService
  ) {
  }

  ngOnInit() {
    //TODO: dodać guarda z przekierowaniem na landing page jeżeli w storze nie ma dodanej nazwy notatek.

    zip(
      this.store.select(SettingsSelectors.getNoteNameSelector),
      this.store.select(SettingsSelectors.getMinutesTillExpireSelector)
    )
    .subscribe(
      noteSettings => {
        this.notesPackName = noteSettings[0]!
        this.notesLifespan = noteSettings[1]!
      }
    )

    this.signalrService.subscribeOnCurrentlyUpdateNote();
    this.signalrService.isEditingListener();
  }

  updateNotesPack() {
    const updateNoteRequestDto = <UpdateNoteRequestDto> {
      noteName: this.notesPackName,
      expirationMinutesRange: this.notesLifespan,

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
      expirationMinutesRange: this.notesLifespan,
      notesPack: this.toDoList.value,
      connectionId: this.signalrService.connection.connectionId
    };
    this.organizerApiService.updateNotePack(updateNoteRequestDto).subscribe(() => {
      this.noteInput = undefined;
    });
  }
}
