import { BaseApiService } from './baseApi/baseApi.service';
import { HttpClient } from '@angular/common/http';
import { Injectable, InjectionToken, Optional } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
  providedIn: 'root'
})
export class ConfigurationApiService extends BaseApiService {

  public baseUrl: string

  constructor(private http: HttpClient) {
    super();

    this.baseUrl = 'https://localhost:44365';
   }

   landingHomeConfiguration(): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}/api/Configuration/LandingConfiguration`)
    .pipe(
      catchError(this.handleError)
    )
  }
}
