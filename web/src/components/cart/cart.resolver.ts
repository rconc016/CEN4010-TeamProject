import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from "@angular/router";
import { CartService } from '../core/cart.service';
import { UserService } from '../core/user.service';
import { FirebaseCartModel } from '../core/cart.model';
import { FirebaseUserModel } from '../core/user.model';
import { Observable } from 'rxjs';

@Injectable()
export class CartResolver implements Resolve<FirebaseCartModel> {

  constructor(public cartService: CartService, public userService:UserService, private router: Router) { }

  resolve(route: ActivatedRouteSnapshot) : Promise<FirebaseCartModel> {

    let user = new FirebaseUserModel();

    return new Promise((resolve, reject) => {
      this.userService.getCurrentUser()
      .then(res => {
        resolve(this.cartService.getCart(user.id).toPromise())
      }, err => {
        this.router.navigate(['/login']);
        return reject(err);
      })
    })
  }

}
