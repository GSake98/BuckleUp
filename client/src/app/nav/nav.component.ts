import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  loggedIn = false; // Initial value so since its a component its always false on refresh

  constructor(private accountService : AccountService) { }

  ngOnInit(): void {
  }

  // If incorrect password or username show error else show in console their token & login
  login(){
    this.accountService.login(this.model).subscribe({
      next: response => { // {} for multiple statements
        console.log(response);
        this.loggedIn = true;
      },
      error: error => console.log(error)
    })
  }

  logout(){
    this.loggedIn = false;
  }
}
