import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [],
  imports: [
    CommonModule, // Has to be provided in any module we create
    BsDropdownModule.forRoot(), // Required for dropdown angular menu
    // We can even apply the position of the toastr messages
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    })
  ],
  exports: [ 
    // If we want to import modules we also have to export. No need to specify forRoot.
    // Only the components in 'declarations: []' will have access to our modules if we don't export.
    BsDropdownModule,
    ToastrModule
  ]
})
export class SharedModule { }
