import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { Member } from 'src/app/models/member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  // Initial value is undefined so we get around the Typescript error as well with it
  @Input() member: Member | undefined;

  constructor() { }

  ngOnInit(): void {
  }

}
