import { setExpirationDateAction } from 'src/app/state/states/settings/settings.actions';
import { NotesSignalService } from 'src/app/services/signalR/notes.signal.service';
import { OrganizerApiService } from 'src/app/services/api/api.service';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';

import * as SettingsSelectors from '../../state/states/settings/settings.selector'
import { CreateNoteDto } from 'src/app/services/api/service-proxy/service-proxy';

@Component({
  selector: 'password-creator',
  templateUrl: './password-creator.component.html',
  styleUrls: ['./password-creator.component.scss']
})
export class PasswordCreatorComponent implements OnInit {
  @Input()
  isOpen: boolean = false;

  isPasswordRequested:boolean = false;

  newPassword = new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(30)]);

  @Output()
  readonly isOpenChange = new EventEmitter<boolean>();

  constructor(
    private store: Store<NoteSettingsState>,
    public organizerApiService: OrganizerApiService,
    public signalrService: NotesSignalService,
    public router: Router
    ) {}

  ngOnInit() {
  }

  goForward(){
    const newPassword = this.newPassword.value;
    this.CreateNotes(newPassword);
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
          this.store.dispatch(setExpirationDateAction({ expirationDate : new Date( expirationDateDto.expirationDate! )}));
          this.signalrService.joinGroup( noteInitialSettingsDto.noteName! );
          this.router.navigate(['/to-do']);
          });
  }

  buttonEnabled(): boolean{
    return !this.isPasswordRequested || this.isPasswordRequested && this.newPassword.valid;
  }

  resetPassword(): void {
    this.newPassword.setValue("");
  }
}
