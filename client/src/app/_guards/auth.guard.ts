import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
   //if all guard return true then navigation continue else stop

   constructor(private accountService:AccountService,private toastr:ToastrService){}

  canActivate(): Observable<boolean> {

     return this.accountService.currentUser$.pipe(
      map((user)=>{
        if(!user){
          this.toastr.error('You shall not pass!')
        }
        return !!user;       
      })
    )
  }

  //authguard is automatically going to subscribe to replaysubsject observable.Hence directly we use pipe n no subscribe
  
}