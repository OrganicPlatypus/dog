import { CreateNoteDto, NoteSettingsDto, NotesSessionDto, UpdateNoteExpiriationTimeDto, UpdateNoteRequestDto } from './service-proxy/service-proxy';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class OrganizerApiService {
  private baseUrl: string;

  constructor(
    private http: HttpClient
    ) {
    this.baseUrl = "https://localhost:44365";
   }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  isPasswordRequired(noteName: string): Observable<boolean>{
    return this.http.get<boolean>(`${this.baseUrl}/api/isPasswordRequired/${noteName}`);
  }

  joinTheNote(key: string): Observable<NotesSessionDto> {
    return this.http.get<NotesSessionDto>(`${this.baseUrl}/api/joinSession/${key}`);
  }

  updateNotePack(noteDto: UpdateNoteRequestDto): Observable<void> {
    const options: Object = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    return this.http.put<void>(`${this.baseUrl}/api/Organizer/UpdateNotesPack`, noteDto, options);
  }

  updateNoteExpiriationTime(updateExpiriationTimeDto: UpdateNoteExpiriationTimeDto): Observable<void> {
    const options: Object = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    return this.http.put<void>(`${this.baseUrl}/api/Organizer/UpdateNoteExpiriationTime`, updateExpiriationTimeDto, options);
  }

  createNote(noteDto: CreateNoteDto): Observable<NoteSettingsDto>  {
    const options: Object = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8',
        'Accept': 'text/plain'
      })
    }
    return this.http.post<NoteSettingsDto>(`${this.baseUrl}/api/Organizer/CreateNotesPack`, noteDto, options);
  }
}
