import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { delay, finalize, Observable } from 'rxjs';
import { BusyService } from '../services/busy.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService: BusyService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.busyService.busy(); // Increments busy request count

    return next.handle(request).pipe(
      delay(800), // rjxs operator called delay so we can pretent that our app takes a second
      // What to do after it hass completed (Turn off the spinner loader)
      finalize(() => {
        this.busyService.idle();
      })
    );
  }
}
