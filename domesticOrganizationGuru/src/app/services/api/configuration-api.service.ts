import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationApiService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = "https://localhost:44365";
   }

  //  constructor(private http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
  //   this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
  //  }

   landingHomeConfiguration(): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}/api/Configuration/LandingConfiguration`)
    .pipe(
      catchError(this.handleError)
    )
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
