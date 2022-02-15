import { Injectable, InjectionToken } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
  providedIn: 'root'
})
export class BaseApiService{

  constructor(public toastr: ToastrService) {
  }
}
