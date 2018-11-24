import { Component, OnInit } from '@angular/core';
import { AuthService } from '../core/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Book } from '../book.details/book.model';
import { CartService } from '../core/cart.service';
import { FirebaseCartModel } from '../core/cart.model';

@Component({
  selector: 'page-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  cart: FirebaseCartModel;
  showDataNotFound: true;

  constructor(
    private cartService: CartService,
    private route: ActivatedRoute,
  ) {
    this.cart = new FirebaseCartModel();
  }

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.cart = data['cartResolver'];
    });
  }

  addProduct(product: Book, userId:string) {
    this.cartService.addToCart(this.cart, product);
    this.getCart();
    
  }

  removeProduct(product: Book) {
    this.cartService.removeFromCart(this.cart, product);
    this.getCart();
  }

  saveProduct(product: Book) {
    this.cartService.saveForLater(this.cart, product);
    this.getCart();
  }

  removeSaved(product: Book) {
    this.cartService.removeSavedFromCart(this.cart, product);
    this.getCart();
  }

  purchase() {
    this.cartService.purchase(this.cart);
    this.getCart();
  }

  getCart() {
    this.cartService.getCart(this.cart.id)
      .subscribe((data: FirebaseCartModel) => this.cart = {
        id: data['id'],
        products: data['products'],
        savedForLater: data['savedForLater'],
        purchased: data['purchased'],
        totalPrice: data['totalPrice']
      });
  }

}
