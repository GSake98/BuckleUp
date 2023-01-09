import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/';
  validationErrors: string[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  get404Error() {
    // Get the 404 from our ErrorController's methods
    this.http.get(this.baseUrl + 'error/not-found').subscribe({
      next: response => console.log(response), // We will never have a successful response
      error: error => console.log(error)
    })
  }

  get400Error() {
    // Get the 400 from our ErrorController's methods
    this.http.get(this.baseUrl + 'error/bad-request').subscribe({
      next: response => console.log(response), // We will never have a successful response
      error: error => console.log(error)
    })
  }

  get500Error() {
    // Get the 500 from our ErrorController's methods
    this.http.get(this.baseUrl + 'error/server-error').subscribe({
      next: response => console.log(response), // We will never have a successful response
      error: error => console.log(error)
    })
  }

  get401Error() {
    // Get the 401 from our ErrorController's methods
    this.http.get(this.baseUrl + 'error/auth').subscribe({
      next: response => console.log(response), // We will never have a successful response
      error: error => console.log(error)
    })
  }

  get400ValidationError() {
    // Post the 400 Validation from our AccountController (account/register this one for now)
    this.http.post(this.baseUrl + 'account/register', {}).subscribe({
      next: response => console.log(response), // We will never have a successful response
      error: error => {
        console.log(error);
        this.validationErrors = error; // The property will be set to that array now}
      }
    })
  }
}
