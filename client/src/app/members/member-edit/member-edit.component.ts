import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Member } from 'src/app/models/member';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';
import { MembersService } from 'src/app/services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  // This is how we connect our angular form to our template (.html is component, .ts is child)
  // @ViewChild is an Angular feature, we use it because the template is a child of our component
  @ViewChild('editForm') editForm: NgForm | undefined;
  // To get prompts of the browser we use @HostListener and the following is mandatory
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }
  member: Member | undefined;
  user: User | null = null; // To get around issues we assign it to null since it can be null

  constructor(private accountService: AccountService,
    private memberService: MembersService, private toastr: ToastrService) {
    // As soon as we have the user our request is completed and we don't need to unsubscribe
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      // user is initially null until we do this and we populate the user
      next: user => this.user = user
    })
  }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    if (!this.user) {
      return;
    }
    else {
      this.memberService.getMember(this.user.username).subscribe({
        next: member => this.member = member
      })
    }
  }

  updateMember() {
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next: empty => {
        this.toastr.success('Profile Updated.');
        this.editForm?.reset(this.member); // We can reset it to something we want
      }
    })

  }
}
