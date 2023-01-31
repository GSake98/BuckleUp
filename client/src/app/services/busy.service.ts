import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  // As a request takes place we will increase it and if it is greater than 0 we enable the spinner
  busyRequestCounter = 0;

  constructor(private spinnerService: NgxSpinnerService) { }

  // Enables Loading feature
  busy() {
    this.busyRequestCounter++;
    this.spinnerService.show(undefined, {
      type: 'ball-spin-clockwise-fade',
      bdColor: 'rgba(255,255,255,0)',
      color: 'rgba(0,135,238,0.9)',
      size: 'medium'
    });
  }

  // Disables Loading feature
  idle() {
    this.busyRequestCounter--;
    if (this.busyRequestCounter <= 0) {
      this.busyRequestCounter = 0; // Things may go wrong we should make sure it goes to 0 not less
      this.spinnerService.hide();
    }
  }
}
