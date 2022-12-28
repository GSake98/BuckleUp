import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
  }

  // In case of http requests we don't need to unsubscribe but in our own observables
  // we ought to unsubscribe from them to not cause a memory leak
  // If incorrect password or username show error else show in console their token & login
  login() {
    this.accountService.login(this.model).subscribe({
      next: response => { // {} for multiple statements
        console.log(response);
      },
      error: error => console.log(error)
    })
  }

  logout() {
    this.accountService.logout(); // remove item from storage as well
  }
}
