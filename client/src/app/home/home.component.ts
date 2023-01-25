import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any; // any turns type safety off

  constructor() { }

  ngOnInit(): void {

  }

  registerToggle() {
    this.registerMode = !this.registerMode; // Sets it to the opposite of that value
  }

  cancelRegisterMode(event: boolean) { // This will take the value of false from our child component
    this.registerMode = event; // It will equal false or true depending what we pass on it
  }
}
