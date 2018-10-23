import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { CartService } from '../core/cart.service';
import { FirebaseCartModel } from "../core/cart.model";

@Injectable({
  providedIn: 'root'
})
export class CartResolver {
  constructor(private cartService: CartService) {
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<FirebaseCartModel> {

    let cart = new FirebaseCartModel();

    return this.cartService.getCart(route.params['id']);
  }

  /*resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<BookInterface[]> {
    let sortCommand = new SortCommand();
    sortCommand.key = this.sortKey;
    sortCommand.sortBy = SortDirection.Asc;

    return this.bookService.findAll(sortCommand, null, null);
  }*/
}
