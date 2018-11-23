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

  //Not found messages
  messageTitle = "No Products Found in Cart";
  messageDescription = "Please add items to cart";

  constructor(
    private cartService: CartService
  ) {
    this.cart = new FirebaseCartModel;
  }

  ngOnInit(): void {
  
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

  getCart() {
    this.cartService.getCart(this.cart.id)
      .subscribe((data: FirebaseCartModel) => this.cart = {
        id: data['id'],
        products: data['products'],
        savedForLater: data['savedForLater'],
        totalPrice: data['totalPrice']
      });;

    if (this.cart.products == null) {
      this.messageTitle;
      this.messageDescription;
    }
    
  }

}
