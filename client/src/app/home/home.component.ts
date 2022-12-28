import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any; // any turns type safety off

  constructor(private http : HttpClient) { }

  ngOnInit(): void {
    this.getUsers(); // So home component is responsible to get users from our list of API
  }

  registerToggle(){
    this.registerMode = !this.registerMode; // Sets it to the opposite of that value
  }

  getUsers(){
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

  cancelRegisterMode(event: boolean){ // This will take the value of false from our child component
    this.registerMode = event; // It will equal false or true depending what we pass on it
  }
}
