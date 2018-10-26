import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { CartService } from '../core/cart.service';
import { FirebaseCartModel } from "../core/cart.model";

@Injectable({
  providedIn: 'root'
})
export class CartResolver implements Resolve<FirebaseCartModel> {
  constructor(private cartService: CartService) {
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<FirebaseCartModel> {

    let cart = new FirebaseCartModel();

    return this.cartService.getCart(cart.id);
  }

}
