import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];

  constructor(private http: HttpClient) { }

  getMembers() {
    if (this.members.length > 0) {
      return of(this.members); // Returns an observable 'of' is an rxjs operator to complete this
    }
    else {
      return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
        map(members => {
          this.members = members;
          // We also need to return our list of members because the component is using them
          return members;
        })
      )
    }
  }

  getMember(username: string) {
    const member = this.members.find(x => x.userName === username);
    if (member) {
      return of(member);
    }
    else {
      return this.http.get<Member>(this.baseUrl + 'users/' + username)
    }
  }

  updateMember(member: Member) {
    // We pass our member parameter to the API (route: https...users)
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => { 
        // Tells us the index of the element of member in this members array
        const index = this.members.indexOf(member);
        // Spread operator works like this '{...}
        // Takes all the elements in our array and spreads them so we can have id,name,age etc
        this.members[index] = {...this.members[index],...member}; // And spread it to member
      })
    )
  }
}
