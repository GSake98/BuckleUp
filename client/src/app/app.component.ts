import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

// OnInit happens after our component has been constructed
export class AppComponent implements OnInit{
  title = 'BuckleUp';
  users: any; // Any turns type safety off

  // This defines a constructor in typescript-javascript
  // 1=name 2=access 3=var name 4=what to get
  constructor(private http: HttpClient) 
  {

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
    this.http.get('https://localhost:5001/api/users').subscribe({
      // We want to get users inside our new variable and assign it
      next: response => this.users = response, 
      // We sent out our error in the console.log
      error: error => console.log(error),
      // http requests always complete and we always use () as lambda expression
      // We'll get the string we passed if our request has been executed correctly
      complete: () => console.log('Request has completed')
    });
  }
}
