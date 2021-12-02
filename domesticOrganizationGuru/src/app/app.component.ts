import { NotesSignalService } from './services/signalR/notes.signal.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'domesticOrganizationGuru';

  constructor(
    private signalR: NotesSignalService
  ) {
  }

  ngOnInit(){
    this.signalR.startConnection();
  }
}
