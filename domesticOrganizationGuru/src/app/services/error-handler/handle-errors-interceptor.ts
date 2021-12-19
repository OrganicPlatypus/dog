import { HandleErrorService } from './handle-error.service';
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";

@Injectable()
export class HandleErrorsInterceptor implements HttpInterceptor{

  constructor(private error: HandleErrorService){}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return new Observable((observer) => {
      next.handle(req).subscribe(
        (res: any) => {
        if(res instanceof HttpResponse) {
          observer.next(res);
          }
        },
        (err: HttpErrorResponse) => {
          this.error.handleError(err);
        }
      )
    });
  }
}
