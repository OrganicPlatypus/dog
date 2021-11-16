import { CreateNotesPackDto, NoteDto, NotesSessionDto, UpdateNoteRequestDto } from '../service-proxy/service-proxy';
import { Inject, Injectable, InjectionToken, Optional } from '@angular/core';
import { HttpClient, HttpContext, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { retry, catchError, tap, map } from 'rxjs/operators';
import { connectableObservableDescriptor } from 'rxjs/internal/observable/ConnectableObservable';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
  providedIn: 'root'
})

export class OrganizerApiService {

  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = "https://localhost:44365";
   }

  //  constructor(private http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
  //   this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
  //  }


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

  handleError(error: any) {
     let errorMessage = '';
     if(error.error instanceof ErrorEvent) {
       errorMessage = error.error.message;
     } else {
       errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
     }
     window.alert(errorMessage);
     return throwError(errorMessage);
  }
}
