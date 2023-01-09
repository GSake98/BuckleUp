import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      // catchError is from rxjs
      catchError((error: HttpErrorResponse) => {
        if (error) {
          switch (error.status) {
            case 400:
              // .error is the object of error and then .errors is what our API sends back to us
              if (error.error.errors) {
                const modelStateErrors = []; // Format into array so its easier to be handled
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    // Builds an array of errors for validation errors and store it in the const
                    modelStateErrors.push(error.error.errors[key])
                  }
                }
                throw modelStateErrors.flat(); // flats 2 arrays into a single array
              } else {
                // Code given by Udemy comments and it works for toastr messages
                this.toastr.error(error.statusText === "OK" ? "Bad Request" : error.statusText, error.status.toString());
                break;
              }
            case 401:
              // Code given by Udemy comments and it works for toastr messages
              this.toastr.error(error.statusText === "OK" ? "Unauthorized" : error.statusText, error.status.toString());
              break;
            case 404:
              this.router.navigateByUrl('/not-found'); // Redirects to not found page
              break;
            case 500:
              // { state: { error: error.error } } contains our exception 
              const navigationExtras: NavigationExtras = { state: { error: error.error } }
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              this.toastr.error('Something unexpected went wrong.');
              console.log(error);
              break;
          }
        }
        throw error; // Needed to close for error: HttpErrorResponse
      })
    )
  }
}
