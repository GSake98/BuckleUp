import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, map } from 'rxjs';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root' // Effectively tells us that this will be instantiated on app start
})
export class AuthGuard implements CanActivate {
  // We can have either of 'Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree '
  constructor(private accountService : AccountService, private toastr : ToastrService,private router:Router) {}

  // The auth guard automatically susbcribes or unsubscribes from that particular observable
  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map(user => {
        // If we have a user then the auth guard can access the route links that we clicked else no
        if(user)
        {
          return true;
        }
        else
        {
          this.router.navigateByUrl('/'); // Redirects to home page so its not blank
          this.toastr.error('Requires to log in.');
          return false;
        }
      })
    )
  }
}
