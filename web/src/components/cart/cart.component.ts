import { Component, OnInit } from '@angular/core';
//import { UserService } from '../core/user.service';
import { AuthService } from '../core/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
//import { FirebaseUserModel } from '../core/user.model';
import { Book } from '../book.details/book.model';
import { CartService } from '../core/cart.service';
import { FirebaseCartModel } from '../core/cart.model';
import { UserComponent } from '../user/user.component';

@Component({
  selector: 'page-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cart: FirebaseCartModel;
  showDataNotFound: true;

  //Not found messages
  messageTitle = "No Products Found in Cart";
  messageDescription = "Please add items to cart";

  constructor(
    private cartService: CartService,
    private userComponent: UserComponent
  ) {
  }

  ngOnInit(): void {
    this.getCart();
  }

  addProduct(product: Book) {
    this.cartService.addToCart(this.cart.id, product);
  }

  removeProduct(product: Book) {
    this.cartService.removeFromCart(this.cart.id, product);

    this.getCart();
  }

  getCart() {
    this.cartService.getCart(this.cart.id)
      .subscribe((data: FirebaseCartModel) => this.cart = {
        id: data['id'],
        products: data['products']
      });;

    if (this.cart.products == null) {
      this.messageTitle;
      this.messageDescription;
    }
  }

}
