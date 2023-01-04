import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  // In case of http requests we don't need to unsubscribe but in our own observables
  // we ought to unsubscribe from them to not cause a memory leak
  // If incorrect password or username show error else show in console their token & login
  login() {
    this.accountService.login(this.model).subscribe({
      // {} only for multiple statements
      next: () => this.router.navigateByUrl('/members'), // Nagivates to members page after logging in 
      error: error => this.toastr.error(error.error) // This is how we show the error to the user
    })
  }

  logout() {
    this.accountService.logout(); // remove item from storage as well
    this.router.navigateByUrl('/'); // Nagivates to home page after logging out 
  }
}
