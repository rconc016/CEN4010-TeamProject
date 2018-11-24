import * as firebase from "firebase";
import { Book } from '../book.details/book.model';

export class FirebaseCartModel {
  id: string;
  products: Book[];
  savedForLater: Book[];
  purchased: Book[];
  totalPrice: number;

  constructor() {
    this.products = [];
    this.savedForLater = [];
    this.purchased = [];
    this.totalPrice = 0;
    firebase.auth().onAuthStateChanged((user) => {
      if (user) {
        this.id = user.uid;
      } else {
        this.id = '';
      }
    });
  }
}
