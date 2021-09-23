import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Datting App';
  users: any;
  constructor(private accountService: AccountService){}
  ngOnInit()
  {
    //this.getUsers()
    this.setCurrentUser();
  }
  setCurrentUser() {
    
    //const user: User =  JSON.parse(localStorage.getItem('user') ?? '');
    const user: User =   JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }
  /*getUsers(){
    this.http.get('https://localhost:44361/api/Users').subscribe(response=>{
      this.users= response;
    },error=> {
      console.log(error);
    })
  } */ // we moved this code to home component
}
