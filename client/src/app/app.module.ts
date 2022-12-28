import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http' // Imported by hand

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule, // Must be imported by hand since Angular doesn't specify it
    FormsModule, // Comes with Angular 4
    BrowserAnimationsModule, // Required for dropdown angular menu
    BsDropdownModule.forRoot() // Required for dropdown angular menu
  ],
  providers: [], // (equivalent to @Injectable provided in) example HttpClient etc
  bootstrap: [AppComponent]
})
export class AppModule { }
