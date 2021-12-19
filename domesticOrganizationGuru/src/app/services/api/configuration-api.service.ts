import { HttpClient } from '@angular/common/http';
import { Injectable, InjectionToken } from '@angular/core';
import { Observable } from 'rxjs';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
  providedIn: 'root'
})
export class ConfigurationApiService {

  public baseUrl: string

  constructor(
    private http: HttpClient
    ) {
    this.baseUrl = 'https://localhost:44365';
   }

  landingHomeConfiguration(): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}/api/Configuration/LandingConfiguration`);
  }
}
