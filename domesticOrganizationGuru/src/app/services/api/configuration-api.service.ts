import { BaseApiService } from './baseApi/baseApi.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationApiService extends BaseApiService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    super();
    //TODO: Przenieść do BaseApiService
    this.baseUrl = "https://localhost:44365";
   }

  //TODO: Przenieść do BaseApiService
  //  constructor(private http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
  //   this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
  //  }

   landingHomeConfiguration(): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}/api/Configuration/LandingConfiguration`)
    .pipe(
      catchError(this.handleError)
    )
  }
}
