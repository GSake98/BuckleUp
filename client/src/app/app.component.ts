import { Component, OnInit } from '@angular/core';
import { User } from './models/user';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

// OnInit happens after our component has been constructed
export class AppComponent implements OnInit {
  title = 'BuckleUp';

  // This defines a constructor in typescript-javascript
  // 1=name 2=access 3=var name 4=what to get
  constructor(private accountService: AccountService) {

  }

  // Gets our constructor's parameter and returns an observable (ex. website)
  // Need to subscribe and then describe what we want to do with the observable

  // 26-35L - Inspector (F12) Browser: ngOnInit() what it does
  /*
  If we use the Inspector (F12) in our localhost server we run the app we can see
  in Network: our users table which then allow sub-tabs of Header, Preview etc to
  check in order to see if everything works as expected. If the request is not blocked
  we will see in Header that our request Url is the same as in our http.get method and
  that allow origins have allowed the localhost the angular server is running on.
  We need to make sure in our program.cs that we have specified our builder app is
  allowing any headers and any methods within our origin of the localhost server
  angular is running which is 4200.
  /**/
  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (userString) {
      // Since we stored a string value it doesn't throw errors because it knows that IF we have
      // a value it can parse it now or else it would complain that it could parse a null value.
      const user: User = JSON.parse(userString);
      this.accountService.setCurrentUser(user); // Makes sure we persist the login status
    }
    else {
      return;
    }
  }
}
