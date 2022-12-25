import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
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

  constructor(private http: HttpClient) { }

  login(model: any)
  {
    return this.http.post(this.baseUrl + 'account/login', model) // Concat value url + string url
  }
}
