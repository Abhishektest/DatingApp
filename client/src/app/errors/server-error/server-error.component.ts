import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css']
})
export class ServerErrorComponent implements OnInit {
error:any;
constructor(private router: Router) {
  const navigation = this.router.getCurrentNavigation();
  this.error = navigation?.extras?.state?.error;
 }
  /*constructor(private router:Router) {  // we can only access the router state in constructor.
    const navigation = this.router.getCurrentNavigation();
    this.error = navigation?.extras?.state?.console.error;
     // (?. it is optional chaining operator)if user refreshes the page so we lose the info hence to be safe we will use optional chaining operator or ?.
   }*/

  ngOnInit(): void {
  }

}
