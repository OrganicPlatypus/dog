import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HandleErrorService {

constructor(private toaster: ToastrService) { }

  public handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    console.log(error)
    if(error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = error.error;
    }
    this.toaster.error(errorMessage);
  }
}
