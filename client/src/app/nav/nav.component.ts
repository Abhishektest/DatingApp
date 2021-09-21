import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model:any = {}
  //loggedIn: boolean | undefined;
 // currentUser$ : Observable<User> | undefined;  // we can use *ngIf="LoggedIn" to "currentUser$ | async"
  constructor(public accountservice : AccountService) { }

  ngOnInit(): void {
    //this.getCurrentUser();
   // this.currentUser$= this.accountservice.currentUser$; // Alternativle we can call the serivice in html itsef. by making service public
  }
  login(){
    this.accountservice.login(this.model).subscribe(response => {
      console.log(response);
      //this.loggedIn = true;  as we using async pipe
    },error => {
      console.log(error);
     })
  }
  logout(){
    this.accountservice.logout();
   // this.loggedIn=false;
  }
 /* getCurrentUser()   // we could use asyn pipe to set structural directive decision by consuming  accountservice.currentUsers 
  {
    this.accountservice.currentUser$.subscribe(user=>{
      this.loggedIn = !!user; // turn object into a boolean, if user is null then it is false and otherwise
    },error =>{
      console.log(error);
    })
  } */
}
