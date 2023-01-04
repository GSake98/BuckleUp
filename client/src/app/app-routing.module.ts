// this app-routing.module.ts is where we define what routes are available in our angular app
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';

// In this made array of routes we specify root paths for the components we created
const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard], // This is how we use guards, any children inside this path will have it
    // Which child routes we want to protect, it can also be an array []
    children: [
      { path: 'members', component: MemberListComponent}, 
      { path: 'members/:id', component: MemberDetailComponent }, // Member details for the ids
      { path: 'lists', component: ListsComponent },
      { path: 'messages', component: MessagesComponent },
    ]
  },
  { path: '**', component: HomeComponent, pathMatch: 'full' } // Do this when redirecting empty path routes
  // 2 Asterisks are the wildcards, they redirect them in case they press anything else
  // other than our specified components to where we desire, in this case 'Home'
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
