import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  //users:any;
  constructor() { }

  ngOnInit(): void {
   // this.getUsers()
  }
  registerToggle(){
    this.registerMode= !this.registerMode;
  }
 /* getUsers(){  // we are not passing now value from home to register P to child
    this.http.get('https://localhost:44361/api/Users').subscribe(
      users=> this.users=users
    );} */
    cancelRegisterMode(event: boolean) {
      this.registerMode = event;
    }
}
