import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // We use Input to have parent-to-child communication
  // We use Output to have child-to-parent communication
  @Output() cancelRegister = new EventEmitter();
  model: any = {} // Initializes value to empty object (this is needed)

  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  register() {
    this.accountService.register(this.model).subscribe({
      next: () => { // If we don't use data we can just use '()'
        this.cancel(); // Call cancel to close register form
      },
      error: error => {
        this.toastr.error(error.error), // This is how we show the error to the user
          console.log(error);
      }
    });
  }

  cancel() {
    this.cancelRegister.emit(false); // We want to turn off the registerMode in homeComponent
  }
}
