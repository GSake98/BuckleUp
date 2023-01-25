import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Member } from 'src/app/models/member';
import { MembersService } from 'src/app/services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member: Member | undefined;
  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[] = [];

  // Activated route helps us with routing from a component to somewhere else
  // accessing its parameters and in our case go to 'members/:username'
  constructor(private memberService: MembersService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();

    this.galleryOptions = [
      {
        width: '450px',
        height: '450px',
        imagePercent: 85,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }]
  }

  getImages() {
    // If we don't have a member return [] since it is an empty array
    if (!this.member) {
      return [];
    }
    else {
      const imageUrls = [];
      for (const photo of this.member.photos) {
        imageUrls.push({
          // We must specify all s/m/b properties since its a made gallery
          small: photo.url,
          medium: photo.url,
          big: photo.url,
        });
      }
      return imageUrls;
    }
  }

  loadMember() {
    const username = this.route.snapshot.paramMap.get('username');
    if (!username) {
      return; // If we don't have a username return
    }
    else {
      // If we do get the member's username
      this.memberService.getMember(username).subscribe({
        next: member => {
          this.member = member;
          // We make sure we get the images when the loadMember is called for the timing to be right
          this.galleryImages = this.getImages();
        }
      })
    }
  }
}
