import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { AngularFirestore } from '@angular/fire/firestore';
import { AngularFireAuth } from '@angular/fire/auth';
import { environment } from '../../environments/environment';
import { FirebaseCartModel } from '../core/cart.model';
import { Observable } from 'rxjs';
import { Book } from '../book.details/book.model';


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
};

@Injectable()
export class CartService {
  url: string = `${environment.apiUrl}/cart/`

  successAdd = "Item was successfully added to cart";
  successSave = "Item was successfully saved for later";

  constructor(
    public db: AngularFirestore,
    public afAuth: AngularFireAuth,
    private http: HttpClient
  ) {
  }

  // Adding new Product to cart db 
  addToCart(cart: FirebaseCartModel, product: Book): void {
    cart.products.push(product);

    this.totalPrice(cart);
    this.updateCart(cart).subscribe(res => {});

  }

  // Removing product from cart
  removeFromCart(cart: FirebaseCartModel, product: Book){

    for (let i = 0; i < cart.products.length; i++) {
      if (cart.products[i].title === product.title) {
        cart.products.splice(i, 1);
        break;
      }
    }

    this.totalPrice(cart);
    this.updateCart(cart).subscribe(res=>{});
  }

  removeSavedFromCart(cart: FirebaseCartModel, product: Book){

    for (let i = 0; i < cart.savedForLater.length; i++) {
      if (cart.savedForLater[i].title === product.title) {
        cart.savedForLater.splice(i, 1);
        break;
      }
    }

    this.totalPrice(cart);
    this.updateCart(cart).subscribe(res=>{});
  }

  // Save Product for Later
  saveForLater(cart: FirebaseCartModel, product: Book) {

    cart.savedForLater.push(product);
    this.removeFromCart(cart, product);
    
  }

  //Update Cart
  updateCart(cart: FirebaseCartModel): Observable<FirebaseCartModel> {
    return this.http.put<FirebaseCartModel>(this.url + cart.id, cart, httpOptions);
  }


  // Get Cart 
  getCart(id: string): Observable<FirebaseCartModel> {
    return this.http.get<FirebaseCartModel>(this.url + id);
  }

  // Return Total Price of Cart
  totalPrice(cart: FirebaseCartModel) {
    cart.totalPrice = 0;
    for (let i = 0; i < cart.products.length; i++) {
      cart.totalPrice = (cart.totalPrice * 1) + (cart.products[i].price *1)
    }

  }

}

