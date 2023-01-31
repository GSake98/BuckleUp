import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<MemberEditComponent> {
  canDeactivate(component: MemberEditComponent): boolean {
    // Since we get access to our components here we have access to their properties too
    if(component.editForm?.dirty){
      // This is a Javascript confirm -- To be changed later on --
      return confirm('Are you sure you want to continue without saving your profile changes?');
    }
    return true;
  }

}
