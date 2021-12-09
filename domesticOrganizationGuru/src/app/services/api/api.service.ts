import { BaseApiService } from './baseApi/baseApi.service';
import { CreateNotesPackDto, NotesSessionDto, UpdateNoteRequestDto } from '../service-proxy/service-proxy';
import { Injectable, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class OrganizerApiService extends BaseApiService {

  private baseUrl: string;

  constructor(private http: HttpClient) {
    super();
    this.baseUrl = "https://localhost:44365";
   }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  joinTheNote(key: string): Observable<NotesSessionDto> {
    return this.http.get<NotesSessionDto>(`${this.baseUrl}/join/${key}`)
    .pipe(
      catchError(this.handleError)
    )
  }


  saveNote(noteDto: UpdateNoteRequestDto): Observable<UpdateNoteRequestDto> {
    return this.http.post<UpdateNoteRequestDto>(`${this.baseUrl}/api/Organizer/SaveNote`, JSON.stringify(noteDto), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
  )}


  updateNotePack(noteDto: UpdateNoteRequestDto): Observable<void> {
    const options: Object = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    return this.http.put<void>(`${this.baseUrl}/api/Organizer/UpdateNotesPack`, noteDto, options).pipe(
      catchError(this.handleError)
    );
  }

  createNote(noteDto: CreateNotesPackDto): Observable<string>  {
    const options: Object = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8',
        'Accept': 'text/plain'
      }),
      responseType: 'text' as const
    }

    return this.http.post<string>(`${this.baseUrl}/api/Organizer/CreateNotesPack`, noteDto, options)
    .pipe(
        catchError(this.handleError)
    );
  }
}
