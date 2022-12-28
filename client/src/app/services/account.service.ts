import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../models/user';
/* Service Code Knowledge
// This AccountService will be responsible for making HTTP requests from our client to our server
// Using server: only destroys after user exits webpage
// Using component: gets destroyed every time user chooses another component
// Services are injectable like we see in Line 10(!)
// Services are singletons which mean they are instantiated on start and destroyed on close
/**/
@Injectable({
  providedIn: 'root'
})

export class AccountService {
  baseUrl = 'https://localhost:5001/api/'; // Strings always have warnings in case of typos
  // Initializes value and we use ' | null' to defy the rule that doesn't let us pass null in ()
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable(); // $ means observable and store one as

  constructor(private http: HttpClient) { }

  login(model: any) {
    // We know when the user logs in we get their token and their username
    // We specify the type of thing inside '<>' we want to return and we can use our user.ts now
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe( // Concat value url + string url
      // This executes as the observable comes back from API and before the component subscription
      map((response: User) => {
        const user = response; // Storing response in a variable called user
        if (user) {
          // Specify the key of the item as user and in JSON.stringify we pass what
          // we want to store inside local storage and make it a string from JSON
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user); // We set what it's next value is after login
        }
      })
    )
  }

  // Effectively register method looks like login because we just consider them as logged in now
  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      // If we want to return a response when we use map() we have to do it inside it
      map(user => { // Now it knows our value is user
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user); // Convenience method to pass our user in
  }

  logout() {
    localStorage.removeItem('user'); // We remove the item when they logout
    this.currentUserSource.next(null); // We set it to null after logout
  }
}
