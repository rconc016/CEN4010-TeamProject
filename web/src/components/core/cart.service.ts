import { Injectable } from "@angular/core";
import {
  AngularFireDatabase,
  AngularFireList,
  AngularFireObject
} from "angularfire2/database";
import { Book } from "../book.details/book.model";
import { AuthService } from "./auth.service";
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { AngularFirestore } from '@angular/fire/firestore';
import { AngularFireAuth } from '@angular/fire/auth';
import * as firebase from 'firebase/app';
import { environment } from '../../environments/environment';
import { FirebaseCartModel } from '../core/cart.model';
import { Observable } from 'rxjs';
import { UserComponent } from '../user/user.component';
//import { CartComponent } from '../cart/cart.component';


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
};

@Injectable()
export class CartService {
  url: string = `${environment.apiUrl}/cart/`

  successAdd = "Item was successfully added to cart";

  constructor(
    public db: AngularFirestore,
    public afAuth: AngularFireAuth,
    private http: HttpClient,
    private userComponent: UserComponent,
    //private cartComponent: CartComponent
  ) {
  }
  
  // Adding new Product to cart db 
  addToCart(userId: string, product: Book): void {
   /* var cart = this.cartComponent.cart

    cart.products.push(product);
    this.updateCart(cart);
    this.successAdd;*/
  }

  // Removing product from cart
  removeFromCart(userId : string, product: Book) {
    /*var cart = this.cartComponent.cart

    for (let i = 0; i < cart.products.length; i++) {
      if (cart[i].products.id === product.id) {
        cart.products.splice(i, 1);
        break;
      
    }
    
    this.updateCart(cart);

    //this.calculateLocalCartProdCounts();*/
  }

  //Update Cart
  updateCart(cart: FirebaseCartModel): Observable<FirebaseCartModel> {
    return this.http.put<FirebaseCartModel>(this.url + cart.id, cart, httpOptions);
  }


  // Get Cart 
  getCart(id: string): Observable<FirebaseCartModel> {
    return this.http.get<FirebaseCartModel>(this.url + id);
  }

  // returning LocalCarts Product Count
  /*calculateLocalCartProdCounts() {
    this.navbarCartCount = this.getCart().length;
  }*/
}

