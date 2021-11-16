import { UpdateNoteRequestDto, NoteDto, OrganizerServiceProxy, CreateNotesPackDto } from './../../services/service-proxy/service-proxy';
import { OrganizerApiService } from 'src/app/services/api/api.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { ConfigurationApiService } from 'src/app/services/api/configuration-api.service';


@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.scss']
})
export class StartComponent implements OnInit {
  initialExpirationSpan = 0;

  noteName = new FormControl('');
  joinSessionByName = new FormControl('');

  constructor(
    public router: Router,
    public organizerApiService: OrganizerApiService,
    public configurationApiService: ConfigurationApiService
  ) {}

  ngOnInit() {
    this.configurationApiService.landingHomeConfiguration().subscribe(minutes => {
      this.initialExpirationSpan = minutes;
    })
  }

  public createNotePack() {
    const notesPack: CreateNotesPackDto = <CreateNotesPackDto> {
      expirationMinutesRange: this.initialExpirationSpan,
      noteName: this.noteName.value
    }

    this.organizerApiService.createNote(notesPack).subscribe((name) => {
      this.router.navigate(['/to-do']);
    });
  }

  public joinSession(){
    this.organizerApiService.joinTheNote(this.joinSessionByName.value).subscribe((notesPack) => {
    this.router.navigate(['/to-do']);
  });
  }
}
